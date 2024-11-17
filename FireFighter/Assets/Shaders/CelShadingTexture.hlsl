#ifndef CEL_SHADING_INCLUDED
#define CEL_SHADING_INCLUDED

#ifndef SHADERGRAPH_PREVIEW
struct SurfaceVariables
{
    float3 normal;
    float3 view;
    float smoothness;
    float shininess;
    float rimFalloff;
    float diffuseThreshold;
    float specularThreshold;
    float rimThreshold;
};

float3 CalculateLighting(Light l, SurfaceVariables s) {
    float attenuation = l.shadowAttenuation * l.distanceAttenuation;
    
    float diffuse = saturate(dot(s.normal, l.direction));
    diffuse *= attenuation;
    float preCelDiffuse = diffuse;
    diffuse = diffuse > s.diffuseThreshold ? 1 : 0;
    
    float3 h = SafeNormalize(l.direction + s.view);
    float specular = pow(saturate(dot(s.normal, h)), s.shininess);
    specular *= diffuse;
    specular = specular > s.specularThreshold ? 1 : 0;
    specular *= s.smoothness;
    
    float rim = 1 - dot(s.view, s.normal);
    rim *= pow(preCelDiffuse, s.rimFalloff);
    rim = rim > s.rimThreshold ? 1 : 0;
    rim *= s.smoothness;
    
    return l.color * (diffuse + max(specular, rim));
}
#endif

void CelShading_float(float Smoothness, float RimFalloff, float DiffuseThreshold, float SpecularThreshold, float RimThreshold, float3 Normal, float3 View, float3 positionWS, float4 positionCS, out float3 Color) {
#ifdef SHADERGRAPH_PREVIEW
    Color = float3(1.0f, 0.0f, 1.0f);
#else
    
    SurfaceVariables s;
    s.normal = normalize(Normal);
    s.view = SafeNormalize(View);
    s.smoothness = Smoothness;
    s.shininess = exp2(10 * Smoothness + 1);
    s.rimFalloff = RimFalloff;
    s.diffuseThreshold = DiffuseThreshold;
    s.specularThreshold = SpecularThreshold;
    s.rimThreshold = RimThreshold;
    
    InputData inputData;
    
    //Fix lights disappearing (thank you https://github.com/rsofia/CustomLightingForwardPlus/)
    float4 screenPos = float4(positionCS.x, (_ScaledScreenParams.y - positionCS.y), 0, 0);
    inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(screenPos);
    inputData.positionWS = positionWS;
    
    // calculating shadow coordinate
#ifdef SHADOWS_SCREEN
    // if using screen shadows, convert clip space position to screen position
    float4 shadowCoord = ComputeScreenPos(positionCS);
#else
    // otherwise
    float4 shadowCoord = TransformWorldToShadowCoord(positionWS);
#endif
    
    // initializing lightCount based on main light
    Color = CalculateLighting(GetMainLight(shadowCoord), s);
    
    // looping through additional lights, adding to lightCount if its in at least one light
    int additionalLightCount = GetAdditionalLightsCount();
    LIGHT_LOOP_BEGIN(additionalLightCount)
    Color += CalculateLighting(GetAdditionalLight(lightIndex, positionWS, 1), s);
    LIGHT_LOOP_END
#endif
}
#endif
