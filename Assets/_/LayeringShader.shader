Shader "Custom/Layering/LayeringShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        
        // Blend SrcAlpha OneMinusSrcAlpha
        // Blend One SrcAlpha
        LOD 200
        // Blend SrcAlpha OneMinusSrcAlpha
        // ZWrite Off
        // Cull Off

        // Blend SrcAlpha OneMinusSrcAlpha, Zero OneMinusSrcAlpha
        // ZWrite Off

        ZTest Always Cull Off ZWrite Off
        Lighting Off
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            // ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            // make fog work
            // #pragma multi_compile_fog

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _LastTex;
            float4 _MainTex_ST;
            float _SrcAlpha;
            float _ColorLerp;
            float _ColorAlpha;
            float _IsClear;
            

            fixed4 frag (v2f_img i) : COLOR
            {
                fixed4 src = tex2D(_MainTex, i.uv);
                fixed4 lastSrc = tex2D(_LastTex, i.uv);
                fixed4 col = lerp(src, lastSrc, _ColorLerp);
                // fixed4 col = lerp(src, fixed4(0,0,0,1), _ColorLerp);
                
                col = src;
                col.a = _ColorAlpha;
                // col.rgb = max(src.rgb, lastSrc.rgb);
                // col = lastSrc + src;
                // col = lastSrc;

                if( _IsClear == 1 ) col = fixed4(0,0,0,1);
                else if( _IsClear == 2 ) col = fixed4(src.a,src.a,src.a, 1);

                // col = fixed4(1, 0, 1, 1);

                return col;
            }
            ENDCG
        }
    }
}
