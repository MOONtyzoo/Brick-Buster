Shader "Unlit/RingWave"
{
    Properties
    {
        [HideInInspector] _uvScale("UV Scale", Float) = 1.0

        [Header(Time Settings)][Space]
        [HideInInspector] _TimeScale ("Time Scale", Float) = 1.0
        [HideInInspector] _Lifetime ("Lifetime", Float) = 1.0
        [HideInInspector] _StartTime ("Start Time", Float) = 0.0

        [Header(Ring Settings)][Space]
        [HideInInspector] _RingNumber ("Number", Integer) = 1
        [HideInInspector] _RingSpawnDelay ("Spawn Delay", Float) = 0.5

        [Header(Speed Settings)][Space]
        [HideInInspector] _RingStartSpeed ("Start Speed", Float) = 1.0
        [HideInInspector] _RingDeceleration ("Deceleration", Float) = 0.0

        [Header(Fade Settings)][Space]
        [HideInInspector] _RingColor ("Color", Color) = (0.0, 0.0, 0.0, 0.0)
        [HideInInspector] _FadeStartTime ("Fade Start Time", float) = 99999.0
        [HideInInspector] _FadeDuration ("Fade Duration", float) = 1.0

        [Header(Width Settings)][Space]
        [HideInInspector] _RingStartWidth("Start Width", float) = 0.1
        [HideInInspector] _RingThinningStartTime ("Thinning Start Time", float) = 99999.0
        [HideInInspector] _RingThinningDuration ("Thinning Duration", float) = 1.0
        
        [Header(Angle Settings)][Space]
        [HideInInspector] _RingAngleSpread ("Angle Spread", Range(0.0, 360.0)) = 360.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" } // Transparent meshes are rendered after opaque meshes
        Blend SrcAlpha OneMinusSrcAlpha // Blend source and destination colors based on transparency
        ZTest Off // If ZTest is on the shader will override Unity's sorting layer for some reason
        LOD 100 // ???

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define PI 3.14159265359

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            /*
                Vertex Shader
            */

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Model-View-Projection
                o.uv = v.uv;
                return o;
            }

            /*
                Fragment Shader
            */

            float _uvScale;

            float _TimeScale;
            float _Lifetime;
            float _StartTime;

            int _RingNumber;
            float _RingSpawnDelay;

            float _RingStartSpeed;
            float _RingDeceleration;

            float4 _RingColor;
            float _FadeStartTime;
            float _FadeDuration;

            float _RingStartWidth;
            float _RingThinningStartTime;
            float _RingThinningDuration;

            float _RingAngleSpread;

            float sdfRing(float2 p, float innerRadius, float outerRadius)
            {
                float distanceFromInnerRadius = innerRadius - length(p);
                float distanceFromOuterRadius = length(p) - outerRadius;
                return max(distanceFromInnerRadius, distanceFromOuterRadius);
            }

            float4 DrawRing(float2 uv, float spawnTime) {
                float ringLifetime = _TimeScale*((_Time.y - _StartTime) % _Lifetime)- spawnTime;
                float ringSpeed = _RingStartSpeed - _RingDeceleration*ringLifetime;
                
                float angle = abs(atan2(uv.y, uv.x));

                float outerRadius = ringSpeed*ringLifetime;
                float ringWidth = _RingStartWidth*clamp((1.0 - (1.0/_RingThinningDuration)*(ringLifetime-_RingThinningStartTime)), 0.0, 1.0);
                float innerRadius = outerRadius-ringWidth;
                
                float isInRing = 1.0-step(0.0, sdfRing(uv, innerRadius, outerRadius));
                isInRing *= 1.0-step((PI/360.0)*_RingAngleSpread, angle);
        
                float3 color = _RingColor;
                float alpha = (isInRing)*clamp((1.0-(1.0/_FadeDuration)*(ringLifetime-_FadeStartTime)), 0.0, 1.0);
                return float4(color, alpha);
            }

            fixed4 frag (v2f input) : SV_Target
            {
                float2 uv = 2.0*(input.uv - 0.5)*_uvScale;

                fixed4 col = float4(uv, 0.0, 0.0);

                float4 rings = float4(0.0, 0.0, 0.0, 0.0);
                for (int i = 0; i < _RingNumber; i++) {
                    float4 ring = DrawRing(uv, float(i)*_RingSpawnDelay);
                    rings = lerp(ring, rings, 1.0-ring.a); // Add rings together via alpha blending
                }
                
                col = rings;

                return col;
            }
            ENDCG
        }
    }
}
