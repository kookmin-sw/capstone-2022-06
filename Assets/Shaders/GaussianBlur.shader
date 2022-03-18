Shader "Unlit/GaussianBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "gray" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }

        CGINCLUDE
        float2 UVtoXY(float2 uv, float2 texelSize)
        {
            return float2(uv.x / texelSize.x, uv.y / texelSize.y);
        }

        float2 XYtoUV(float2 pos, float2 texelSize)
        {
            return float2(pos.x * texelSize.x, pos.y * texelSize.y);
        }
        ENDCG

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform half4 _MainTex_TexelSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sigma = 1.0
                float gaussianFilter9x9[81] = {
                    0.012162, 0.012231, 0.01228 , 0.012309, 0.012319, 0.012309, 0.01228 , 0.012231, 0.012162,
                    0.012231, 0.012299, 0.012349, 0.012378, 0.012388, 0.012378, 0.012349, 0.012299, 0.012231,
                    0.01228 , 0.012349, 0.012398, 0.012428, 0.012438, 0.012428, 0.012398, 0.012349, 0.01228 ,
                    0.012309, 0.012378, 0.012428, 0.012458, 0.012468, 0.012458, 0.012428, 0.012378, 0.012309,
                    0.012319, 0.012388, 0.012438, 0.012468, 0.012478, 0.012468, 0.012438, 0.012388, 0.012319,
                    0.012309, 0.012378, 0.012428, 0.012458, 0.012468, 0.012458, 0.012428, 0.012378, 0.012309,
                    0.01228 , 0.012349, 0.012398, 0.012428, 0.012438, 0.012428, 0.012398, 0.012349, 0.01228 ,
                    0.012231, 0.012299, 0.012349, 0.012378, 0.012388, 0.012378, 0.012349, 0.012299, 0.012231,
                    0.012162, 0.012231, 0.01228 , 0.012309, 0.012319, 0.012309, 0.01228 , 0.012231, 0.012162,
                };
                int maskWidth = 9;
                int maskOffset = -floor(maskWidth * 0.5);
                float2 texelSize = _MainTex_TexelSize;
                float2 xy = UVtoXY(i.uv, texelSize);
                float4 color = float4(0, 0, 0, 0);

                for (int r = 0; r < maskWidth; ++r)
                {
                    int offsetR = maskOffset + r;
                    for (int c = 0; c < maskWidth; ++c)
                    {
                        int offsetC = maskOffset + c;

                        int2 newRC = int2(xy.x + offsetC, xy.y + offsetR);
                        float2 newUV = XYtoUV(newRC, texelSize);

                        fixed filterPos = r * 9 + c;
                        float filter = gaussianFilter9x9[filterPos];
                        color += (tex2D(_MainTex, newUV) * filter);                      
                    }
                }

                return color;
            }
            ENDCG
        }
    }
}
