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
                float cornerx = saturate(abs(((i.uv.x/HPnormalised)-0.5)*2)-0.8)*5;
                float cornery = abs(i.uv.y*2-1);
                bool incorner = (cornery*cornery + cornerx*cornerx) < 1;
                clip(isinside-0.5);
                clip(incorner-0.5);
                float pulse = 1 + (sin(_Time.y*(20-_HP))-0.5)*0.25*(_HP<20);
                float2 samplepos = float2(HPnormalised,i.uv.y);
                float4 col = tex2D(_MainTex, samplepos);
                //float4 col = lerp(_EndColor,_StartColor,HPcolor)*isinside;
                //return abs(((i.uv.x/HPnormalised)-0.5)*2);
                return col;
            }
            ENDCG
        }
    }
}
