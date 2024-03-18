Shader "Custom/RightToLeftGradientYellow"
{
    SubShader
    {
        Tags { "RenderType"="Transparent" }  // Transparent로 변경하여 투명도를 적용할 수 있도록 합니다.
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha // 알파 블렌딩을 활성화하여 투명도를 관리합니다.

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 오른쪽에서 왼쪽으로 점차 투명해집니다.
                float alpha = 0.8 - ((1.0 - i.uv.x) * 0.8); // i.uv.x가 1에서 0으로 이동함에 따라 알파값이 0.8에서 0으로 변화합니다.
                fixed4 color = fixed4(255.0 / 255.0, 210.0 / 255.0, 87.0 / 255.0, alpha); // 노란색에 알파값을 적용합니다.
                return color;
            }
            ENDCG
        }
    }
}
