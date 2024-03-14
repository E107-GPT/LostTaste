using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class IntOnlyInputField : MonoBehaviour
{
    private InputField inputField;
    void Start()
    {
        inputField = GetComponent<InputField>();

        // UI 시스템에서 input field 컴포넌트 값이 변할 때 마다 호출
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    // int 검증 함수
    void ValidateInput(string input)
    {
        inputField.text = System.Text.RegularExpressions.Regex.Replace(input, "[^0-9]", "");
    }
}
