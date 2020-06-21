// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "beffio/The Hunt/Environment_Chunks"
{
	Properties
	{
		_Colorshift("Color shift", Color) = (0.6764706,0.6764706,0.6764706,0)
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Detail_Normal("Detail_Normal", 2D) = "bump" {}
		_Occlusion("Occlusion", 2D) = "white" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothnesshift("Smoothnes shift", Range( 0 , 2)) = 1
		_Occlusionshift("Occlusion shift", Range( 0 , 2)) = 1
		[HideInInspector]_TextureSample0("Texture Sample 0", 2D) = "bump" {}
		_Ripplesintensity("Ripples intensity", Range( 0 , 10)) = 0
		_RipplesSpeed("Ripples Speed", Range( 0 , 40)) = 20
		_RipplesTiling("Ripples Tiling", Range( 0 , 50)) = 10
		_Height("Height", 2D) = "white" {}
		_Heightpower("Height power", Range( 0 , 0.08)) = 0
		_Offset("Offset", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 viewDir;
			INTERNAL_DATA
		};

		uniform sampler2D _Normal;
		uniform float _Offset;
		uniform sampler2D _Height;
		uniform float4 _Height_ST;
		uniform float _Heightpower;
		uniform sampler2D _Detail_Normal;
		uniform float _Ripplesintensity;
		uniform sampler2D _TextureSample0;
		uniform float _RipplesTiling;
		uniform float _RipplesSpeed;
		uniform sampler2D _Albedo;
		uniform float _Smoothnesshift;
		uniform float4 _Colorshift;
		uniform float _Metallic;
		uniform sampler2D _Occlusion;
		uniform float _Occlusionshift;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_cast_0 = (_Offset).xx;
			float2 uv_TexCoord39 = i.uv_texcoord + temp_cast_0;
			float2 uv_Height = i.uv_texcoord * _Height_ST.xy + _Height_ST.zw;
			float2 Offset34 = ( ( tex2D( _Height, uv_Height ).r - 1 ) * i.viewDir.xy * _Heightpower ) + uv_TexCoord39;
			float2 temp_cast_3 = (_RipplesTiling).xx;
			float2 uv_TexCoord27 = i.uv_texcoord * temp_cast_3;
			float2 appendResult23 = (float2(frac( uv_TexCoord27.x ) , frac( uv_TexCoord27.y )));
			// *** BEGIN Flipbook UV Animation vars ***
			// Total tiles of Flipbook Texture
			float fbtotaltiles22 = 4.0 * 4.0;
			// Offsets for cols and rows of Flipbook Texture
			float fbcolsoffset22 = 1.0f / 4.0;
			float fbrowsoffset22 = 1.0f / 4.0;
			// Speed of animation
			float fbspeed22 = _Time[ 1 ] * _RipplesSpeed;
			// UV Tiling (col and row offset)
			float2 fbtiling22 = float2(fbcolsoffset22, fbrowsoffset22);
			// UV Offset - calculate current tile linear index, and convert it to (X * coloffset, Y * rowoffset)
			// Calculate current tile linear index
			float fbcurrenttileindex22 = round( fmod( fbspeed22 + 0.0, fbtotaltiles22) );
			fbcurrenttileindex22 += ( fbcurrenttileindex22 < 0) ? fbtotaltiles22 : 0;
			// Obtain Offset X coordinate from current tile linear index
			float fblinearindextox22 = round ( fmod ( fbcurrenttileindex22, 4.0 ) );
			// Multiply Offset X by coloffset
			float fboffsetx22 = fblinearindextox22 * fbcolsoffset22;
			// Obtain Offset Y coordinate from current tile linear index
			float fblinearindextoy22 = round( fmod( ( fbcurrenttileindex22 - fblinearindextox22 ) / 4.0, 4.0 ) );
			// Reverse Y to get tiles from Top to Bottom
			fblinearindextoy22 = (int)(4.0-1) - fblinearindextoy22;
			// Multiply Offset Y by rowoffset
			float fboffsety22 = fblinearindextoy22 * fbrowsoffset22;
			// UV Offset
			float2 fboffset22 = float2(fboffsetx22, fboffsety22);
			// Flipbook UV
			half2 fbuv22 = appendResult23 * fbtiling22 + fboffset22;
			// *** END Flipbook UV Animation vars ***
			float4 tex2DNode2 = tex2D( _Albedo, Offset34 );
			float temp_output_10_0 = ( tex2DNode2.a * _Smoothnesshift );
			float4 lerpResult15 = lerp( float4(0.0078125,0,1,1) , float4( UnpackScaleNormal( tex2D( _TextureSample0, fbuv22 ), _Ripplesintensity ) , 0.0 ) , step( step( temp_output_10_0 , 0.7 ) , 0.0 ));
			o.Normal = BlendNormals( BlendNormals( UnpackNormal( tex2D( _Normal, Offset34 ) ) , UnpackNormal( tex2D( _Detail_Normal, Offset34 ) ) ) , lerpResult15.rgb );
			float4 blendOpSrc8 = tex2DNode2;
			float4 blendOpDest8 = _Colorshift;
			o.Albedo = ( saturate( (( blendOpDest8 > 0.5 ) ? ( 1.0 - ( 1.0 - 2.0 * ( blendOpDest8 - 0.5 ) ) * ( 1.0 - blendOpSrc8 ) ) : ( 2.0 * blendOpDest8 * blendOpSrc8 ) ) )).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = temp_output_10_0;
			o.Occlusion = ( tex2D( _Occlusion, Offset34 ) * _Occlusionshift ).r;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = IN.tSpace0.xyz * worldViewDir.x + IN.tSpace1.xyz * worldViewDir.y + IN.tSpace2.xyz * worldViewDir.z;
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
0;92;1593;1001;2726.154;959.2198;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;29;-2101.274,753.0887;Float;False;1970.602;602.7297;Ripples;14;14;15;21;20;17;18;19;22;26;27;25;23;24;28;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-2258.154,-797.2198;Float;False;Property;_Offset;Offset;15;0;Create;True;0;0;False;0;0;0.874;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;38;-1842.836,-157.1927;Float;False;Tangent;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;36;-1830.672,-289.3316;Float;False;Property;_Heightpower;Height power;14;0;Create;True;0;0;False;0;0;0;0;0.08;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;39;-1887.399,-671.7695;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;35;-2374.468,-519.6731;Float;True;Property;_Height;Height;13;0;Create;True;0;0;False;0;None;2ac1662f45acc834bbdf860dd5410637;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;-2079.274,842.1219;Float;False;Property;_RipplesTiling;Ripples Tiling;12;0;Create;True;0;0;False;0;10;7.7;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;27;-1679.251,820.2131;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ParallaxMappingNode;34;-1450.787,-463.1527;Float;False;Normal;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-675.7043,328.8761;Float;False;Property;_Smoothnesshift;Smoothnes shift;7;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;25;-1660.092,1022.193;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1037.568,-645.0933;Float;True;Property;_Albedo;Albedo;2;0;Create;True;0;0;False;0;None;8c73d683073d71b479b87bfd35d1334d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FractNode;26;-1511.511,1024.877;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-257.6682,-183.0609;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;23;-1497.521,1116.866;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1638.755,1240.818;Float;False;Property;_RipplesSpeed;Ripples Speed;11;0;Create;True;0;0;False;0;20;17;0;40;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-629.8044,1121.622;Float;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;False;0;0.7;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;18;-437.9332,1096.743;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1264.281,825.4159;Float;False;Property;_Ripplesintensity;Ripples intensity;10;0;Create;True;0;0;False;0;0;1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCFlipBookUVAnimation;22;-1287.563,1035.993;Float;False;0;0;6;0;FLOAT2;0,0;False;1;FLOAT;4;False;2;FLOAT;4;False;3;FLOAT;20;False;4;FLOAT;0;False;5;FLOAT;0;False;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.StepOpNode;19;-284.6724,1092.824;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-888.2172,803.0886;Float;False;Constant;_Color0;Color 0;5;0;Create;True;0;0;False;0;0.0078125,0,1,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;-1053.272,-15.54615;Float;True;Property;_Detail_Normal;Detail_Normal;4;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;20;-949.5101,1004.599;Float;True;Property;_TextureSample0;Texture Sample 0;9;1;[HideInInspector];Create;True;0;0;False;0;26a9a3ecb7498764798bb2105a8e4f94;26a9a3ecb7498764798bb2105a8e4f94;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1045.72,-444.3258;Float;True;Property;_Normal;Normal;3;0;Create;True;0;0;False;0;None;16a3685128bdb40428650f064d3da9ae;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-450.9292,-702.4435;Float;False;Property;_Colorshift;Color shift;1;0;Create;True;0;0;False;0;0.6764706,0.6764706,0.6764706,0;0.6764706,0.6764706,0.6764706,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-1056.867,-226.7417;Float;True;Property;_Occlusion;Occlusion;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;15;-349.1849,815.3324;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendNormalsNode;12;-601.1428,-11.04312;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-916.7853,228.7958;Float;False;Property;_Occlusionshift;Occlusion shift;8;0;Create;True;0;0;False;0;1;1.35;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;8;-158.3747,-364.1271;Float;False;Overlay;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendNormalsNode;16;-212.2525,-8.759627;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TriplanarNode;30;704.8401,-358.7575;Float;True;Spherical;World;False;Top Texture 0;_TopTexture0;white;0;None;Mid Texture 0;_MidTexture0;white;-1;None;Bot Texture 0;_BotTexture0;white;-1;None;Triplanar Sampler;False;9;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;8;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-550.2913,209.1951;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-731.9822,464.5866;Float;False;Property;_Metallic;Metallic;6;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;149.921,-29.18816;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;beffio/The Hunt/Environment_Chunks;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;39;1;41;0
WireConnection;27;0;28;0
WireConnection;34;0;39;0
WireConnection;34;1;35;0
WireConnection;34;2;36;0
WireConnection;34;3;38;0
WireConnection;25;0;27;2
WireConnection;2;1;34;0
WireConnection;26;0;27;1
WireConnection;10;0;2;4
WireConnection;10;1;9;0
WireConnection;23;0;26;0
WireConnection;23;1;25;0
WireConnection;18;0;10;0
WireConnection;18;1;17;0
WireConnection;22;0;23;0
WireConnection;22;3;24;0
WireConnection;19;0;18;0
WireConnection;13;1;34;0
WireConnection;20;1;22;0
WireConnection;20;5;21;0
WireConnection;3;1;34;0
WireConnection;4;1;34;0
WireConnection;15;0;14;0
WireConnection;15;1;20;0
WireConnection;15;2;19;0
WireConnection;12;0;3;0
WireConnection;12;1;13;0
WireConnection;8;0;2;0
WireConnection;8;1;7;0
WireConnection;16;0;12;0
WireConnection;16;1;15;0
WireConnection;31;0;4;0
WireConnection;31;1;32;0
WireConnection;0;0;8;0
WireConnection;0;1;16;0
WireConnection;0;3;11;0
WireConnection;0;4;10;0
WireConnection;0;5;31;0
ASEEND*/
//CHKSM=2D1A403F843AD045D891EC64D008298FEEF59516