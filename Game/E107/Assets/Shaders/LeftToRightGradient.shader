Shader "Custom/LeftToRightGradient"
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
                // 왼쪽에서 오른쪽으로 점차 투명해집니다.
                float alpha = 0.6 - (i.uv.x * 0.6); // i.uv.x가 0에서 1로 이동함에 따라 알파값이 0.6에서 0으로 변화합니다.
                return fixed4(0, 0, 0, alpha); // 검은색에 알파값을 적용합니다.
            }
            ENDCG
        }
    }
}
