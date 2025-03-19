#ifndef PUDDLE_EFFECT_INCLUDED
#define PUDDLE_EFFECT_INCLUDED

#define MAX_PUDDLES 5

//float4 _PuddleEffects[MAX_PUDDLES];
float4 _PuddleEffect;
 
void PuddleIntersection_float(float3 WorldPos, float RadiusMargin, out float Intersection) {
#ifdef SHADERGRAPH_PREVIEW
    Intersection = 0.0f;
#else
    float combined = 0.0f;
    
    //for (int i = 0; i < MAX_PUDDLES; ++i) {
    //    float dist = distance(WorldPos, _PuddleEffects[i].xyz);
    //    float radius = _PuddleEffects[i].w + RadiusMargin;
    //    combined += 1.0f - saturate(dist / radius);
    //}
    
    float dist = distance(WorldPos, float3(-1.0f, 0.0f, 1.0f));
    float radius = 0.5f + RadiusMargin;
    combined += 1.0f - saturate(dist / radius);
    
    Intersection = saturate(combined);
#endif    
}

#endif
