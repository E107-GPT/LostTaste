using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D fingerCursor; // 손가락 모양의 커서 이미지

    // 마우스가 UI 오브젝트에 들어갈 때 호출되는 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(fingerCursor, Vector2.zero, CursorMode.Auto); // 마우스 커서를 손가락 모양으로 변경
    }

    // 마우스가 UI 오브젝트에서 나갈 때 호출되는 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // 마우스 커서를 기본 모양으로 변경
    }
}
