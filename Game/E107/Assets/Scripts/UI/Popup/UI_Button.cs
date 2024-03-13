using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    


    enum Buttons
    {
        PointButton
    }
    enum Texts
    {
        PointText,
        ScoreText
    }
    // 목록을 넘겨 주고싶어
    // Replection 사용할 것

    enum GameObjects
    {
        TestObject
    }

    enum Images
    {
        ItemImage,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));


        //UI_EventHandler evt = GetImage((int)Images.ItemImage).gameObject.GetComponent<UI_EventHandler>();
        //evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position;});

        GetButton((int)Buttons.PointButton).gameObject.AddUIEvnent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemImage).gameObject;
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

    }

    int _score = 0;

    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"Score : {_score}";

    }
}
