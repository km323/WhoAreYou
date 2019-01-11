﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Transparent_AlphaMask"
{
	Properties
	{
	_MainTex("Texture", 2D) = "white" {}
	_AlphaTex("Alpha mask (R)", 2D) = "white" {}
	}
		SubShader
	{
	Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
	LOD 100
	Blend SrcAlpha OneMinusSrcAlpha
	Pass
	{
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"
	struct appdata
	{
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
	};
	struct v2f
	{
	half2 uv : TEXCOORD0;
	float4 vertex : SV_POSITION;
	};
	sampler2D _MainTex;
	sampler2D _AlphaTex;
	float4 _MainTex_ST;
	v2f vert(appdata v)
	{
	v2f o;
	o.vertex = UnityObjectToClipPos(v.vertex);
	o.uv = TRANSFORM_TEX(v.uv, _MainTex);
	return o;
	}
	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		fixed4 col = tex2D(_MainTex, i.uv);
		fixed4 col2 = tex2D(_AlphaTex, i.uv);
		return fixed4(col.r, col.g, col.b, col2.r);
		}
		ENDCG
		}
	}
}