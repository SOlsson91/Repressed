// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'
// Upgrade NOTE: replaced 'glstate.matrix.texture[0]' with 'UNITY_MATRIX_TEXTURE0'
// Upgrade NOTE: replaced 'samplerRECT' with 'sampler2D'
// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

Shader "Godrays Colored" {
	Properties {
		_MainTex ("Base (RGB)", RECT) = "white" {}
		_sunpos ("Sun Pos (Automatic)", vector) = (0.5,0.5,0.0,0.0)
		_params ("Weight Exposure Density Decay", vector) = (0.2,0.9,0.5,0.9)
	}
	
	SubShader { // PS3.0, Bipass, 31 layers
		LOD 1500
		pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform float2 _sunpos;
uniform float4 _params;

struct v2f {
	float4	pos : POSITION;
	float2 uv : TEXCOORD0;
};

v2f vert( appdata_img v )
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	float2 uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
	o.uv = uv;
	return o;
};

half3 frag (v2f v) : COLOR
{
	half2 deltaTexCoord = (v.uv - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 31 * _params.z;
	
	float3 color = tex2D(_MainTex, v.uv);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 31; ++i)
	{
		v.uv -= deltaTexCoord;
		
		half4 sample = tex2D(_MainTex,v.uv);
		
		sample.rgb *= sample.a * 0.03225806452; // 1/31, the sample count
		
		sample.rgb *= illuminationDecay * _params.x;
		
		color += sample.rgb;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.5;
};

ENDCG
		}
		
		pass // Second pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Blend One One
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform float2 _sunpos;
uniform float4 _params;

struct v2f {
	float4	pos : POSITION;
	float2 uv : TEXCOORD0;
};

v2f vert( appdata_img v )
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	float2 uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
	o.uv = uv;
	return o;
};

half3 frag (v2f v) : COLOR
{
	half2 deltaTexCoord = (v.uv - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 31 * _params.z;
	
	float3 color = tex2D(_MainTex, v.uv);
	
	v.uv += deltaTexCoord / 2;
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 31; ++i)
	{
		v.uv -= deltaTexCoord;
		
		half4 sample = tex2D(_MainTex,v.uv);
		
		sample.rgb *= sample.a * 0.03225806452; // 1/31, the sample count
		
		sample.rgb *= illuminationDecay * _params.x;
		
		color += sample.rgb;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.5;
};

ENDCG
		}
	}
	
	// --------- End Bipass
	
	SubShader { // PS3.0, Single pass, 31 layers
		LOD 1000
		pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform float2 _sunpos;
uniform float4 _params;

struct v2f {
	float4	pos : POSITION;
	float2 uv : TEXCOORD0;
};

v2f vert( appdata_img v )
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	float2 uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
	o.uv = uv;
	return o;
};

half3 frag (v2f v) : COLOR
{
	half2 deltaTexCoord = (v.uv - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 31 * _params.z;
	
	float3 color = tex2D(_MainTex, v.uv);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 31; ++i)
	{
		v.uv -= deltaTexCoord;
		
		half4 sample = tex2D(_MainTex,v.uv);
		
		sample.rgb *= sample.a * 0.03225806452; // 1/31, the sample count
		
		sample.rgb *= illuminationDecay * _params.x;
		
		color += sample.rgb;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y;
};

ENDCG
		}
	}
	// ------------------ End PS3.0, 31-layer
	
	SubShader { // PS3.0, Single pass, 16 layers
		LOD 450
		pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform float2 _sunpos;
uniform float4 _params;

struct v2f {
	float4	pos : POSITION;
	float2 uv : TEXCOORD0;
};

v2f vert( appdata_img v )
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	float2 uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
	o.uv = uv;
	return o;
};

half3 frag (v2f v) : COLOR
{
	half2 deltaTexCoord = (v.uv - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 16 * _params.z;
	
	float3 color = tex2D(_MainTex, v.uv);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 16; ++i)
	{
		v.uv -= deltaTexCoord;
		
		half4 sample = tex2D(_MainTex,v.uv);
		
		sample.rgb *= sample.a * 0.0625; // 1/16, the sample count
		
		sample.rgb *= illuminationDecay * _params.x;
		
		color += sample.rgb;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y;
};

ENDCG
		}
	}
	
	// ------------------ End PS3.0
	
	//-----------------------------------------------------------------

// PS2.0, eight passes (11 + (9*7) = 74 layers), for highend PS2.0 GPU's
// Secondary passes can only do seven layers due to instruction limits

	SubShader{ // PS2.0, Seperate Color (White rays only), 40 layers
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
	
	for (int i = 0; i < 8; ++i)
	{
		v.uv -= deltaTexCoord;
		
		half4 sample = tex2D(_MainTex,v.uv);
		
		sample.rgb *= illuminationDecay * _params.x * sample.a * 0.0625;
		
		color.rgb += sample.rgb;// + 1;
		
		illuminationDecay *= _params.a * _params.a;
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
	
	for (int i = 0; i < 8; ++i)
	{
		v.uv -= deltaTexCoord;
		
		half4 sample = tex2D(_MainTex,v.uv);
		
		sample.rgb *= illuminationDecay * _params.x * sample.a * 0.0625;
		
		color.rgb += sample.rgb;// + 1;
		
		illuminationDecay *= _params.a * _params.a;
	}
	
	return (color * _params.y * 0.5);
};

ENDCG

}
}
	FallBack off
}