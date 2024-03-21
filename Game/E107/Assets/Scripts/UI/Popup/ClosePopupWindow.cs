using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �˾� â�� ��Ȱ��ȭ �ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class ClosePopupWindow : MonoBehaviour
{
    // ��ư
    [Header("[ ��ư ]")]
    public Button closeButton;

    // �˾� â
    [Header("[ �˾� â ]")]
    public GameObject popupWindow;

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
        // ���� Ȯ�� â Ȱ��ȭ
        popupWindow.SetActive(false);
    }
}
