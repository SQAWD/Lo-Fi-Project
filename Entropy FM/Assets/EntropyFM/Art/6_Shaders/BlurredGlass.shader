Shader "Unlit/BlurredGlass"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0, 10)) = 1.0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                half4 color = tex2D(_MainTex, uv);

                // Simple blur by averaging surrounding pixels
                color += tex2D(_MainTex, uv + float2(_BlurSize, 0));
                color += tex2D(_MainTex, uv - float2(_BlurSize, 0));
                color += tex2D(_MainTex, uv + float2(0, _BlurSize));
                color += tex2D(_MainTex, uv - float2(0, _BlurSize));

                return color * 0.2; // Average the colors
            }
            ENDCG
        }
    }
}
