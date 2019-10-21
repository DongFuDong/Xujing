// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:True,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:35098,y:32955,varname:node_9361,prsc:2|emission-2251-OUT,custl-2251-OUT,alpha-8875-OUT;n:type:ShaderForge.SFN_Append,id:8725,x:32474,y:33037,varname:node_8725,prsc:2|A-3308-OUT,B-3422-OUT;n:type:ShaderForge.SFN_Time,id:8090,x:32091,y:32992,varname:node_8090,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:9011,x:32091,y:32910,ptovrint:False,ptlb:U_z_Speed,ptin:_U_z_Speed,varname:node_9011,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_ValueProperty,id:2742,x:32091,y:33174,ptovrint:False,ptlb:V_z_Speed,ptin:_V_z_Speed,varname:node_2742,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:3308,x:32269,y:32965,varname:node_3308,prsc:2|A-9011-OUT,B-8090-T;n:type:ShaderForge.SFN_Multiply,id:3422,x:32269,y:33116,varname:node_3422,prsc:2|A-8090-T,B-2742-OUT;n:type:ShaderForge.SFN_Add,id:1987,x:32665,y:32942,varname:node_1987,prsc:2|A-7413-UVOUT,B-8725-OUT;n:type:ShaderForge.SFN_TexCoord,id:9921,x:31068,y:33201,varname:node_9921,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Rotator,id:7413,x:31600,y:33200,varname:node_7413,prsc:2|UVIN-9921-UVOUT,ANG-4746-OUT;n:type:ShaderForge.SFN_Slider,id:9363,x:31218,y:33307,ptovrint:False,ptlb:Rotate,ptin:_Rotate,varname:node_9363,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:360;n:type:ShaderForge.SFN_Pi,id:5517,x:31408,y:33394,varname:node_5517,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9378,x:31600,y:33394,varname:node_9378,prsc:2|A-9363-OUT,B-5517-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1287,x:31375,y:33530,ptovrint:False,ptlb:00,ptin:_00,varname:node_1287,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:180;n:type:ShaderForge.SFN_Divide,id:4746,x:31818,y:33394,varname:node_4746,prsc:2|A-9378-OUT,B-1287-OUT;n:type:ShaderForge.SFN_Multiply,id:2251,x:34603,y:33047,varname:node_2251,prsc:2|A-2482-OUT,B-8576-OUT;n:type:ShaderForge.SFN_Slider,id:8576,x:34525,y:32935,ptovrint:False,ptlb:Glow ,ptin:_Glow,varname:node_8576,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:2,max:10;n:type:ShaderForge.SFN_Tex2d,id:8566,x:33533,y:32761,ptovrint:False,ptlb:01_Tex,ptin:_01_Tex,varname:node_8566,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2a0c0b8577708b943a0ad64881e80fb1,ntxv:0,isnm:False|UVIN-1987-OUT;n:type:ShaderForge.SFN_Tex2d,id:2812,x:33533,y:32953,ptovrint:False,ptlb:02_Tex,ptin:_02_Tex,varname:_02,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:203cb1da871e445498d1d44ae1b3289c,ntxv:0,isnm:False|UVIN-1987-OUT;n:type:ShaderForge.SFN_Tex2d,id:9539,x:33533,y:33154,ptovrint:False,ptlb:03_Tex,ptin:_03_Tex,varname:_03,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:76bbd1fc33d2f70489ac3d7f9cbe22d4,ntxv:0,isnm:False|UVIN-6451-OUT;n:type:ShaderForge.SFN_Tex2d,id:7221,x:33533,y:33359,ptovrint:False,ptlb:04_Tex,ptin:_04_Tex,varname:_04,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0dae363135cc28943b65d106edc63124,ntxv:0,isnm:False|UVIN-6451-OUT;n:type:ShaderForge.SFN_Multiply,id:3136,x:33898,y:32771,varname:node_3136,prsc:2|A-8566-RGB,B-5643-RGB;n:type:ShaderForge.SFN_Color,id:5643,x:33696,y:32648,ptovrint:False,ptlb:01_Color,ptin:_01_Color,varname:node_5643,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.6,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:121,x:33696,y:32871,ptovrint:False,ptlb:02_Color,ptin:_02_Color,varname:_node_5643_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.9333334,c3:0.3882353,c4:1;n:type:ShaderForge.SFN_Add,id:2482,x:34220,y:33074,varname:node_2482,prsc:2|A-3136-OUT,B-902-OUT,C-8091-OUT,D-824-OUT;n:type:ShaderForge.SFN_Multiply,id:8091,x:33898,y:33154,varname:node_8091,prsc:2|A-9539-RGB,B-6579-RGB;n:type:ShaderForge.SFN_Color,id:6579,x:33696,y:33084,ptovrint:False,ptlb:03_Color,ptin:_03_Color,varname:_node_5643_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.8196079,c3:0.3529412,c4:1;n:type:ShaderForge.SFN_Multiply,id:824,x:33898,y:33359,varname:node_824,prsc:2|A-7221-RGB,B-351-RGB;n:type:ShaderForge.SFN_Color,id:351,x:33696,y:33305,ptovrint:False,ptlb:04_Color,ptin:_04_Color,varname:_node_5643_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9764706,c2:0.6196079,c3:0.3882353,c4:1;n:type:ShaderForge.SFN_Multiply,id:902,x:33898,y:32953,varname:node_902,prsc:2|A-121-RGB,B-2812-RGB;n:type:ShaderForge.SFN_Append,id:5622,x:32491,y:33460,varname:node_5622,prsc:2|A-5822-OUT,B-7119-OUT;n:type:ShaderForge.SFN_Time,id:806,x:32108,y:33415,varname:node_806,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8065,x:32108,y:33333,ptovrint:False,ptlb:U_f_Speed,ptin:_U_f_Speed,varname:_U_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.5;n:type:ShaderForge.SFN_ValueProperty,id:1943,x:32108,y:33597,ptovrint:False,ptlb:V_f_Speed,ptin:_V_f_Speed,varname:_V_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:5822,x:32286,y:33388,varname:node_5822,prsc:2|A-8065-OUT,B-806-T;n:type:ShaderForge.SFN_Multiply,id:7119,x:32286,y:33539,varname:node_7119,prsc:2|A-806-T,B-1943-OUT;n:type:ShaderForge.SFN_Add,id:6451,x:32682,y:33365,varname:node_6451,prsc:2|A-7413-UVOUT,B-5622-OUT;n:type:ShaderForge.SFN_Add,id:69,x:33795,y:33540,varname:node_69,prsc:2|A-8566-R,B-2812-R;n:type:ShaderForge.SFN_Add,id:5768,x:33795,y:33705,varname:node_5768,prsc:2|A-9539-R,B-7221-R;n:type:ShaderForge.SFN_Add,id:8875,x:34003,y:33614,varname:node_8875,prsc:2|A-69-OUT,B-5768-OUT;n:type:ShaderForge.SFN_Add,id:40,x:34220,y:32868,varname:node_40,prsc:2|A-3136-OUT,B-902-OUT;proporder:9011-2742-8065-1943-9363-8576-8566-5643-2812-121-9539-6579-7221-351-1287;pass:END;sub:END;*/

