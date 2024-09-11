Shader "Testing/Background"
{
    Properties
    {
        _MainTexture("Main Texture", 2D) = "white" {}
        _Colour("Colour", Color) = (1,1,1,1)
        _Ball("Ball Location",Float) = (0,0,0,0)
        _Vel("Ball Velocity",Float) = (0,0,0,0)
    }

    SubShader
    {
        Pass
        {
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
            
            fixed4 _Ball;
            fixed4 _Colour;
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
                float4 pixelColor = tex2D(_MainTexture, (screenPos/1920+_Time.x)%1);
                float2 diff = screenPos - _Ball;
                float mod = sqrt(diff.x*diff.x + diff.y*diff.y);
                float savemod = mod/64;
                diff = diff/mod + pixelColor;
                savemod *= dot(diff,_Vel)+0.6;
                mod = mod*(dot(diff,_Vel)+1)/10;
                float4 outColor = {(sin(mod/2+_Time.y*7)+1)/2,(sin(mod/3-_Time.y*4)+1)/2,(sin(mod/6+_Time.y*10)+1)/2,0.5};
                return outColor*max(1-savemod,0);
            }

            ENDCG
        }
    }
}