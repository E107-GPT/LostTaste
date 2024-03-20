using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// FormSubmitOnEnter 클래스는 Unity UI 폼에서 Enter 키를 누르는 것으로
/// 지정된 버튼의 클릭 이벤트를 발생시키는 기능을 제공합니다.
/// </summary>
public class FormSubmitOnEnter : MonoBehaviour
{
    // 버튼
    [Header("[ 버튼 ]")]
    public Button submitButton; // 사용자가 Enter를 눌렀을 때 클릭되어야 할 버튼

    void Update()
    {
        // 사용자가 Enter 키를 눌렀는지 확인
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 지정된 버튼의 onClick 이벤트를 호출
            submitButton.onClick.Invoke();
        }
    }
}
