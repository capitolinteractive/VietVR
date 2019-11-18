Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" { }
		_Cutoff ("Alpha cutoff", Range(0.000000,1.000000)) = 0.500000
	}
	SubShader
	{
		Tags { "QUEUE"="AlphaTest" "IGNOREPROJECTOR"="true" "RenderType"="TransparentCutout" }
		LOD 100
		ZTest Always
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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			fixed _Cutoff;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				clip(col.a - _Cutoff);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				//return col;
				 float edgeHeight = 0.015;
                return lerp(
                    col,
                    fixed4(0, 4, 2, 1),
					step(col.a, _Cutoff + edgeHeight)
					);
			}
			ENDCG
		}
	}
}
