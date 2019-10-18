// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-8027-RGB;n:type:ShaderForge.SFN_Tex2d,id:8027,x:32549,y:32808,varname:node_9461,prsc:2,tex:6bbc7ce889d6018429bfe7a264a98fdc,ntxv:0,isnm:False|UVIN-1651-UVOUT,TEX-9798-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:9798,x:32248,y:33238,ptovrint:False,ptlb:node_937,ptin:_node_937,varname:node_937,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6bbc7ce889d6018429bfe7a264a98fdc,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:5121,x:31855,y:32880,varname:node_5121,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:3058,x:32094,y:32790,varname:node_3058,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:1651,x:32341,y:32790,varname:node_1651,prsc:2,spu:1,spv:0|UVIN-3058-UVOUT,DIST-2906-OUT;n:type:ShaderForge.SFN_Slider,id:2791,x:31739,y:33041,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_1326,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:2906,x:32094,y:32987,varname:node_2906,prsc:2|A-5121-T,B-2791-OUT;proporder:9798-2791;pass:END;sub:END;*/

Shader "Shader Forge/Hyperspace_Shader" {
    Properties {
        _node_937 ("node_937", 2D) = "white" {}
        _Speed ("Speed", Range(0, 1)) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_937; uniform float4 _node_937_ST;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_5121 = _Time;
                float2 node_1651 = (i.uv0+(node_5121.g*_Speed)*float2(1,0));
                float4 node_9461 = tex2D(_node_937,TRANSFORM_TEX(node_1651, _node_937));
                float3 emissive = node_9461.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
