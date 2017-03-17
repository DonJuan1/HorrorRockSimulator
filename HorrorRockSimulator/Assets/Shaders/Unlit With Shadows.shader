﻿Shader "Unlit/Transparent Cutout (Shadow)" {
	Properties{
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Cutoff("Alpha cutoff", Range(0, 1)) = 0.5
	}
	SubShader{
			Tags{ "Queue" = "AlphaTest" "RenderType" = "TransparentCutout" }

			Pass{
				Tags{ "LightMode" = "ForwardBase" }
				CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdbase

#include "UnityCG.cginc"
#include "AutoLight.cginc"

				struct v2f
				{
					float4  pos         : SV_POSITION;
					float2  uv          : TEXCOORD0;
					LIGHTING_COORDS(1, 2)
				};

				v2f vert(appdata_tan v)
				{
					v2f o;

					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					o.uv = v.texcoord.xy;
					TRANSFER_VERTEX_TO_FRAGMENT(o);
					return o;
				}

				sampler2D _MainTex;
				float _Cutoff;
				fixed4 _Color;

				fixed4 frag(v2f i) : COLOR
				{
					fixed4 color = tex2D(_MainTex, i.uv);
					clip(color.a - _Cutoff);

					//fixed atten = LIGHT_ATTENUATION(i); // Light attenuation + shadows.
					fixed atten = SHADOW_ATTENUATION(i) + 0.25 ; // Shadows ONLY.
					return color * atten * _Color;
				}
					ENDCG
			}
		
		}

		Fallback "Transparent/Cutout/VertexLit"
}