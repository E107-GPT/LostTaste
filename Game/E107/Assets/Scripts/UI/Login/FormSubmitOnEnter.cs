using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// FormSubmitOnEnter Ŭ������ Unity UI ������ Enter Ű�� ������ ������
/// ������ ��ư�� Ŭ�� �̺�Ʈ�� �߻���Ű�� ����� �����մϴ�.
/// </summary>
public class FormSubmitOnEnter : MonoBehaviour
{
    // ��ư
    [Header("[ ��ư ]")]
    public Button submitButton; // ����ڰ� Enter�� ������ �� Ŭ���Ǿ�� �� ��ư

    void Update()
    {
        // ����ڰ� Enter Ű�� �������� Ȯ��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // ������ ��ư�� onClick �̺�Ʈ�� ȣ��
            submitButton.onClick.Invoke();
        }
    }
}
