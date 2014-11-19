Shader "Custom/CameraMaterial" 
{
	Properties 
	{
		_MainTex ("Main", 2D) = "white"  {}
		_SecondTex ("Second", 2D) = "White" {}
		
		_UseRandom			 ("UseRandom", Range (0,1)) = 0.0
		_Random 			 ("Random", Range (0,1)) = 0.5
		_Random2 			 ("Random2", Range (0,1)) = 0.5
		_Lerp		 		 ("LerpEffect", Range (0,1)) = 0
		_Alpha 				 ("RemoveWhite", Range (0,1)) = 0
		_BlackAndWhite 		 ("BlackAndWhite", Range (0,1)) = 0
		_BlackAndWhiteEffect ("BlackAndWhiteEffect", Range (0,1)) = 0
	}
	SubShader 
	{
		Tags { "RenderType"="Transparent" }
		
		Pass{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		sampler2D _SecondTex;
		float _Lerp;
		float _Random;
		float _Random2;
		int _UseRandom;
		int _Alpha;
		int _BlackAndWhite;
		int _BlackAndWhiteEffect;
		
		/////////
		struct vertexInput
		{
			float4 vertex : POSITION;
			float4 texCoord : TEXCOORD;
			float4 texCoord2 : TEXCOORD1;
		};
		
		struct fragmentInput
		{
			float4 pos : POSITION;
			half2 uv : TEXCOORD;
			half2 uv2 : TEXCOORD1;
		};
		
		fragmentInput vert( vertexInput v )
		{
			fragmentInput o;
			
			o.pos = mul( UNITY_MATRIX_MVP, v.vertex );
			o.uv = v.texCoord;
			o.uv2 = v.texCoord2;
			
			if(_UseRandom != 0)
			{
				o.uv2.x += _Random;
				o.uv2.y += _Random2;
			}
			
			return o;
		}
		
		half4 frag( fragmentInput l ) : COLOR
		{
			//Setup for defaultShader
			float4 FinalColor;
			float4 RendTexCol = tex2D(_MainTex, l.uv);
			float4 SecondCol  = tex2D(_SecondTex, l.uv2);
			//float4 EffectCol  = tex2D(_EffectTex, l.uv);
			//float4 ColorDef   = float4(clamp(SecondCol.x, 0, _Clamp));
			//float4 ColorMix1  = lerp(RendTexCol, ColorDef, _Lerp);
			FinalColor = RendTexCol;
			
			//Setup for Effects
			if(_BlackAndWhite != 0)
			{
				if(_BlackAndWhiteEffect != 0)
				{
					float4 ColorAlpha = SecondCol;
					if(_Alpha != 0)
					{
						ColorAlpha     = lerp(SecondCol, FinalColor, SecondCol.x);
					}
					float4 ColorBWLerp  = lerp(FinalColor, ColorAlpha, _Lerp);
					float4 ColorBWFinal = (ColorBWLerp.x);
					FinalColor 			= ColorBWFinal;
				}
				else
				{
					float4 ColorBW    = (FinalColor.x);
					float4 ColorAlpha = SecondCol;
					if(_Alpha != 0)
					{
						ColorAlpha     = lerp(SecondCol, ColorBW, SecondCol.x);
					}
					float4 ColorBWEffect = lerp(ColorBW, ColorAlpha, _Lerp);
					FinalColor 			 = ColorBWEffect;
				}
			}
			else if(_Alpha != 0)
			{
				//Removing the white
				float4 ColorAlpha     = lerp(SecondCol, FinalColor, SecondCol.x);
				float4 ColorAlphaLerp = lerp(FinalColor, ColorAlpha, _Lerp);
				FinalColor = ColorAlphaLerp;
			}
			else 
			{
				//only blend in the effect
				float4 ColorNormal = lerp(FinalColor, SecondCol, _Lerp);
				FinalColor = ColorNormal;
			}
			
			return FinalColor;
		}
		
		//half4 frag( fragmentInput l ) : COLOR
		//{
		//	float4 maincol = tex2D(_MainTex, l.uv);
		//	float4 texcol = tex2D(_SecondTex, l.uv2);
		//	float4 test = (0,0,0,0);
		//	test = float4(lerp(maincol, texcol, _Lerp));
		//	
		//	return float4(test);
		//}
		
		/////
		ENDCG
		}
	} 
	FallBack "Diffuse"
}

