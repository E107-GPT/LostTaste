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

        // UI �ý��ۿ��� input field ������Ʈ ���� ���� �� ���� ȣ��
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    // int ���� �Լ�
    void ValidateInput(string input)
    {
        inputField.text = System.Text.RegularExpressions.Regex.Replace(input, "[^0-9]", "");
    }
}
