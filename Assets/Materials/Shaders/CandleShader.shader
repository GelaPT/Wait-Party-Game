Shader "Unlit/CandleShader"
{
    Properties
    {
        _Color1 ("Color1", Color) = (0.2,0.2,1,1)
        _Color2 ("Color2", Color) = (1,0,0,1)
        _FresnelBias ("Fresnel Bias", Float) = 0
		_FresnelScale ("Fresnel Scale", Range(-1,0)) = 0.5
		_FresnelPower ("Fresnel Power", Range(1,10)) = 1
    }
    SubShader
    {
        Tags {
        	"RenderType"="Opaque"
        	"Queue" = "Transparent"
        	}
        LOD 100

        Pass
        {
        	//Cull Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
        	
            CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
				half3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
				float fresnel : TEXCOORD1;
			};

            float4 _Color1;
            float4 _Color2;
            fixed _FresnelBias;
			fixed _FresnelScale;
			fixed _FresnelPower;

            v2f vert (appdata_t v)
            {
                v2f o;
				o.pos = UnityObjectToClipPos(v.pos);

				float3 i = normalize(ObjSpaceViewDir(v.pos));
				o.fresnel = _FresnelBias + _FresnelScale * pow(1 + dot(i, v.normal), _FresnelPower);
				return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color1;
            	
                return lerp(col, _Color2, 1 - i.fresnel);
            }
            ENDCG
        }
    }
}
