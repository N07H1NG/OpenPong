Shader "Testing/Background"
{
    Properties
    {
        _MainTexture("Main Texture", 2D) = "white" {}
        _Colour("Colour", Color) = (1,1,1,1)
        _Ball("Ball Location",Float) = (0,0,0,0)
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM

            #pragma vertex vertexFunc
            #pragma fragment fragmentFunc

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f{
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
                
            };
            
            fixed4 _Ball;
            fixed4 _Colour;
            sampler2D _MainTexture;

            v2f vertexFunc(appdata IN)
            {
                v2f OUT;
                IN.vertex += _Ball;
                OUT.position = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                
                return OUT;
            }

            fixed4 fragmentFunc(v2f IN) : SV_Target
            {
                fixed4 pixelColor = tex2D(_MainTexture, IN.uv);
                return pixelColor * _Colour;
            }

            ENDCG
        }
    }
}