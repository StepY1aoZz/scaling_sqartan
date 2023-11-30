Shader "Custom/Jump"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("add color", Color) = (1,1,1,1)
        _BeAttack("BeAttack", Int) = 0
    }
    SubShader
    {
        Tags { "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                half4 vertex:POSITION;
                float2 texcoord:TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                half4 pos:SV_POSITION;
                float2 uv:TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            int _BeAttack;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                if (_BeAttack == 1) 
                {
                    tex.rgb = tex.rgb + _Color.rgb;
                }
                return tex;
            }
            ENDCG
        }
    }
}