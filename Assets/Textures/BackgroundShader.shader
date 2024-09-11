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

            fixed4 fragmentFunc(v2f IN, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
            {
                fixed4 pixelColor = tex2D(_MainTexture, IN.uv);
                fixed2 diff = screenPos - _Ball;
                float mod = sqrt(diff.x*diff.x + diff.y*diff.y);
                diff = diff/mod;
                mod = mod*-1*(dot(diff,_Vel)+1)/10;
                fixed4 outColor = {sin(mod/2),sin(mod/3),sin(mod/6),1.0};
                return outColor;
            }

            ENDCG
        }
    }
}