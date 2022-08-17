Shader "Hidden/Pixel"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorDepth("Color Depth", Integer) = 64
        _DitherStrength("DitherStrength", Range(0.0,1.0)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            Texture2D _MainTex;
            SamplerState point_clamp_sampler;

            int _ColorDepth;
            float _DitherStrength;
            static const float4x4 ditherTable = float4x4
                (
                    0, 8, 2, 10,
                    12, 4, 14, 6,
                    3, 11, 1, 9,
                    15, 7, 13, 5
                );

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = _MainTex.Sample(point_clamp_sampler, i.uv);
                
                uint2 pixelCoord = i.uv * _ScreenParams.xy;
                col += ditherTable[pixelCoord.x*0.5 % 4][pixelCoord.y*0.5 % 4] / 15 * _DitherStrength;
                col = round(col * _ColorDepth) / _ColorDepth;
                return col;
            }
            ENDCG
        }
    }
}
