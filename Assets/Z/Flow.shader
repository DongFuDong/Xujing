// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-2562-OUT,clip-9907-A;n:type:ShaderForge.SFN_Tex2d,id:9907,x:32072,y:32672,ptovrint:False,ptlb:Diffiuse,ptin:_Diffiuse,varname:_Diffiuse,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:74380c8e293445d4f972846d29186917,ntxv:0,isnm:False|UVIN-3958-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3958,x:31493,y:32686,varname:node_3958,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:6202,x:30643,y:33076,varname:node_6202,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:8420,x:30907,y:33135,varname:node_8420,prsc:2,spu:1,spv:0|UVIN-6202-UVOUT,DIST-5827-OUT;n:type:ShaderForge.SFN_Panner,id:6747,x:31125,y:33122,varname:node_6747,prsc:2,spu:0,spv:1|UVIN-8420-UVOUT,DIST-2761-OUT;n:type:ShaderForge.SFN_Rotator,id:9045,x:31363,y:33132,varname:node_9045,prsc:2|UVIN-6747-UVOUT,ANG-2345-OUT;n:type:ShaderForge.SFN_Multiply,id:1723,x:31898,y:33133,varname:node_1723,prsc:2|A-301-RGB,B-4480-OUT,C-4316-RGB;n:type:ShaderForge.SFN_Slider,id:6927,x:30299,y:33583,ptovrint:False,ptlb:node_6927,ptin:_node_6927,varname:_node_6927,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_Slider,id:1187,x:30755,y:33532,ptovrint:False,ptlb:node_1187,ptin:_node_1187,varname:_node_1187,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0.2,max:5;n:type:ShaderForge.SFN_Time,id:8212,x:30447,y:33778,varname:node_8212,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5827,x:30616,y:33406,varname:node_5827,prsc:2|A-6927-OUT,B-8212-T;n:type:ShaderForge.SFN_Multiply,id:2761,x:31003,y:33398,varname:node_2761,prsc:2|A-1187-OUT,B-8212-T;n:type:ShaderForge.SFN_Tex2d,id:301,x:31575,y:33139,ptovrint:False,ptlb:node_301,ptin:_node_301,varname:_node_301,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6be09c1e2cee3b9428a73aef5b050ab9,ntxv:0,isnm:False|UVIN-9045-UVOUT;n:type:ShaderForge.SFN_Slider,id:4480,x:31701,y:33452,ptovrint:False,ptlb:node_4480,ptin:_node_4480,varname:_node_4480,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:2345,x:31223,y:33451,ptovrint:False,ptlb:Rotator,ptin:_Rotator,varname:_Rotator,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.0175823,max:1;n:type:ShaderForge.SFN_Color,id:4316,x:31947,y:33564,ptovrint:False,ptlb:Flow_light,ptin:_Flow_light,varname:_Flow_light,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.9396552,c3:0.7426471,c4:1;n:type:ShaderForge.SFN_Add,id:2562,x:32322,y:32921,varname:node_2562,prsc:2|A-9907-RGB,B-1723-OUT;proporder:9907-6927-1187-301-4480-2345-4316;pass:END;sub:END;*/

Shader "Shader Forge/Flow" {
    Properties {
        _Diffiuse ("Diffiuse", 2D) = "white" {}
        _node_6927 ("node_6927", Range(-5, 5)) = 0
        _node_1187 ("node_1187", Range(-5, 5)) = 0.2
        _node_301 ("node_301", 2D) = "white" {}
        _node_4480 ("node_4480", Range(0, 1)) = 1
        _Rotator ("Rotator", Range(-1, 1)) = 0.0175823
        _Flow_light ("Flow_light", Color) = (1,0.9396552,0.7426471,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
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
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffiuse; uniform float4 _Diffiuse_ST;
            uniform float _node_6927;
            uniform float _node_1187;
            uniform sampler2D _node_301; uniform float4 _node_301_ST;
            uniform float _node_4480;
            uniform float _Rotator;
            uniform float4 _Flow_light;
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
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _Diffiuse_var = tex2D(_Diffiuse,TRANSFORM_TEX(i.uv0, _Diffiuse));
                clip(_Diffiuse_var.a - 0.5);
////// Lighting:
////// Emissive:
                float node_9045_ang = _Rotator;
                float node_9045_spd = 1.0;
                float node_9045_cos = cos(node_9045_spd*node_9045_ang);
                float node_9045_sin = sin(node_9045_spd*node_9045_ang);
                float2 node_9045_piv = float2(0.5,0.5);
                float4 node_8212 = _Time + _TimeEditor;
                float2 node_9045 = (mul(((i.uv0+(_node_6927*node_8212.g)*float2(1,0))+(_node_1187*node_8212.g)*float2(0,1))-node_9045_piv,float2x2( node_9045_cos, -node_9045_sin, node_9045_sin, node_9045_cos))+node_9045_piv);
                float4 _node_301_var = tex2D(_node_301,TRANSFORM_TEX(node_9045, _node_301));
                float3 node_1723 = (_node_301_var.rgb*_node_4480*_Flow_light.rgb);
                float3 emissive = (_Diffiuse_var.rgb+node_1723);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Diffiuse; uniform float4 _Diffiuse_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _Diffiuse_var = tex2D(_Diffiuse,TRANSFORM_TEX(i.uv0, _Diffiuse));
                clip(_Diffiuse_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
