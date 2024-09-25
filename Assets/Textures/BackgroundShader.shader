Shader "Testing/Background"
{
    Properties
    {
        _MainTexture("Main Texture", 2D) = "white" {}
        _Colour("Colour", Color) = (1,1,1,1)
        _Colour1("Colour2", Color) = (1,1,1,1)
        _Colour2("Colour3", Color) = (1,1,1,1)
        _Vel("Ball Velocity",Float) = (0,0,0,0)
    }

    SubShader
    {
        Tags {"PreviewType"="Plane" "Queue"="Transparent" "RenderType"="Transparent"}
        Pass
        {
            Blend SrcAlpha One
            ZWrite Off
            CGPROGRAM

            #pragma vertex vertexFunc
            #pragma fragment fragmentFunc
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                
            };
            
            fixed4 _Colour;
            fixed4 _Colour1;
            fixed4 _Colour2;
            fixed2 _Vel;
            sampler2D _MainTexture;

            v2f vertexFunc(appdata IN, out float4 outposition : SV_POSITION)
            {
                v2f OUT;
                outposition = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                
                return OUT;
            }

            float4 fragmentFunc(v2f IN, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
            {
                float2 diff = (0.5-IN.uv)*190;
                float mod = sqrt(diff.x*diff.x + diff.y*diff.y);
                //float savemod = sqrt(diff.x*diff.x + diff.y*diff.y);
                float2 dir = float2(0,-1);
                float pixelColor = tex2D(_MainTexture, frac(IN.uv*0.6+_Time.x*3*dir));
                float pixelColor2 = tex2D(_MainTexture, frac(IN.uv/8+_Time.x*2*dir));
                diff = diff/mod;
                //mod = mod*((dot(diff,_Vel)+1)/10+pixelColor);
                float mod2 = mod*((dot(diff,_Vel)+1)/8+pixelColor2);
                mod = mod*((dot(diff,_Vel)+1)+pixelColor*4+pixelColor2*6);
                //float coef1 = (sin(mod-_Time.y*2)+1)/3+0.2;
                //float coef2 = (sin(mod-_Time.y*3)+1)/3+0.2;
                //float coef3 = (sin(mod-_Time.y*5)+1)/3+0.2;
                float4 outColor = _Colour;
                //outColor = outColor*saturate(1-mod2*mod2*mod/100)*_Colour;
                outColor.a = saturate(1-mod2*mod/50);
                //return -1*dot(diff,_Vel)*abs(dot(diff,_Vel));
                //return frac(mod);
                return outColor;
            }

            ENDCG
        }
    }
}