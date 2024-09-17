Shader "Unlit/Healthbar"
{
    Properties
    {
        _HP("Health",Range(0,100)) = 100
        _StartColor("Start Color", Color) = (0,1,0,0)
        _EndColor("End Color", Color) = (1,0,0,0)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _StartColor;
            float4 _EndColor;
            float _HP;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                
                float HPnormalised = _HP/100;
                //float HPcolor = (_HP/80 - 0.25)/0.75;
                bool isinside = (i.uv.x<=HPnormalised);
                float2 uvnorm = i.uv*float2(8,1);
                float sdf = distance(uvnorm,float2(clamp(uvnorm.x,0.5,7.5), 0.5))*2 - 1;
                //return length(xcoord);
                //bool incorner = (cornery*cornery + cornerx*cornerx) < 1;
                //clip(isinside-0.5);
                //float sdf = distance(dir*float2(8,1),float2(clamp(dir.x,-HPnormalised+0.125,HPnormalised-0.125),0)*float2(8,1));
                clip(-sdf); 
                float border = sdf+0.2;
                float pd = fwidth(border);
                //float bordermask = step(0,border);
                float bordermask = 1-saturate(border/pd);
                float pulse = 1 + (sin(_Time.y*(10))-0.5)*0.25*(_HP<20);
                float2 samplepos = float2(HPnormalised,i.uv.y);
                float4 col = tex2D(_MainTex, samplepos);
                //float4 col = lerp(_EndColor,_StartColor,HPcolor);
                //return abs(((i.uv.x/HPnormalised)-0.5)*2);
                //return border/fwidth(border);
                //step(border,1);
                return col*bordermask*pulse;
            }
            ENDCG
        }
    }
}
