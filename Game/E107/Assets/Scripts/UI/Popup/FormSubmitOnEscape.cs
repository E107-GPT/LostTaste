using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// FormSubmitOnEscape Ŭ������ Unity UI ������ Esc Ű�� ������ ������
/// ������ ��ư�� Ŭ�� �̺�Ʈ�� �߻���Ű�� ����� �����մϴ�.
/// </summary>
public class FormSubmitOnEscape : MonoBehaviour
{
    [Header("[ ��ư ]")]
    public Button submitButton; // ����ڰ� Esc�� ������ �� Ŭ���Ǿ�� �� ��ư

    void Update()
    {
        // ����ڰ� Esc Ű�� �������� Ȯ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ������ ��ư�� onClick �̺�Ʈ�� ȣ��
            submitButton.onClick.Invoke();
        }
    }
}
