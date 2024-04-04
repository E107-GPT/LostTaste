using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// UI ������Ʈ�� ���콺 Ŀ���� �����ų� ���� ��, �Ǵ� Ŭ������ �� Ŀ�� ����� �����ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler // IPointerClickHandler �������̽� �߰�
{
    [Header("[ Ŀ�� �̹��� ]")]
    public Texture2D fingerCursor; // �հ��� ����� Ŀ�� �̹���

    // ���콺�� UI ������Ʈ�� �� �� ȣ��Ǵ� �޼���
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(fingerCursor, Vector2.zero, CursorMode.Auto); // ���콺 Ŀ���� �հ��� ������� ����
    }

    // ���콺�� UI ������Ʈ���� ���� �� ȣ��Ǵ� �޼���
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // ���콺 Ŀ���� �⺻ ������� ����
    }

    // UI ������Ʈ�� Ŭ������ �� ȣ��Ǵ� �޼���
    public void OnPointerClick(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // ���콺 Ŀ���� �⺻ ������� ����
    }
}
