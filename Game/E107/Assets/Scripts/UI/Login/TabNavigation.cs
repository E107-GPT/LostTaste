using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// TabNavigation 클래스는 Unity UI에서 Tab 키를 이용한 입력 필드 간의 순환 이동을 가능하게 합니다.
/// 이 클래스를 사용함으로써, 사용자는 Tab 키를 눌러 지정된 입력 필드 배열(inputs) 내의 다음 필드로 포커스를 이동시킬 수 있습니다.
/// 배열의 마지막 입력 필드에서 Tab을 누르면, 배열의 첫 번째 입력 필드로 포커스가 이동하게 됩니다.
/// </summary>
public class TabNavigation : MonoBehaviour
{
    // 입력 필드 배열
    [Header("입력 필드 배열")]
    public Selectable[] inputs;

    void Update()
    {
        // Tab 키가 눌렸을 때의 동작을 처리
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 현재 선택된 UI 요소를 가져옴
            Selectable current = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
            if (current != null)
            {
                // 현재 선택된 요소가 inputs 배열 내에 있는지 확인하고, 그 인덱스를 찾음
                int index = System.Array.IndexOf(inputs, current);
                if (index >= 0)
                {
                    // 현재 선택된 입력 필드의 다음 입력 필드를 계산
                    // 배열의 마지막 입력 필드에서 Tab을 누르면, 첫 번째 입력 필드로 돌아감
                    Selectable next = inputs[(index + 1) % inputs.Length];
                    if (next != null)
                    {
                        // 다음 입력 필드가 InputField 컴포넌트를 가지고 있으면, 포인터 클릭 이벤트를 시뮬레이션 함
                        InputField inputfield = next.GetComponent<InputField>();
                        if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(EventSystem.current));

                        // EventSystem을 통해 다음 입력 필드로 포커스를 이동
                        EventSystem.current.SetSelectedGameObject(next.gameObject, new BaseEventData(EventSystem.current));
                    }
                }
            }
        }
    }
}
