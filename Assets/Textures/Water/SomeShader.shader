Shader "Unlit/WaterShader"
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
        ZWrite Off
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
            //float4 _ScreenParams;

            float4 lerp(float4 a, float4 b, float4 v){
                return a*saturate(1-v)+b*saturate(v);
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
                float3 noiseValue = tex2D(_MainTex,frac(i.grabPos.xy/16+float2(_Time.x,0)));
                // /return noiseValue.xxxx;
                float4 bg =tex2Dproj(_BackgroundTexture, i.grabPos);

                noiseValue -= 0.25;
                i.uv -=0.5;
                float2 seconduv = i.uv/fwidth(i.uv)/64;
                float2 edgedist = 0.5/fwidth(i.uv)/64- abs(seconduv);
                float vertdist = edgedist.y;
                edgedist = min(edgedist.x,edgedist.y);
                float edgedistunsaturated = vertdist;
                edgedist = saturate(edgedist);

                float4 grab_recentered = i.grabPos;
                grab_recentered /= float4(i.grabPos.w,i.grabPos.w,1,1);
                grab_recentered -=float4(0.5,0.5,0,0);
                //return grab_recentered;
                //grab_recentered *= float4(1+noiseValue.xy*edgedist*0.2,1,1);
                grab_recentered += float4(noiseValue.xy*edgedist*0.05,0,0);
                //return grab_recentered;
                //return float4(1+noiseValue.xy*edgedist*0.3,0,1);
                //return edgedist.x;
                grab_recentered +=float4(0.5,0.5,0,0);
                grab_recentered *= float4(i.grabPos.w,i.grabPos.w,1,1);
                float4 col = tex2Dproj(_BackgroundTexture, grab_recentered);
                float reflectionPos = i.grabPos.y/i.grabPos.w;
                reflectionPos += sign(i.uv.y)*2*vertdist*64/_ScreenParams.y;
                reflectionPos *= i.grabPos.w;
                float4 reflection = tex2Dproj(_BackgroundTexture, float4(grab_recentered.x,reflectionPos,grab_recentered.zw));

                
                float deg = (1- edgedistunsaturated.x*8);

                float2 map = float2(seconduv.x,deg.x*2-1);
                float wave1 =sin(map.x*3+_Time.y*1);
                float wave2 =sin(map.x*5-_Time.y*4)/2.5;
                
                float colorchoose = saturate(((wave1+wave2-0.8)-map.y)/60);

                colorchoose = lerp(colorchoose,edgedistunsaturated/2+noiseValue/2,edgedistunsaturated/4);
                colorchoose = saturate(colorchoose);
                //colorchoose -= noiseValue/16;

                float4 usecolor = lerp(_MainColor,_SecondColor, colorchoose);
                float4 c = usecolor;
                usecolor = lerp(reflection,usecolor,0.1+colorchoose);
                usecolor = max(usecolor,c*0.2);

                bool edge = map.y<wave1+wave2-0.8;
                float4 water = col*(0.2+0.8*usecolor)+usecolor*0.04;

                return lerp(bg,water,edge);

            }
            ENDCG
        }
    }
}
