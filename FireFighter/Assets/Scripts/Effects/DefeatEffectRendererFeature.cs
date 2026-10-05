using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DefeatEffectRendererFeature : ScriptableRendererFeature
{
	public Shader effectShader;
	private DefeatEffectRenderPass _effectRenderPass;
	
	public override void Create() {
		_effectRenderPass = new DefeatEffectRenderPass(CoreUtils.CreateEngineMaterial(effectShader));
	}
	
	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData _renderingData) {
		renderer.EnqueuePass(_effectRenderPass);
	}
}

