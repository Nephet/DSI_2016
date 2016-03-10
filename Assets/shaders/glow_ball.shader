// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32657,y:32734,varname:node_4013,prsc:2|diff-9230-OUT,emission-6630-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:31914,y:32306,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4598506,c2:0.1038603,c3:0.8308824,c4:1;n:type:ShaderForge.SFN_Multiply,id:5714,x:32300,y:32829,varname:node_5714,prsc:2|A-1548-RGB,B-6604-OUT,C-7349-OUT;n:type:ShaderForge.SFN_Tex2d,id:2060,x:32247,y:32263,ptovrint:False,ptlb:node_2060,ptin:_node_2060,varname:node_2060,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1aefcd576dff40547a77e7f74051378f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6604,x:32070,y:32793,varname:node_6604,prsc:2|A-7076-OUT,B-9349-RGB,C-2770-OUT;n:type:ShaderForge.SFN_Multiply,id:9230,x:32635,y:32457,varname:node_9230,prsc:2|A-2060-RGB,B-9855-OUT;n:type:ShaderForge.SFN_Vector1,id:9855,x:32336,y:32487,varname:node_9855,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Tex2d,id:9349,x:31804,y:32929,ptovrint:False,ptlb:node_9349,ptin:_node_9349,varname:node_9349,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-4386-UVOUT;n:type:ShaderForge.SFN_Power,id:7076,x:31833,y:32671,varname:node_7076,prsc:2|VAL-7934-OUT,EXP-7130-OUT;n:type:ShaderForge.SFN_OneMinus,id:7934,x:31641,y:32628,varname:node_7934,prsc:2|IN-6932-OUT;n:type:ShaderForge.SFN_Vector1,id:7130,x:31595,y:32838,varname:node_7130,prsc:2,v1:10;n:type:ShaderForge.SFN_Panner,id:4386,x:31633,y:32956,varname:node_4386,prsc:2,spu:1,spv:1|UVIN-6810-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6810,x:31425,y:32921,varname:node_6810,prsc:2,uv:0;n:type:ShaderForge.SFN_Slider,id:2770,x:31711,y:33224,ptovrint:False,ptlb:node_2770,ptin:_node_2770,varname:node_2770,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Lerp,id:6630,x:32467,y:32801,varname:node_6630,prsc:2|A-1304-RGB,B-5714-OUT,T-5714-OUT;n:type:ShaderForge.SFN_Fresnel,id:6932,x:31426,y:32615,varname:node_6932,prsc:2|NRM-7996-OUT,EXP-1010-OUT;n:type:ShaderForge.SFN_NormalVector,id:7996,x:31196,y:32586,prsc:2,pt:False;n:type:ShaderForge.SFN_Color,id:1548,x:32009,y:32517,ptovrint:False,ptlb:node_1548,ptin:_node_1548,varname:node_1548,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2308607,c2:0.8970588,c3:0.2354552,c4:1;n:type:ShaderForge.SFN_Vector1,id:7349,x:32106,y:32986,varname:node_7349,prsc:2,v1:5;n:type:ShaderForge.SFN_Vector1,id:1010,x:31288,y:32788,varname:node_1010,prsc:2,v1:6;proporder:1304-2060-9349-2770-1548;pass:END;sub:END;*/

Shader "Shader Forge/glow_ball" {
    Properties {
        _Color ("Color", Color) = (0.4598506,0.1038603,0.8308824,1)
        _node_2060 ("node_2060", 2D) = "white" {}
        _node_9349 ("node_9349", 2D) = "white" {}
        _node_2770 ("node_2770", Range(0, 1)) = 1
        _node_1548 ("node_1548", Color) = (0.2308607,0.8970588,0.2354552,1)
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
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _node_2060; uniform float4 _node_2060_ST;
            uniform sampler2D _node_9349; uniform float4 _node_9349_ST;
            uniform float _node_2770;
            uniform float4 _node_1548;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _node_2060_var = tex2D(_node_2060,TRANSFORM_TEX(i.uv0, _node_2060));
                float3 diffuseColor = (_node_2060_var.rgb*1.5);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_4926 = _Time + _TimeEditor;
                float2 node_4386 = (i.uv0+node_4926.g*float2(1,1));
                float4 _node_9349_var = tex2D(_node_9349,TRANSFORM_TEX(node_4386, _node_9349));
                float3 node_5714 = (_node_1548.rgb*(pow((1.0 - pow(1.0-max(0,dot(i.normalDir, viewDirection)),6.0)),10.0)*_node_9349_var.rgb*_node_2770)*5.0);
                float3 emissive = lerp(_Color.rgb,node_5714,node_5714);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _node_2060; uniform float4 _node_2060_ST;
            uniform sampler2D _node_9349; uniform float4 _node_9349_ST;
            uniform float _node_2770;
            uniform float4 _node_1548;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _node_2060_var = tex2D(_node_2060,TRANSFORM_TEX(i.uv0, _node_2060));
                float3 diffuseColor = (_node_2060_var.rgb*1.5);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
