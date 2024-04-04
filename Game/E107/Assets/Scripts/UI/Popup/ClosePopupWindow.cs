using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �˾� â�� ��Ȱ��ȭ�ϰ� ������ �Է� �ʵ带 �ʱ�ȭ�ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class ClosePopupWindow : MonoBehaviour
{
    // ��ư
    [Header("[ ��ư ]")]
    public Button closeButton;

    // �˾� â
    [Header("[ �˾� â ]")]
    public GameObject popupWindow;

    // �Է� �ʵ� �迭
    [Header("[ �Է� �ʵ� �迭 ]")]
    public InputField[] inputs;

    // ��ũ��Ʈ�� Ȱ��ȭ�Ǿ��� �� ȣ��Ǵ� �޼���
    private void Awake()
    {
        // ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        if (closeButton != null)
            this.closeButton.onClick.AddListener(HandleCloseButtonClick);
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void HandleCloseButtonClick()
    {
        // ������ �Է� �ʵ带 �ʱ�ȭ
        foreach (var input in inputs)
        {
            if (input != null) // �Է� �ʵ尡 �����Ǿ����� Ȯ��
                input.text = "";
        }

        // �˾� â ��Ȱ��ȭ
        popupWindow.SetActive(false);
    }
}
