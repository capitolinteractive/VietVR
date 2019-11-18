Shader "Unlit/TestoShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" { }
		_Cutoff ("Alpha cutoff", Range(0.000000,1.000000)) = 0.500000
		radius ("Radius", Range(0,30)) = 15
		resolution ("Resolution", float) = 800
		hstep("HorizontalStep", Range(0,1)) = 0.5
		vstep ("VerticalStep", Range(0,1)) = 0.5
	}
	SubShader
	{
		Tags { "QUEUE"="AlphaTest" "IGNOREPROJECTOR"="true" "RenderType"="TransparentCutout" }
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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			fixed _Cutoff;
			float4 _MainTex_ST;

			float radius;
            float resolution;

			//the direction of our blur
            //hstep (1.0, 0.0) -> x-axis blur
            //vstep(0.0, 1.0) -> y-axis blur
            //for example horizontaly blur equal:
            //float hstep = 1;
            //float vstep = 0;
            float hstep;
            float vstep;
			
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

				float2 uvC = i.uv.xy;
				//float4 sum = float4(0.0, 0.0, 0.0, 0.0);
				float4 sum = fixed4(0, 4, 2, 1);
				float2 tc = uvC;
				float blur = radius/resolution/4;     

				sum += tex2D(_MainTex, float2(tc.x - 4.0*blur*hstep, tc.y - 4.0*blur*vstep)) * 0.0162162162;
                sum += tex2D(_MainTex, float2(tc.x - 3.0*blur*hstep, tc.y - 3.0*blur*vstep)) * 0.0540540541;
                sum += tex2D(_MainTex, float2(tc.x - 2.0*blur*hstep, tc.y - 2.0*blur*vstep)) * 0.1216216216;
                sum += tex2D(_MainTex, float2(tc.x - 1.0*blur*hstep, tc.y - 1.0*blur*vstep)) * 0.1945945946;

                sum += tex2D(_MainTex, float2(tc.x, tc.y)) * 0.2270270270;

                sum += tex2D(_MainTex, float2(tc.x + 1.0*blur*hstep, tc.y + 1.0*blur*vstep)) * 0.1945945946;
                sum += tex2D(_MainTex, float2(tc.x + 2.0*blur*hstep, tc.y + 2.0*blur*vstep)) * 0.1216216216;
                sum += tex2D(_MainTex, float2(tc.x + 3.0*blur*hstep, tc.y + 3.0*blur*vstep)) * 0.0540540541;
                sum += tex2D(_MainTex, float2(tc.x + 4.0*blur*hstep, tc.y + 4.0*blur*vstep)) * 0.0162162162;

                return float4(sum.rgb, 1);

			}
			ENDCG
		}
	}
}
