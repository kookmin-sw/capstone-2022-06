Shader "Unlit/FogProjection"
{
    Properties
    {
        _CurrFogTex ("Current Fog Texture", 2D) = "gray" {}
        _PrevFogTex ("Previous Fog Texture", 2D) = "gray" {}
        _Color ("Color", Color) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        ZWrite Off
        Offset -1, -1
        // Overwrite to fog
        // Blend DstColor Zero

        // Blend with fog
        Blend SrcAlpha OneMinusSrcAlpha
        // ZTest Equal

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _CurrFogTex, _PrevFogTex;
            float4x4 unity_Projector;
            fixed4 _Color;
            float _Blend;
            float _SemiOpacity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = mul(unity_Projector, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float aCurr = tex2Dproj(_CurrFogTex, i.uv).a;
                float aPrev = tex2Dproj(_PrevFogTex, i.uv).a;

                // float a = min(1, aPrev + aCurr * _SemiOpacity);
                // float a = lerp(aPrev, aCurr, _SemiOpacity);
                fixed a = lerp(aPrev, aCurr, _Blend);
                _Color.a = max(0, _Color.a - a);
                // _Color.a = a;
                return _Color;
            }
            ENDCG
        }
    }
    Fallback Off
}
