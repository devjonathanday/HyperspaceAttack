// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32845,y:32517,varname:node_2865,prsc:2|emission-8642-OUT;n:type:ShaderForge.SFN_Tex2d,id:8454,x:31969,y:32707,ptovrint:False,ptlb:texture,ptin:_texture,varname:node_8454,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f30baf4a171319a4a824750ef61405ba,ntxv:0,isnm:False|UVIN-8731-UVOUT;n:type:ShaderForge.SFN_Fresnel,id:8872,x:31953,y:32983,varname:node_8872,prsc:2|EXP-4808-OUT;n:type:ShaderForge.SFN_Multiply,id:6660,x:32205,y:32916,varname:node_6660,prsc:2|A-8454-RGB,B-8872-OUT,C-894-OUT;n:type:ShaderForge.SFN_Clamp01,id:4460,x:32362,y:32859,varname:node_4460,prsc:2|IN-6660-OUT;n:type:ShaderForge.SFN_SceneColor,id:476,x:32160,y:32494,varname:node_476,prsc:2;n:type:ShaderForge.SFN_Clamp01,id:1451,x:32322,y:32494,varname:node_1451,prsc:2|IN-476-RGB;n:type:ShaderForge.SFN_Lerp,id:8642,x:32573,y:32639,varname:node_8642,prsc:2|A-1451-OUT,B-9904-RGB,T-4460-OUT;n:type:ShaderForge.SFN_Slider,id:4808,x:31631,y:33066,ptovrint:False,ptlb:fresnel strength,ptin:_fresnelstrength,varname:node_4808,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3,max:3;n:type:ShaderForge.SFN_Slider,id:894,x:31815,y:33183,ptovrint:False,ptlb:transparency,ptin:_transparency,varname:node_894,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.908895,max:5;n:type:ShaderForge.SFN_TexCoord,id:5502,x:31385,y:32336,varname:node_5502,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:3295,x:31565,y:32529,varname:node_3295,prsc:2,spu:0,spv:1|UVIN-5502-UVOUT,DIST-4625-OUT;n:type:ShaderForge.SFN_Panner,id:8731,x:31759,y:32642,varname:node_8731,prsc:2,spu:1,spv:0|UVIN-3295-UVOUT,DIST-9540-OUT;n:type:ShaderForge.SFN_Multiply,id:4625,x:31385,y:32539,varname:node_4625,prsc:2|A-3162-T,B-2737-OUT;n:type:ShaderForge.SFN_Multiply,id:9540,x:31385,y:32683,varname:node_9540,prsc:2|A-3162-T,B-2737-OUT;n:type:ShaderForge.SFN_Time,id:3162,x:31091,y:32609,varname:node_3162,prsc:2;n:type:ShaderForge.SFN_Slider,id:2737,x:31001,y:32492,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_2737,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:0.5;n:type:ShaderForge.SFN_VertexColor,id:9904,x:32322,y:32639,varname:node_9904,prsc:2;proporder:4808-894-2737-8454;pass:END;sub:END;*/

Shader "Shader Forge/bubble_1" {
    Properties {
        _fresnelstrength ("fresnel strength", Range(0, 3)) = 3
        _transparency ("transparency", Range(0, 5)) = 1.908895
        _speed ("speed", Range(0, 0.5)) = 0.5
        _texture ("texture", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float _fresnelstrength;
            uniform float _transparency;
            uniform float _speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float4 node_3162 = _Time;
                float2 node_8731 = ((i.uv0+(node_3162.g*_speed)*float2(0,1))+(node_3162.g*_speed)*float2(1,0));
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(node_8731, _texture));
                float3 emissive = lerp(saturate(sceneColor.rgb),i.vertexColor.rgb,saturate((_texture_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnelstrength)*_transparency)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float _fresnelstrength;
            uniform float _transparency;
            uniform float _speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : SV_Target {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_3162 = _Time;
                float2 node_8731 = ((i.uv0+(node_3162.g*_speed)*float2(0,1))+(node_3162.g*_speed)*float2(1,0));
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(node_8731, _texture));
                o.Emission = lerp(saturate(sceneColor.rgb),i.vertexColor.rgb,saturate((_texture_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnelstrength)*_transparency)));
                
                float3 diffColor = float3(0,0,0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0, specColor, specularMonochrome );
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
