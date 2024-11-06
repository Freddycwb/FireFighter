#ifndef CEL_SHADING_INCLUDED
#define CEL_SHADING_INCLUDED

#ifndef SHADERGRAPH_PREVIEW
bool IsInLight(Light l, float3 normal, float incidenceThreshold, float attenuationThreshold) {
    float incidence = saturate(dot(normal, l.direction));
    return incidence > incidenceThreshold && (l.shadowAttenuation * l.distanceAttenuation) > attenuationThreshold;
}
#endif

void CelShading_float(float3 Normal, float3 positionWS, float4 positionCS, float3 LightColor, float3 NormalColor, float3 DarkColor, float IncidenceThreshold, float AttenuationThreshold, out float3 Color) {
#ifdef SHADERGRAPH_PREVIEW
    Color = float3(1.0f, 0.0f, 1.0f);
#else
    
    float3 normal = normalize(Normal);
    
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
    int lightCount = IsInLight(GetMainLight(shadowCoord), normal, IncidenceThreshold, AttenuationThreshold) ? 1 : 0;
    
    // looping through additional lights, adding to lightCount if its in at least one light
    int additionalLightCount = GetAdditionalLightsCount();
    LIGHT_LOOP_BEGIN(additionalLightCount)
    if (IsInLight(GetAdditionalLight(lightIndex, positionWS, 1), normal, IncidenceThreshold, AttenuationThreshold)) {
        ++lightCount;
        break;
    }
    LIGHT_LOOP_END
    
    if (lightCount < 1)
        Color = DarkColor;
    else if (lightCount == 1)
        Color = NormalColor;
    else if (lightCount > 1)
        Color = LightColor;
#endif
}
#endif
