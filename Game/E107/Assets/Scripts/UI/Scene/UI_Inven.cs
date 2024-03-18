using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    public enum GameObjects
    {
        GridPanel
    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        // 실제 인벤토리 정보를 참고해야한다.
        for(int i = 0; i < 8; i++)
        {
            UI_Inven_Item invenItem = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform);
            // InvenItem을 반환 받음



            //GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            //UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"Sowrd{i}");
            // Component에 Set해주기
            
        }

    }

   
}
