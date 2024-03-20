using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 배경 이미지를 블러 처리하여 화면을 어둡게 표현하는 클래스입니다.
/// </summary>
public class BlurBackground : MonoBehaviour
{
    // 배경 이미지
    [Header("[ 배경 이미지 ]")]
    public Image backgroundImage; // 배경 이미지
    public float blurAmount = 2f; // 블러 정도

    private Material blurMaterial; // 블러 효과를 적용할 Material

    /// <summary>
    /// 스크립트가 시작될 때 호출되는 함수입니다.
    /// 배경 이미지에 블러 효과를 적용하기 위해 Material을 설정합니다.
    /// </summary>
    void Start()
    {
        // 이미지에 블러 효과를 적용하기 위해 Material을 복사합니다.
        blurMaterial = new Material(Shader.Find("UI/Default"));
        backgroundImage.material = blurMaterial;
    }

    /// <summary>
    /// 매 프레임마다 호출되는 함수입니다.
    /// 블러 정도를 조절합니다.
    /// </summary>
    void Update()
    {
        // 블러 정도를 조절합니다.
        blurMaterial.SetFloat("_Size", blurAmount);
    }
}
