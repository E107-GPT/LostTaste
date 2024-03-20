using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// TabNavigation Ŭ������ Unity UI���� Tab Ű�� �̿��� �Է� �ʵ� ���� ��ȯ �̵��� �����ϰ� �մϴ�.
/// �� Ŭ������ ��������ν�, ����ڴ� Tab Ű�� ���� ������ �Է� �ʵ� �迭(inputs) ���� ���� �ʵ�� ��Ŀ���� �̵���ų �� �ֽ��ϴ�.
/// �迭�� ������ �Է� �ʵ忡�� Tab�� ������, �迭�� ù ��° �Է� �ʵ�� ��Ŀ���� �̵��ϰ� �˴ϴ�.
/// </summary>
public class TabNavigation : MonoBehaviour
{
    // �Է� �ʵ� �迭
    [Header("�Է� �ʵ� �迭")]
    public Selectable[] inputs;

    void Update()
    {
        // Tab Ű�� ������ ���� ������ ó��
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // ���� ���õ� UI ��Ҹ� ������
            Selectable current = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
            if (current != null)
            {
                // ���� ���õ� ��Ұ� inputs �迭 ���� �ִ��� Ȯ���ϰ�, �� �ε����� ã��
                int index = System.Array.IndexOf(inputs, current);
                if (index >= 0)
                {
                    // ���� ���õ� �Է� �ʵ��� ���� �Է� �ʵ带 ���
                    // �迭�� ������ �Է� �ʵ忡�� Tab�� ������, ù ��° �Է� �ʵ�� ���ư�
                    Selectable next = inputs[(index + 1) % inputs.Length];
                    if (next != null)
                    {
                        // ���� �Է� �ʵ尡 InputField ������Ʈ�� ������ ������, ������ Ŭ�� �̺�Ʈ�� �ùķ��̼� ��
                        InputField inputfield = next.GetComponent<InputField>();
                        if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(EventSystem.current));

                        // EventSystem�� ���� ���� �Է� �ʵ�� ��Ŀ���� �̵�
                        EventSystem.current.SetSelectedGameObject(next.gameObject, new BaseEventData(EventSystem.current));
                    }
                }
            }
        }
    }
}
