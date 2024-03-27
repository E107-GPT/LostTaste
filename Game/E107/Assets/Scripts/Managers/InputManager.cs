using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager
{

    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    
    public void OnUpdate()
    {
        // ��� �ּ�ó��
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("TTEESSTT");
            if (!IsPointerOverIgnoredUI()) return;

        }


        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();
        //if (KeyAction != null)
        //    KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.Click);
                }

                _pressed = false;



            }
        }
    }

    private bool IsPointerOverIgnoredUI()
    {
        // ���� �������� ��ġ�� ������� �� PointerEventData ��ü�� �����մϴ�.
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // ����ĳ��Ʈ ����� ���� ����Ʈ�� �����մϴ�.
        List<RaycastResult> results = new List<RaycastResult>();
        GraphicRaycaster raycaster = UnityEngine.Object.FindObjectOfType<GraphicRaycaster>();
        if (raycaster == null)
        {
            Debug.LogError("GraphicRaycaster not found in the current scene.");
            return false;
        }
        // ����ĳ��Ʈ�� �����մϴ�.
        raycaster.Raycast(pointerData, results);
        Debug.Log(results.Count);
        foreach (var result in results)
        {
            Debug.Log($"{result}");
            if (result.gameObject.name == "StageName") // Ư�� �±׸� ���� UI�� �����մϴ�.
            {
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
