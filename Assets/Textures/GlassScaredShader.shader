Shader "Custom/GlassShakeShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _EmissionColor ("Emission Color", Color) = (0.000000,0.000000,0.000000,1.000000)
        _Noise("Noise",2D) = "white" {}
        _Shake("Shake",Range(0,6)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:shake

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;


        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Shake;
        fixed4 _EmissionColor;
        sampler2D _Noise;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void shake(inout appdata_full v)
        {
            //float n = tex2D(_Noise, v.yz);
            float4 worldvert = float4(mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0)).xyz,1.0);
            float4 newworldvert = float4(worldvert);
            newworldvert.y +=0.3*_Shake*sin(_Time.w*6+worldvert.x/4);
            newworldvert.x +=0.3*_Shake*sin(_Time.w*6+worldvert.y/4);
            //newworldvert.xy += _Shake*sin(_Time.w*6+1.4*worldvert.yx);
            v.vertex.xyz = mul(unity_WorldToObject, float4(newworldvert.xyz, 1.0)).xyz;
            //v.vertex.x += _Shake*sin(_Time.z+worldvert.x*worldvert.y);


        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Emission = _EmissionColor;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
