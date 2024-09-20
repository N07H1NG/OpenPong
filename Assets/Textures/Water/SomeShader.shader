Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor ("Color", Color) = (1,1,1,1)
        _SecondColor ("Color2", Color) = (1,1,1,1)
        _Degree ("Degree", Float) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent+1" }
        LOD 100
        ZTest Always
        GrabPass
        {
            "_BackgroundTexture"
        }

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
                float4 pos : SV_POSITION;
                float4 grabPos : TEXCOORD1;
            };
            sampler2D _BackgroundTexture;
            sampler2D _MainTex;
            float4 _MainColor;
            float4 _SecondColor;
            float4 _Degree;

            float4 lerp(float4 a, float4 b, float4 v){
                return a*(1-v)+b*v;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                return o;
            }

            

            float4 frag (v2f i) : SV_Target
            {
                float3 noiseValue = tex2D(_MainTex,i.grabPos.xy/10+float2(_Time.x,0));
                float4 bg =tex2Dproj(_BackgroundTexture, i.grabPos);
                //noiseValue *= 0.8;
                noiseValue -= 0.25;
                //return float4(noiseValue.xy,0,1);
                i.uv -=0.5;
                float2 seconduv = i.uv/fwidth(i.uv)/64;
                float2 edgedist = 0.5/fwidth(i.uv)/64- abs(seconduv);
                edgedist = min(edgedist.x,edgedist.y);
                float edgedistunsaturated = edgedist;
                edgedist = saturate(edgedist);
                //return edgedist.x;
                float4 grab_recentered = i.grabPos;
                grab_recentered /= float4(32,32,1,1);
                grab_recentered -=float4(0.5,0.5,0,0);
                grab_recentered *= float4(1+noiseValue.xy*edgedist*1.2,1,1);
                grab_recentered +=float4(0.5,0.5,0,0);
                grab_recentered *= float4(32,32,1,1);
                //return grab_recentered;
                //grab_recentered = grab_recentered + float4(0.5,0.5,0,0);
                //return grab_recentered;
                
                //i.grabPos +=float4(noiseValue.x,noiseValue.y,0,noiseValue.z*10);
                // i.grabPos 
                float4 col = tex2Dproj(_BackgroundTexture, grab_recentered);
                //return col;
                //float4 usecolor = lerp(_MainColor,_SecondColor, );
                //return usecolor;
                //return 1 - edgedist.x*2;
                
                float deg = (1- edgedistunsaturated.x*16);
                //return deg;
                float2 map = float2(seconduv.x,deg.x*2-1);
                float wave1 =sin(map.x*3+_Time.y*1);
                float wave2 =sin(map.x*6-_Time.y*4)/2.5;
                
                float colorchoose = saturate(((wave1+wave2-0.8)-map.y)/60);
                //return colorchoose;
                colorchoose = lerp(colorchoose,edgedistunsaturated/2+noiseValue/4,edgedistunsaturated/4);
                //return colorchoose;
                colorchoose = saturate(colorchoose+noiseValue.x*0.0);
                // /return colorchoose;
                float4 usecolor = lerp(_MainColor,_SecondColor, colorchoose);
                //return usecolor;
                bool edge = map.y<wave1+wave2-0.8;
                float4 water = col*(0.2+0.8*usecolor)+usecolor*0.02;
                //return usecolor;
                // return water;
                return lerp(bg,water,edge);
                //4F716B
                //0B1E52
            }
            ENDCG
        }
    }
}
