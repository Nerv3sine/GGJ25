Shader "Unlit/light_tst"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Hatch0  ("hatch_0", 2D) = "white" {}
        _Hatch1 ("hatch_1", 2D) = "white" {}
        _Journal ("journal", 2D) = "white" {}
        _Degree ("shader_degree", Float) = 40
        _Intensity ("intensity", Float) = 1.2
        _JournalWeight ("Journal Weight", Float) = 0.5
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
       

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 wPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _Hatch0;
            sampler2D _Hatch1;
            sampler2D _Journal;
            float _Intensity;
            float _JournalWeight;
            float _Degree;

            float2 rotationMatrix(float angle)
            {
	        	angle *= 3.1415 / 180.0;
                float sine = sin(angle);
                float cosine = cos(angle);
                return float2( cosine, -sine);
             
            }

            float random (in float2 uv) {
            return frac(
            sin(
                dot(
                    uv,
                    float2(
                        12.9898, 78.233
                    )
                
                    )
                )

            * 43758.5453);
            }

           float noise (in float2 st) {

            float2 i = floor(st);
            float2 f = frac(st);

            // Four corners in 2D of a tile
            float a = random(i);
            float b = random(i + float2(1.0, 0.0));
            float c = random(i + float2(0.0, 1.0));
            float d = random(i + float2(1.0, 1.0));

            // Smooth Interpolation

            // Cubic Hermine Curve.  Same as SmoothStep()
            float2 u = f*f*(3.0-2.0*f);
            // u = smoothstep(0.,1.,f);

            // Mix 4 coorners percentages
            return lerp(a, b, u.x) +
                    (c - a)* u.y * (1.0 - u.x) +
                    (d - b) * u.x * u.y;
            }


            fixed3 Hatching(float2 _uv, half _intensity)
            {
                half3 hatch0 = tex2D(_Hatch0, _uv).rgb;
                half3 hatch1 = tex2D(_Hatch1, _uv).rgb;

                half3 overbright = max(0, _intensity - 1.0);

              

                half3 weightsA = saturate((_intensity * 6.0) + half3(-0, -1, -2));
                half3 weightsB = saturate((_intensity * 6.0) + half3(-3, -4, -5));

                weightsA.xy -= weightsA.yz;
                weightsA.z -= weightsB.x;
                weightsB.xy -= weightsB.yz;

                hatch0 = hatch0 * weightsA;
                hatch1 = hatch1 * weightsB;

                half3 hatching = overbright + hatch0.r +
    	            hatch0.g + hatch0.b +
    	            hatch1.r + hatch1.g +
    	            hatch1.b;

                return hatching;
            
            }
            
            fixed3 SimpleHatch(float2 _uv, half _intensity)
            {
                half3 hatch0 = tex2D(_Hatch0, _uv).rgb;
                half3 hatch1 = tex2D(_Hatch1, _uv).rgb;

                return lerp(hatch0, hatch1, _intensity);
            }


      

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal( v.normal );
                o.wPos = mul( unity_ObjectToWorld, v.vertex );
                return o ;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);

                /*
                float3 N = normalize(i.normal);
                float3 L = normalize(_WorldSpaceLightPos0.xyz);
                float3 lambert = saturate( dot( N, L ) );
                float3 diffuseLight = lambert * _LightColor0.xyz;
                half intensity = dot(diffuseLight, half3(0.2326, 0.7152, 0.0722));
                */
                fixed3 diffuse = max(0.1 , dot(_WorldSpaceLightPos0, normalize(i.normal) ) );

                fixed intensity = dot(diffuse, fixed3(0.2326, 0.7152, 0.0722));
               // return float4(intensity,intensity, intensity, 1.);
              // i.uv *= rotationMatrix(_Degree);
            
               color.rgb = Hatching(i.uv * 8, intensity * _Intensity) * _LightColor0.rgb;
                //color.rgb =  lerp(CopyHatching(i.uv * 8., intensity * _Intensity), tex2D(_Journal, i.uv * 8.).xyz, _JournalWeight);
              //  float randomNum = lerp(1.0, 40.0, random(i.uv));
               // 
              //  color.rgb = lerp(Hatching(i.uv * 16., intensity * _Intensity), tex2D(_Journal, i.uv * 8.).xyz, _JournalWeight);
             
                //  color.rgb = SimpleHatch(i.uv * 20, intensity * _Intensity );
                return color;
            }
             
            ENDCG
        }
    }
}
