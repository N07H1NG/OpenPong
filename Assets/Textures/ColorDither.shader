Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _DitherTex ("DitherTexture", 2D) = "white" {}
        _MainTex ("Main Texture", 2D) = "whine" {}
        _SectorSize ("Sector", Range(0,1)) = 0.2
        _PatternScale("Scale",Range(0,1)) = 1
        _Divisions("Divisions", Range(1,16)) = 1
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

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : TEXCOORD1;
            };

            sampler2D _DitherTex;
            sampler2D _MainTex;
            float _SectorSize;
            float _PatternScale;
            float _Divisions;

            v2f vert (appdata v, out float4 outposition : SV_POSITION)
            {
                v2f o;
                outposition = UnityObjectToClipPos(v.vertex);
                o.pos = v.vertex;
                o.uv = v.uv;
                return o;
            }

            float maxofthree(float a, float b, float c){
                return max(max(a,b),c);
            }

            float minofthree(float a, float b, float c){
                return min(min(a,b),c);
            }

            float get_saturation(float3 color)
            {
                float r = color.r;
                float g = color.g;
                float b = color.b;
                float cmax = maxofthree(r,g,b);
                float cmin = minofthree(r,g,b);
                float l = (cmax + cmin)/2;
                float d = cmax - cmin;

                return d/(1-abs(2*l - 1));
            }

            float get_brightness(float3 color)
            {
                float r = color.r;
                float g = color.g;
                float b = color.b;
                float cmax = maxofthree(r,g,b);
                float cmin = minofthree(r,g,b);
                return (cmax + cmin)/2;

            }

            float4 inverse_lerp(float4 x, float4 a, float4 b)
            {
                return saturate((x-a)/(b-a));
            }

            float4 frag (v2f i, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
            {
                float4 color = tex2D(_MainTex,i.uv);
                float2 sampleuv = float2((_PatternScale*screenPos.x%32)/32,(_PatternScale*screenPos.y%32/32));
                float4 dither = tex2D(_DitherTex, sampleuv);
                bool dithred = color.r>dither;
                bool dithgreed = color.g>dither;
                bool dithblue = color.b>dither;
                return float4(dithred,dithgreed,dithblue,1);
            }
            ENDCG
        }
    }
}
