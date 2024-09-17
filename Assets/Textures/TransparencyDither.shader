Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _DitherTex ("DitherTexture", 2D) = "white" {}
        _MainTex ("Main Texture", 2D) = "whine" {}
        _SectorSize ("Sector", Range(0,1)) = 0.2
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

            float4 frag (v2f i, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
            {
                // sample the texture
                
                float4 color = tex2D(_MainTex,i.uv);
                float brightness = get_brightness(color);
                float brightness_offset = floor(brightness/_SectorSize)*_SectorSize;
                float2 sampleuv = float2((screenPos.x%32)/32,(screenPos.y%32/32));
                float4 dither = tex2D(_DitherTex, sampleuv);
                float4 tex = tex2D(_DitherTex,i.uv);
                bool dith = brightness>=dither;
                return dith;
                return float4(sampleuv,0,1);
            }
            ENDCG
        }
    }
}
