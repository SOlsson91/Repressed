// Upgrade NOTE: replaced 'samplerRECT' with 'sampler2D'
// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

Shader "Godrays/PS2" {
	Properties {
		_MainTex ("Base (RGB)", RECT) = "white" {}
		_sunpos ("Sun Pos (Automatic)", vector) = (0.5,0.5,0.0,0.0)
		_params ("Weight Exposure Density Decay", vector) = (0.2,0.9,0.5,0.9)
	}
	
	SubShader{ // PS2.0, Seperate Color (Colored rays), 40 layers
	LOD 400
	
pass // pass, 8 layers
{
ZTest Always Cull Off ZWrite Off
Fog { Mode off }

CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
#pragma exclude_renderers gles
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform float4 _sunpos;
uniform float4 _params;

struct v2f {
	float4	pos : POSITION;
	float2 uv : TEXCOORD0;
};
float4 frag (v2f v) : COLOR
{
	half2 deltaTexCoord = (v.uv - _sunpos.xy);
	
	deltaTexCoord *= 0.0625 * _params.z;
	
	float4 color = tex2D(_MainTex,v.uv);
	
	v.uv += deltaTexCoord * 0.5;
	
	half illuminationDecay = 1.0f;
	
	_params.a = _params.a * _params.a;
	
	for (int i = 0; i < 8; ++i)
	{
		v.uv -= deltaTexCoord;
		
		half4 sample = tex2D(_MainTex,v.uv);
		
		sample.rgb *= illuminationDecay * _params.x * sample.a * 0.0625;
		
		color.rgb += sample.rgb;// + 1;
		
		illuminationDecay *= _params.a;// * _params.a;
	}
	
	return (color * _params.y) * 0.5;
};

ENDCG

}

pass // pass, 8 layers
{
ZTest Always Cull Off ZWrite Off
Fog { Mode off }
blend One One

CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
#pragma exclude_renderers gles
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform float4 _sunpos;
uniform float4 _params;

struct v2f {
	float4	pos : POSITION;
	float2 uv : TEXCOORD0;
};
float4 frag (v2f v) : COLOR
{
	half2 deltaTexCoord = (v.uv - _sunpos.xy);
	
	deltaTexCoord *= 0.0625 * _params.z;
	
	float4 color = tex2D(_MainTex,v.uv);
	
	//v.uv += deltaTexCoord * 0.5;
	
	half illuminationDecay = 1.0f;
	
	_params.a = _params.a * _params.a;
	
	for (int i = 0; i < 8; ++i)
	{
		v.uv -= deltaTexCoord;
		
		half4 sample = tex2D(_MainTex,v.uv);
		
		sample.rgb *= illuminationDecay * _params.x * sample.a * 0.0625;
		
		color.rgb += sample.rgb;// + 1;
		
		illuminationDecay *= _params.a;// * _params.a;
	}
	
	return (color * _params.y * 0.5);
};

ENDCG

}
}
	fallback off
}