Shader "Shader Forge/cheliu" {
    Properties {
        _U_z_Speed ("U_z_Speed", Float ) = 0.5
        _V_z_Speed ("V_z_Speed", Float ) = 0
        _U_f_Speed ("U_f_Speed", Float ) = -0.5
        _V_f_Speed ("V_f_Speed", Float ) = 0
        _Rotate ("Rotate", Range(0, 360)) = 0
        _Glow ("Glow ", Range(1, 10)) = 2
        _01_Tex ("01_Tex", 2D) = "white" {}
        _01_Color ("01_Color", Color) = (1,0.6,0,1)
        _02_Tex ("02_Tex", 2D) = "white" {}
        _02_Color ("02_Color", Color) = (1,0.9333334,0.3882353,1)
        _03_Tex ("03_Tex", 2D) = "white" {}
        _03_Color ("03_Color", Color) = (1,0.8196079,0.3529412,1)
        _04_Tex ("04_Tex", 2D) = "white" {}
        _04_Color ("04_Color", Color) = (0.9764706,0.6196079,0.3882353,1)
        _00 ("00", Float ) = 180
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            AlphaToMask On
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _U_z_Speed;
            uniform float _V_z_Speed;
            uniform float _Rotate;
            uniform float _00;
            uniform float _Glow;
            uniform sampler2D _01_Tex; uniform float4 _01_Tex_ST;
            uniform sampler2D _02_Tex; uniform float4 _02_Tex_ST;
            uniform sampler2D _03_Tex; uniform float4 _03_Tex_ST;
            uniform sampler2D _04_Tex; uniform float4 _04_Tex_ST;
            uniform float4 _01_Color;
            uniform float4 _02_Color;
            uniform float4 _03_Color;
            uniform float4 _04_Color;
            uniform float _U_f_Speed;
            uniform float _V_f_Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_7413_ang = ((_Rotate*3.141592654)/_00);
                float node_7413_spd = 1.0;
                float node_7413_cos = cos(node_7413_spd*node_7413_ang);
                float node_7413_sin = sin(node_7413_spd*node_7413_ang);
                float2 node_7413_piv = float2(0.5,0.5);
                float2 node_7413 = (mul(i.uv0-node_7413_piv,float2x2( node_7413_cos, -node_7413_sin, node_7413_sin, node_7413_cos))+node_7413_piv);
                float4 node_8090 = _Time + _TimeEditor;
                float2 node_1987 = (node_7413+float2((_U_z_Speed*node_8090.g),(node_8090.g*_V_z_Speed)));
                float4 _01_Tex_var = tex2D(_01_Tex,TRANSFORM_TEX(node_1987, _01_Tex));
                float3 node_3136 = (_01_Tex_var.rgb*_01_Color.rgb);
                float4 _02_Tex_var = tex2D(_02_Tex,TRANSFORM_TEX(node_1987, _02_Tex));
                float3 node_902 = (_02_Color.rgb*_02_Tex_var.rgb);
                float4 node_806 = _Time + _TimeEditor;
                float2 node_6451 = (node_7413+float2((_U_f_Speed*node_806.g),(node_806.g*_V_f_Speed)));
                float4 _03_Tex_var = tex2D(_03_Tex,TRANSFORM_TEX(node_6451, _03_Tex));
                float4 _04_Tex_var = tex2D(_04_Tex,TRANSFORM_TEX(node_6451, _04_Tex));
                float3 node_2251 = ((node_3136+node_902+(_03_Tex_var.rgb*_03_Color.rgb)+(_04_Tex_var.rgb*_04_Color.rgb))*_Glow);
                float3 emissive = node_2251;
                float3 finalColor = emissive + node_2251;
                fixed4 finalRGBA = fixed4(finalColor,((_01_Tex_var.r+_02_Tex_var.r)+(_03_Tex_var.r+_04_Tex_var.r)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
