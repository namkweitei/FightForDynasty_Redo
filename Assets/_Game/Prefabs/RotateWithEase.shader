Shader "Custom/RotateWithTexture"{
    Properties{
        [PerRendererData]_MainTex ("Main Texture", 2D) = "white" {}
        _ScreenPos0("Screen Position 0", Range(-5, 5)) = 0.0
        _ScreenPos1("Screen Position 1", Range(-5, 5)) = 5.0
        _ScreenPos2("Screen Position 2", Range(-5, 5)) = 0.5
        _ScreenPos3("Screen Position 3", Range(-5, 5)) = 2.0
        _RotationAngle ("Rotation Angle", Range(0, 360)) = 15
        _Speed("Speed", Range(0.1, 1)) = 0.5
    }
    SubShader{
        Tags { "Queue" = "Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _RotationAngle;
            sampler2D _MainTex;
            float _Speed;
            float _ScreenPos0;
            float _ScreenPos1;
            float _ScreenPos2;
            float _ScreenPos3;


            v2f vert (appdata_t v){
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target{
                // Calculate the screen space coordinates of the vertex
                float4 screenPos = UnityObjectToClipPos(i.vertex);
                
                // Check if the vertex is outside the screen boundaries
                if (screenPos.x < _ScreenPos0 || screenPos.x > _ScreenPos1 || screenPos.y < _ScreenPos2 || screenPos.y > _ScreenPos3){
                        discard;
                }

                // Calculate the rotation using easeInOutSine
                float t = _Time.y * _Speed * 0.1;
                float angle = _RotationAngle * t;
                float sine = sin(angle);
                float cosine = cos(angle);

                // Rotate the UV coordinates
                float2 rotatedUV = float2(cosine * (i.uv.x - 0.5) - sine * (i.uv.y - 0.5) + 0.5, sine * (i.uv.x - 0.5) + cosine * (i.uv.y - 0.5) + 0.5);

                // Sample the texture using the rotated UV coordinates
                half4 texColor = tex2D(_MainTex, rotatedUV);
                return texColor;
            }
            ENDCG
        }
    }
}
