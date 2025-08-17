Shader "URP/WorldGridRadialFade"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _CellSize ("Cell Size (World Units)", Float) = 1
        _Inset ("Box Inset (0-0.49)", Range(0,0.49)) = 0.1
        _Center ("Circle Center (World XZ)", Vector) = (0,0,0,0)
        _Radius ("Fade Radius", Float) = 10
        _Feather ("Edge Feather", Float) = 2
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 worldPos    : TEXCOORD0;
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float  _CellSize;
                float  _Inset;
                float4 _Center;   // Use x,z
                float  _Radius;
                float  _Feather;
            CBUFFER_END

            Varyings vert (Attributes v)
            {
                Varyings o;
                float3 world = TransformObjectToWorld(v.positionOS.xyz);
                o.worldPos = world;
                o.positionHCS = TransformWorldToHClip(world);
                return o;
            }

            // Returns 1 inside the inset box, 0 in the gap near edges
            float BoxMask(float2 p, float inset)
            {
                // p in [0,1)
                float2 a = step(inset, p);
                float2 b = step(inset, 1.0 - p);
                return a.x * a.y * b.x * b.y;
            }

            half4 frag (Varyings i) : SV_Target
            {
                float cs = max(_CellSize, 1e-5);
                float2 gridUV = i.worldPos.xz / cs;
                float2 cell   = frac(gridUV);

                // White square with configurable gap (Inset)
                float inBox = BoxMask(cell, _Inset);

                // Radial fade from world-space center
                float2 centerXZ = float2(_Center.x, _Center.z);
                float d = distance(i.worldPos.xz, centerXZ);
                float fade = 1.0 - smoothstep(_Radius - _Feather, _Radius, d);

                float alpha = inBox * saturate(fade);
                float3 col = _Color.rgb;

                return half4(col * alpha, alpha * _Color.a);
            }
            ENDHLSL
        }
    }
}

