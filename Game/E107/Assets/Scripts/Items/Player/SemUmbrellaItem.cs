using UnityEngine;

public class SemUmbrellaItem : Item
{
    public bool IsOpen
    {
        get { return _openUmbrella.activeSelf; }
        set
        {
            _openUmbrella.SetActive(value);
            _closedUmbrella.SetActive(!value);
        }
    }

    private GameObject _openUmbrella, _closedUmbrella;

    protected override void Init()
    {
        base.Init();

        _openUmbrella = gameObject.transform.Find("Umbrella_Open").gameObject;
        _closedUmbrella = gameObject.transform.Find("Umbrella_Closed").gameObject;

        IsOpen = false;
    }
}
