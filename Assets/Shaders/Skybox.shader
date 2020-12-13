Shader "Unlit/Skybox"
{
    Properties
    {
        _MainTex ("Clouds Texture", 2D) = "black" {}
		_CloudsFactor ("Clouds quantity", Range(0, 1)) = 0
		_CloudsHeight ("Clouds height", Range(0, 3)) = 0
		_CloudsGradientForce ("Clouds gradient force", Range(0, 1)) = 0
		_CloudsSmoothness ("Clouds smoothness", Range(0.01, 1)) = 0
		[HDR] _TopColor ("Top Color", Color) = (1, 1, 1, 1)
		[HDR] _BottomColor ("Bottom Color", Color) = (1, 1, 1, 1)
		_Height ("Height", Range(0, 3)) = 0
		[HDR] _SunColor ("Sun Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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
				float3 viewDir : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _TopColor, _BottomColor, _SunColor;
			float _CloudsFactor, _CloudsHeight, _CloudsGradientForce, _CloudsSmoothness, _Height;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.viewDir = WorldSpaceViewDir(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				half4 clouds = tex2D(_MainTex, i.uv);
				float factor = 1 - _CloudsFactor;
				clouds = smoothstep(factor-_CloudsSmoothness, factor+_CloudsSmoothness, clouds);
				float gradientForce = _CloudsGradientForce;
                clouds = smoothstep(0, 1-gradientForce, lerp(0, clouds, pow(saturate(i.uv.y), _CloudsHeight)));

				float sun = smoothstep(0.99, 1, (dot(normalize(_WorldSpaceLightPos0), -normalize(i.viewDir))));

                fixed4 col = lerp(_BottomColor, _TopColor, pow(saturate(i.uv.y), _Height));
				col = col * (1-clouds) + clouds;
				col += sun * _SunColor;
                return col;
            }
            ENDCG
        }
    }
}
