Shader "Custom/ColorBlendShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlendColor ("Blend Color", Color) = (1, 1, 1, 1)
        _BlendFactor ("Blend Factor", Range(0, 1)) = 0
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Lambert
        
        sampler2D _MainTex;
        fixed4 _BlendColor;
        float _BlendFactor;
        
        struct Input
        {
            float2 uv_MainTex;
        };
        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex);
            
            fixed4 finalColor = lerp(texColor, _BlendColor, _BlendFactor);
            
            o.Albedo = finalColor.rgb;
            o.Alpha = finalColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
