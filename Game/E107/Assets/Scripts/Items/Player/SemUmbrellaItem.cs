using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemUmbrellaItem : Item
{
    private GameObject _openUmbrella, _closedUmbrella;

    protected override void Init()
    {
        base.Init();

        Name = "(삼성)전기 우산";
        FlavorText = "우산 가지고 밖에 나가지 마세요.";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<SemUmbrellaSkill>();

        _openUmbrella = gameObject.transform.Find("Umbrella_Open").gameObject;
        _closedUmbrella = gameObject.transform.Find("Umbrella_Closed").gameObject;

        SetUmbrellaOpen(false);
    }

    public void SetUmbrellaOpen(bool isOpen)
    {
        _openUmbrella.SetActive(isOpen);
        _closedUmbrella.SetActive(!isOpen);
    }
}
