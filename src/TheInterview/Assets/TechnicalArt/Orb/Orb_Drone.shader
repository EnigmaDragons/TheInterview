// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "beffio/The Hunt/Orb Drone"
{
	Properties
	{
		_Metallic("Metallic", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_Emission("Emission", 2D) = "white" {}
		_Albedo("Albedo", 2D) = "white" {}
		_FlashlightEmissionColor("Flashlight Emission Color", Color) = (0,0.462069,1,0)
		_EyeEmissionColor("Eye Emission Color", Color) = (0,0.462069,1,0)
		_ColorGreenchannelDefault("Color  Green channel (Default)", Color) = (1,0.8068966,0,0)
		_Normal("Normal", 2D) = "bump" {}
		_EyeEmissionintensity("Eye Emission intensity", Range( 0 , 100)) = 0
		_FlaslightEmissionIntensity("Flaslight Emission Intensity", Range( 0 , 100)) = 0
		_ColorBluechannel("Color Blue channel", Color) = (0,0.2965517,1,0)
		_ColorRedchannel("Color Red channel", Color) = (1,0,0,0)
		_Metallicshift("Metallic shift", Range( 0 , 2)) = 0
		_Occlusionshift("Occlusion shift", Range( 0 , 2)) = 0
		_Smoothnesshift("Smoothnes shift", Range( 0 , 2)) = 0
		_AmbientOcclusion("Ambient Occlusion", 2D) = "white" {}
		_EmissionMask("Emission Mask", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _ColorBluechannel;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float4 _ColorRedchannel;
		uniform float4 _ColorGreenchannelDefault;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform float4 _EyeEmissionColor;
		uniform float _EyeEmissionintensity;
		uniform sampler2D _EmissionMask;
		uniform float4 _EmissionMask_ST;
		uniform float4 _FlashlightEmissionColor;
		uniform float _FlaslightEmissionIntensity;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform float _Metallicshift;
		uniform float _Smoothnesshift;
		uniform sampler2D _AmbientOcclusion;
		uniform float4 _AmbientOcclusion_ST;
		uniform float _Occlusionshift;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode3 = tex2D( _Albedo, uv_Albedo );
			float4 blendOpSrc22 = _ColorBluechannel;
			float4 blendOpDest22 = tex2DNode3;
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode7 = tex2D( _Mask, uv_Mask );
			float4 lerpResult24 = lerp( tex2DNode3 , ( saturate( (( blendOpDest22 > 0.5 ) ? ( 1.0 - ( 1.0 - 2.0 * ( blendOpDest22 - 0.5 ) ) * ( 1.0 - blendOpSrc22 ) ) : ( 2.0 * blendOpDest22 * blendOpSrc22 ) ) )) , tex2DNode7.b);
			float4 blendOpSrc26 = _ColorRedchannel;
			float4 blendOpDest26 = lerpResult24;
			float4 lerpResult27 = lerp( lerpResult24 , ( saturate( ( blendOpSrc26 * blendOpDest26 ) )) , tex2DNode7.r);
			float4 blendOpSrc35 = lerpResult27;
			float4 blendOpDest35 = _ColorGreenchannelDefault;
			float4 lerpResult11 = lerp( lerpResult27 , ( saturate( (( blendOpDest35 > 0.5 ) ? ( 1.0 - ( 1.0 - 2.0 * ( blendOpDest35 - 0.5 ) ) * ( 1.0 - blendOpSrc35 ) ) : ( 2.0 * blendOpDest35 * blendOpSrc35 ) ) )) , tex2DNode7.g);
			o.Albedo = lerpResult11.rgb;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			float2 uv_EmissionMask = i.uv_texcoord * _EmissionMask_ST.xy + _EmissionMask_ST.zw;
			float4 tex2DNode36 = tex2D( _EmissionMask, uv_EmissionMask );
			float4 lerpResult37 = lerp( tex2D( _Emission, uv_Emission ) , ( _EyeEmissionColor * _EyeEmissionintensity ) , tex2DNode36.r);
			float4 lerpResult41 = lerp( lerpResult37 , ( _FlashlightEmissionColor * _FlaslightEmissionIntensity ) , tex2DNode36.b);
			o.Emission = lerpResult41.rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			float4 tex2DNode6 = tex2D( _Metallic, uv_Metallic );
			o.Metallic = ( tex2DNode6 * _Metallicshift ).r;
			o.Smoothness = ( tex2DNode6.a * _Smoothnesshift );
			float2 uv_AmbientOcclusion = i.uv_texcoord * _AmbientOcclusion_ST.xy + _AmbientOcclusion_ST.zw;
			o.Occlusion = ( tex2D( _AmbientOcclusion, uv_AmbientOcclusion ) * _Occlusionshift ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14201
8;100;1035;937;2220.643;350.2188;1;True;False
Node;AmplifyShaderEditor.ColorNode;23;-1162.74,-1120.075;Float;False;Property;_ColorBluechannel;Color Blue channel;10;0;Create;0,0.2965517,1,0;0.6985294,0.6985294,0.6985294,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1197.31,-627.6609;Float;True;Property;_Albedo;Albedo;3;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendOpsNode;22;-837.0982,-966.5164;Float;False;Overlay;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;7;-1230.591,339.3549;Float;True;Property;_Mask;Mask;1;0;Create;None;68ac0f267aa351244973b2666f7789d7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;25;-1033.259,-1462.248;Float;False;Property;_ColorRedchannel;Color Red channel;11;0;Create;1,0,0,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;24;-326.9597,-586.5532;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;26;-75.16498,-864.0659;Float;False;Multiply;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1799.613,-402.9306;Float;False;Property;_EyeEmissionintensity;Eye Emission intensity;8;0;Create;0;47.1;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;15;-1550.943,-524.391;Float;False;Property;_EyeEmissionColor;Eye Emission Color;5;0;Create;0,0.462069,1,0;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-1537.216,71.84898;Float;True;Property;_Emission;Emission;2;0;Create;None;adcf22bae980c79428146ef1248c02a2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;27;84.7689,-584.1014;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;38;-1819.999,-159.0856;Float;False;Property;_FlashlightEmissionColor;Flashlight Emission Color;4;0;Create;0,0.462069,1,0;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-758.5971,-472.8104;Float;False;Property;_ColorGreenchannelDefault;Color  Green channel (Default);6;0;Create;1,0.8068966,0,0;1,0.8068966,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;39;-1841.698,-281.6851;Float;False;Property;_FlaslightEmissionIntensity;Flaslight Emission Intensity;9;0;Create;0;47.1;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-1287.42,-335.6482;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;36;-1226.065,724.2917;Float;True;Property;_EmissionMask;Emission Mask;16;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;259.4688,-817.9962;Float;False;Property;_Smoothnesshift;Smoothnes shift;14;0;Create;0;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;274.1371,-655.4389;Float;False;Property;_Occlusionshift;Occlusion shift;13;0;Create;0;2;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;-1228.697,152.6628;Float;True;Property;_Metallic;Metallic;0;0;Create;None;80029f3af4b728d4abb2211f229f7841;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-1460.649,-132.7625;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;32;-1228.523,527.7665;Float;True;Property;_AmbientOcclusion;Ambient Occlusion;15;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;37;-945.6893,-138.2932;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;35;248.8458,-467.073;Float;False;Overlay;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;29;258.3463,-742.1841;Float;False;Property;_Metallicshift;Metallic shift;12;0;Create;0;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;498.3059,88.57674;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;499.6075,-93.97874;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;11;514.2861,-420.8474;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;41;-531.0955,-158.6373;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;16;-1183.142,-390.8418;Float;True;Property;_Normal;Normal;7;0;Create;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;430.8076,214.3546;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1632.074,-268.2644;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;beffio/The Hunt/Orb Drone;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;22;0;23;0
WireConnection;22;1;3;0
WireConnection;24;0;3;0
WireConnection;24;1;22;0
WireConnection;24;2;7;3
WireConnection;26;0;25;0
WireConnection;26;1;24;0
WireConnection;27;0;24;0
WireConnection;27;1;26;0
WireConnection;27;2;7;1
WireConnection;42;0;15;0
WireConnection;42;1;13;0
WireConnection;40;0;38;0
WireConnection;40;1;39;0
WireConnection;37;0;5;0
WireConnection;37;1;42;0
WireConnection;37;2;36;1
WireConnection;35;0;27;0
WireConnection;35;1;9;0
WireConnection;30;0;6;4
WireConnection;30;1;28;0
WireConnection;31;0;6;0
WireConnection;31;1;29;0
WireConnection;11;0;27;0
WireConnection;11;1;35;0
WireConnection;11;2;7;2
WireConnection;41;0;37;0
WireConnection;41;1;40;0
WireConnection;41;2;36;3
WireConnection;34;0;32;0
WireConnection;34;1;33;0
WireConnection;0;0;11;0
WireConnection;0;1;16;0
WireConnection;0;2;41;0
WireConnection;0;3;31;0
WireConnection;0;4;30;0
WireConnection;0;5;34;0
ASEEND*/
//CHKSM=9E923880E131BDFD078BFFEC9F495D0005FB700F