using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D fingerCursor; // �հ��� ����� Ŀ�� �̹���

    // ���콺�� UI ������Ʈ�� �� �� ȣ��Ǵ� �Լ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(fingerCursor, Vector2.zero, CursorMode.Auto); // ���콺 Ŀ���� �հ��� ������� ����
    }

    // ���콺�� UI ������Ʈ���� ���� �� ȣ��Ǵ� �Լ�
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // ���콺 Ŀ���� �⺻ ������� ����
    }
}
