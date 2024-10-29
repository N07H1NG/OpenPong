Shader "Unlit/BlackHole"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Color2", Float) = 1
        _Degree ("Degree", Float) = 1
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
                float4 cent : TEXCOORD2;
            };
            sampler2D _BackgroundTexture;
            sampler2D _MainTex;
            float _Radius;
            float _Degree;

            float4 lerp(float4 a, float4 b, float4 v){
                return a*saturate(1-v)+b*saturate(v);
            }

            float2 rotate(float2 v, float ang){
                return float2(v.x*cos(ang)-v.y*sin(ang),v.x*sin(ang)+cos(ang)*v.y);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                o.grabPos/=o.grabPos.w;
                float4 centpos = UnityObjectToClipPos(float4(0,0,0,1));
                o.cent = ComputeGrabScreenPos(centpos);
                o.cent/=o.cent.w;
                return o;
            }

            

            float4 frag (v2f i) : SV_Target
            {
                
                float4 grab_recentered = i.grabPos;
                float4 center_recentered = i.cent;
                grab_recentered -=float4(0.5,0.5,0,0);
                center_recentered -=float4(0.5,0.5,0,0);
                float2 diff = grab_recentered.xy-center_recentered.xy;
                i.uv -= float4(0.5,0.5,0,0);
                float rad = clamp(length(i.uv)*2,0,1);
                float deform = 1/pow(rad*pow(_Degree,0.5),2)*_Radius*2;
                float2 true_deform = diff*(1+deform);
                true_deform = rotate(true_deform,4*sin(3*_Time.x)*pow(1-rad,2));
                true_deform = lerp(diff,true_deform,(1-rad)/2);
                //float2 true_deform = lerp(diff*2,diff,rad);
                grab_recentered = center_recentered+float4(true_deform.xy,0,0);
                grab_recentered /=2;
                grab_recentered +=float4(0.5,0.5,0,0);
                
                float4 col = tex2D(_MainTex, grab_recentered);
                
                return col;

            }
            ENDCG
        }
    }
}
