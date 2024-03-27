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

        if(!IsPointerOverIgnoredUI()) return;



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
        //EventSystem.current.RaycastAll(pointerData, results);
        if (results.Count == 0) return true;
        //Debug.Log(results.Count);
        foreach (var result in results)
        {
            //Debug.Log($"{result.gameObject.layer} == {LayerMask.GetMask("Ignored UI")}");
            if (result.gameObject.layer == 11) // Ư�� �±׸� ���� UI�� �����մϴ�.
            {
                Debug.Log("Ignored...");
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
