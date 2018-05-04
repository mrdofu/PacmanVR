﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

Shader "UsTwo-Cardboard/Transparent Color Overlay Blackout Enviro" {

Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_Environment ("Environment", Float) = 0
	//_MainTex ("Main Texture", 2D) = "" {}
}

Category {
	Tags { "Queue"="Overlay +1" "IgnoreProjector"="True" "RenderType"="Transparent" }

	Blend SrcAlpha OneMinusSrcAlpha
	AlphaTest Off
	Cull Off 
	Lighting Off 
	ZWrite Off 
	ZTest Always

	Fog { Mode Off }
	
	SubShader {
		Pass {
			
			CGPROGRAM
			#pragma vertex VertexProgram
			#pragma fragment FragmentProgram
			#include "CardboardDistortion.cginc"


			struct VertexInput {
			    half4 position : POSITION;
			    //half2 texcoord : TEXCOORD0;
			    //half3 normal : NORMAL;
			};

			struct VertexToFragment {
			    half4 position : SV_POSITION;

			    half4 fog: TEXCOORD0;

			};



			half4 _ZenithColor;
			half4 _ZenithFog;

			half4 _HorizonColor;
			half4 _HorizonFog;
			half4 _FogData;
			half4 _DirectionalData;
			half4 _DirectionalColor;
			half4 _DirectionalFog;
			half4 _Color;
			half _Environment;
			VertexToFragment VertexProgram (VertexInput vertex)
			{
			    VertexToFragment output;
			    //output.position = undistort(vertex.position);
			    output.position = mul (UNITY_MATRIX_MVP, vertex.position);

			    half4 worldPosition = mul (unity_ObjectToWorld,vertex.position);
			    half3 pointVector = worldPosition.xyz - _WorldSpaceCameraPos;
			    half distanceToCamera = length(pointVector);
			    half3 normVector = pointVector / (distanceToCamera+0.0001);

			    ///output.position.z
			    
			    half fogVertical = saturate(normVector.y);

			    ///We can skip the normalize here
				half fogAngle = _DirectionalData.x * saturate((dot(_DirectionalData.zw,normalize(normVector.xz) )));//(atan2(normVector.z, normVector.x) / (2 * 3.1415926) ) + 0.5;	

				half3 horizonColor =( (1-fogAngle)*_HorizonColor.rgb + fogAngle*_DirectionalColor) ;	   	
			   	half3 fogColor = (1-fogVertical)*horizonColor + fogVertical*_ZenithColor.rgb;
			   	//half verticalBlend = smoothstep(_FogData.z, _FogData.w, fogVertical);
			    //half3 fogColor = (1-verticalBlend)*_HorizonColor.rgb + verticalBlend*_ZenithColor.rgb;
			    //half3 groundColor = fogAngle*_FogForwardColor.rgb + (1.0-fogAngle) * _FogBackColor.rgb;
			    //half3 fogColor = fogVertical*_FogSkyColor.rgb + (1.0-fogVertical)*groundColor;

			    output.fog = half4((1-_Environment)*_Color.rgb + _Environment*fogColor.rgb,_Color.a);

			    return output;
			};

			



			half4 FragmentProgram (VertexToFragment fragment) : COLOR
			{  

				return fragment.fog;
			}
			ENDCG
		}
	}
	
}
}
