#ifndef CEL_SHADING_INCLUDED
#define CEL_SHADING_INCLUDED

#ifndef SHADERGRAPH_PREVIEW
float3 CalculateLighting(Light l, float3 normal, float3 position, float3 lightColor, float3 darkColor) {
    float incidence = saturate(dot(normal, l.direction));
    
    int additionalLights = GetAdditionalLightsCount();
    for (int i = 0; i < additionalLights; ++i) {
        incidence += saturate(dot(normal, GetAdditionalLight(i, position, 1).direction));
    }
    
    bool inLight = incidence > 0.2f && l.shadowAttenuation > 0.5f;
    
    return inLight ? lightColor : darkColor;
}
#endif

void CelShading_float(float3 Normal, float3 Position, float3 LightColor, float3 DarkColor, out float3 Color) {
#ifdef SHADERGRAPH_PREVIEW
    Color = float3(1.0f, 0.0f, 1.0f);
#else
    
    // calculating shadow coordinate
#ifdef SHADOWS_SCREEN
    // if using screen shadows, convert the position to clip space position and then to screen position
    float4 clipPos = TransformWorldToHClip(Position);
    float4 shadowCoord = ComputeScreenPos(clipPos);
    
    Color = 0.0f;
    return;
#else
    // otherwise
    float4 shadowCoord = TransformWorldToShadowCoord(Position);
#endif
    
    Color = CalculateLighting(GetMainLight(shadowCoord), normalize(Normal), Position, LightColor, DarkColor);
    //Color = float3(1.0f, 0.5f, 0.5f);
#endif
}
#endif
