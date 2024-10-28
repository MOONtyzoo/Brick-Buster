Shader "Unlit/BackgroundShader"
{
    Properties
    {
        _LineColorA ("Line Color A", Color) = (1, 1, 1, 1)
        _LineColorB ("Line Color B", Color) = (0.9, 0.9, 0.9, 0.9)
        _LineWidth ("Line Width", Float) = 0.2
        _ScrollSpeed ("Scroll Speed", Float) = 0.25
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float2 uv : TEXCOORD;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            /*
                Fragment Shader
            */
            float4 _LineColorA;
            float4 _LineColorB;
            float _LineWidth;
            float _ScrollSpeed;

            float2 rotate2D(in float2 uv, in float angle)
            {
                float2x2 rotationMatrix = {cos(angle), -sin(angle), sin(angle), cos(angle)};
                return mul(uv, rotationMatrix);
                
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = 2.0*(i.uv - 0.5);
                uv.x *= 16.0/9.0;

                float2 rotatedUv = rotate2D(uv, PI/4.0);
                rotatedUv += float2(_Time.x*_ScrollSpeed, 0.0);

                float dist = abs(rotatedUv.x)/_LineWidth + 0.5;
                float fractDist = 2.0*frac(dist/2.0);

                float lines = step(fractDist, 1.0);
                
                fixed4 col = (1.0-lines)*_LineColorA + lines*_LineColorB;
                return col;
            }
            ENDCG
        }
    }
}
