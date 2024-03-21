using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �˾� â�� Ȱ��ȭ �ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class OpenPopupWindow : MonoBehaviour
{
    // ��ư
    [Header("[ ��ư ]")]
    public Button openButton;

    // �˾� â
    [Header("[ �˾� â ]")]
    public GameObject popupWindow;

    // ��ũ��Ʈ�� Ȱ��ȭ�Ǿ��� �� ȣ��Ǵ� �޼���
    private void Awake()
    {
        // ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        if (openButton != null)
            this.openButton.onClick.AddListener(HandleOpenButtonClick);
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void HandleOpenButtonClick()
    {
        // ���� Ȯ�� â Ȱ��ȭ
        popupWindow.SetActive(true);
    }
}
