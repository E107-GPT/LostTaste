using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven_Item : UI_Base
{
    string _name;

    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TMPro.TextMeshProUGUI>().text = _name;
        Get<GameObject>((int)GameObjects.ItemIcon).gameObject.AddUIEvnent((PointerEventData data) => { Debug.Log($"Item Clicked ! {_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }

}
