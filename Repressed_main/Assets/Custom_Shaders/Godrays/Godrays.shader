// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'
// Upgrade NOTE: replaced 'glstate.matrix.texture[0]' with 'UNITY_MATRIX_TEXTURE0'
// Upgrade NOTE: replaced 'samplerRECT' with 'sampler2D'
// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

Shader "Godrays" {
	Properties {
		_MainTex ("Base (RGB)", RECT) = "white" {}
		//_weight ("Weight", float) = 1.0
		//_exposure ("Exposure",float) = 1.0
		_sunpos ("Sun Pos (Automatic)", vector) = (0.5,0.5,0.0,0.0)
		//_density ("Density", float) = 1.0
		//_decay ("decay", float) = 1.0
		_params ("Weight Exposure Density Decay", vector) = (0.2,0.9,0.5,0.9)
	}
	
	
	SubShader { // PS3.0, Three Passes (96 layers)
		LOD 1000
		
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		
pass
{			
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
//uniform float4 _MainTex_TexelSize;
//uniform float _weight;
//uniform float _exposure;
uniform float4 _sunpos;
//uniform float _density;
//uniform float _decay;
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

half3 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 32 * _params.z;
	
	float3 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 32; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.333;
};

ENDCG
		}
		
pass
{
	blend one one			
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
//uniform float4 _MainTex_TexelSize;
//uniform float _weight;
//uniform float _exposure;
uniform float4 _sunpos;
//uniform float _density;
//uniform float _decay;
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

half3 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 32 * _params.z;
	
	float3 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 32; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord - (deltaTexCoord * 0.3)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.333;
};

ENDCG
		}

pass
{
	blend one one			
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
//uniform float4 _MainTex_TexelSize;
//uniform float _weight;
//uniform float _exposure;
uniform float4 _sunpos;
//uniform float _density;
//uniform float _decay;
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

half3 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 32 * _params.z;
	
	float3 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 32; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord - (deltaTexCoord * 0.6)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.334;
};

ENDCG
		}		

	}
// ------------------------- End PS3.0 Three pass
	
	SubShader { // PS3.0, Two Passes, 64 layers
		LOD 800
		
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		
pass
{			
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
//uniform float4 _MainTex_TexelSize;
//uniform float _weight;
//uniform float _exposure;
uniform float4 _sunpos;
//uniform float _density;
//uniform float _decay;
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

half3 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 32 * _params.z;
	
	float3 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 32; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.5;
};

ENDCG
		}
		
pass
{
	blend one one			
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
//uniform float4 _MainTex_TexelSize;
//uniform float _weight;
//uniform float _exposure;
uniform float4 _sunpos;
//uniform float _density;
//uniform float _decay;
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

half3 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 32 * _params.z;
	
	float3 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 32; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord - (deltaTexCoord * 0.5)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.5;
};

ENDCG
		}
	}
// -------------------------------------------- End twopass PS3.0
	
	SubShader { // PS3.0, Single pass, 32 layers
		LOD 500
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
//uniform float4 _MainTex_TexelSize;
//uniform float _weight;
//uniform float _exposure;
uniform float4 _sunpos;
//uniform float _density;
//uniform float _decay;
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

half3 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 32 * _params.z;
	
	float3 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 32; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y;
};

ENDCG
		}
	}
// ------------ End of PS3.0

//-----------------------------------------------------------------

// PS2.0, eight passes (11 + (9*7) = 74 layers), for highend PS2.0 GPU's
// Secondary passes can only do seven layers due to instruction limits

SubShader{ // PS2.0, 8 passes
LOD 900
pass // First pass, 11 layers
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 11 * _params.z;
	
	float4 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 11; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.125;
};

ENDCG

}

pass // Second pass, 7 layers
{
Blend one one
	
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 9 * _params.z;
	
	float4 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 9; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord - (deltaTexCoord * 0.125)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.125;
};

ENDCG

}

pass // Third pass, 7 layers
{
	
Blend one one	
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 9 * _params.z;
	
	float4 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 9; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord - (deltaTexCoord * 0.25)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.125;
};

ENDCG

}

pass // Fourth pass, 7 layers
{
	
Blend one one
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 9 * _params.z;
	
	float4 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 9; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord - (deltaTexCoord * 0.375)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.125;
};

ENDCG

}

pass // Fifth pass, 7 layers
{
	
Blend one one
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 9 * _params.z;
	
	float4 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 9; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord - (deltaTexCoord * 0.5)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.125;
};

ENDCG

}

pass // Fifth pass, 7 layers
{
	
Blend one one
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 9 * _params.z;
	
	float4 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 9; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord - (deltaTexCoord * 0.625)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.125;
};

ENDCG

}

pass // Seventh pass, 7 layers
{
	
Blend one one
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 9 * _params.z;
	
	float4 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 9; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord - (deltaTexCoord * 0.75)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.125;
};

ENDCG

}

pass // Eighth pass, 7 layers
{
	
Blend one one
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 9 * _params.z;
	
	float4 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 9; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord - (deltaTexCoord * 0.875)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.125;
};

ENDCG

}
	
}
// ------------------- End of PS2.0 eightpass

SubShader{ // PS2.0, 1 pass

pass // First pass, 11 layers
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 11 * _params.z;
	
	float4 color = tex2D(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 11; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y;
};

ENDCG

}
	
}
	
	SubShader{ // PS2.0, Seperate Color (White rays only), 40 layers
	LOD 300
pass // First pass, 10 layers
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 10 * _params.z;
	
	float4 color;
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 10; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord + (deltaTexCoord * 0.25)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color += sample;// + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.0625;
};

ENDCG

}

pass // Second pass, 10 layers
{
Blend One One
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 10 * _params.z;
	
	float4 color;
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 10; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord + (deltaTexCoord * 0.5)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color += sample;// + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.0625;
};

ENDCG

}

pass // Third pass, 10 layers
{
Blend One One
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 10 * _params.z;
	
	float4 color;
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 10; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord + (deltaTexCoord * 0.75)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color += sample;// + 1;
		
		illuminationDecay *= _params.a;
	}
	
	return color * _params.y * 0.0625;
};

ENDCG

}

pass // Final pass, 10 layers, + color
{
Blend One One
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
float4 frag (v2f i) : COLOR
{
	half2 texCoord = i.uv;
	
	half2 deltaTexCoord = (texCoord - _sunpos.xy);
	
	deltaTexCoord *= 1.0f / 10 * _params.z;
	
	float4 color; //= texRECT(_MainTex, texCoord);
	
	half illuminationDecay = 1.0f;
	
	for (int i = 0; i < 10; ++i)
	{
		texCoord -= deltaTexCoord;
		
		half sample = tex2D(_MainTex,texCoord + (deltaTexCoord * 0.5)).a;
		
		sample *= illuminationDecay * _params.x;
		
		color *= sample; // Factors in prior layer luminance
		
		illuminationDecay *= _params.a;
	}
	
	return (color * _params.y) + tex2D(_MainTex, i.uv);
};

ENDCG

}

}
 //-----------------------------------------------------------------
	FallBack off
}
