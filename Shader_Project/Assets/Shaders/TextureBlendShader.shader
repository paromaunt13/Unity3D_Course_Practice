Shader "Custom/TextureBlendShader"
{
    Properties
    {
        _MainTex1 ("Texture 1", 2D) = "white" {}
        _MainTex2 ("Texture 2", 2D) = "white" {}
        _BlendFactor ("Blend Factor", Range(0, 1)) = 0
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Lambert
        
        sampler2D _MainTex1;
        sampler2D _MainTex2;
        float _BlendFactor;
        
        struct Input
        {
            float2 uv_MainTex1;
            float2 uv_MainTex2;
        };
        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 color1 = tex2D(_MainTex1, IN.uv_MainTex1);
            fixed4 color2 = tex2D(_MainTex2, IN.uv_MainTex2);
            
            fixed4 finalColor = lerp(color1, color2, _BlendFactor);
            
            o.Albedo = finalColor.rgb;
            o.Alpha = finalColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
