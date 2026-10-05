using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;

public class DefeatEffectRenderPass : ScriptableRenderPass
{
	private Material _effectMaterial;
	
	public class PassData {
		public Material passMaterial;
		public TextureHandle sourceTexture;
	}
	
	public DefeatEffectRenderPass(Material effectMaterial) {
		_effectMaterial = effectMaterial;
		renderPassEvent = RenderPassEvent.BeforeRendering;
	}
	
	public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameContext) {
		// defining render pass
		using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<PassData>(
			"Defeat Effect",
			out var passData
		)) {
			// getting texture resources
			UniversalResourceData frameData = frameContext.Get<UniversalResourceData>();
			
			// getting target texture descriptor
			TextureDesc targetDesc = frameData.activeColorTexture.GetDescriptor(renderGraph);
			
			// initializing pass data
			passData.passMaterial = _effectMaterial;

			TextureDesc sourceDesc = new TextureDesc(targetDesc.width, targetDesc.height);
			sourceDesc.format = targetDesc.format;

			passData.sourceTexture = renderGraph.CreateTexture(sourceDesc);
			
			// setting source texture
			builder.UseTexture(passData.sourceTexture);
			
			// setting render texture
			builder.SetRenderAttachment(frameData.activeColorTexture, 0, AccessFlags.WriteAll);
			
			// setting rendering function for the render pass
			builder.SetRenderFunc(static (PassData data, RasterGraphContext context) => ExecutePass(data, context));
		}
	}
	
	public static void ExecutePass(PassData data, RasterGraphContext context) {
		Blitter.BlitTexture(context.cmd, data.sourceTexture, new Vector4(1, 1, 0, 0), data.passMaterial, 0);
	}
}

