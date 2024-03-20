using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��� �̹����� �� ó���Ͽ� ȭ���� ��Ӱ� ǥ���ϴ� Ŭ�����Դϴ�.
/// </summary>
public class BlurBackground : MonoBehaviour
{
    // ��� �̹���
    [Header("[ ��� �̹��� ]")]
    public Image backgroundImage; // ��� �̹���
    public float blurAmount = 2f; // �� ����

    private Material blurMaterial; // �� ȿ���� ������ Material

    /// <summary>
    /// ��ũ��Ʈ�� ���۵� �� ȣ��Ǵ� �Լ��Դϴ�.
    /// ��� �̹����� �� ȿ���� �����ϱ� ���� Material�� �����մϴ�.
    /// </summary>
    void Start()
    {
        // �̹����� �� ȿ���� �����ϱ� ���� Material�� �����մϴ�.
        blurMaterial = new Material(Shader.Find("UI/Default"));
        backgroundImage.material = blurMaterial;
    }

    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǵ� �Լ��Դϴ�.
    /// �� ������ �����մϴ�.
    /// </summary>
    void Update()
    {
        // �� ������ �����մϴ�.
        blurMaterial.SetFloat("_Size", blurAmount);
    }
}
