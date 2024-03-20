using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{

    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    public void OnUpdate()
    {
        // 잠시 주석처리
        //if (EventSystem.current.IsPointerOverGameObject()) return;

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

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
