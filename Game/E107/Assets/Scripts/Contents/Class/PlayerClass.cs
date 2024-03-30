using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{

    Dictionary<string, GameObject> _clothes = new Dictionary<string, GameObject>();
    Skill _classSkill;
    PhotonView photonView;
    
    
    public Skill ClassSkill
    {
        get { return _classSkill; }
        set { _classSkill = value; }
    }
    
    void Start()
    {
        Init();
        ChangeClass(Define.ClassType.None);
    }


    void Init()
    {

        string[] names = System.Enum.GetNames(typeof(Define.Clothes));

        foreach(string name in names)
        {
            GameObject go = Util.FindChild(gameObject, name, true);
            _clothes.Add(name, go);
            go.SetActive(false);
        }

        _clothes["NoneBody"].SetActive(true);


    }

    void UndresseAll()
    {
        foreach(var go in _clothes.Values)
        {
            go.SetActive(false);
        }
        
    }

    public void ChangeClass(Define.ClassType type)
    {
        UndresseAll();
        switch (type)
        {
            case Define.ClassType.Warrior:
                DressUpWarriorSet();
                break;
            case Define.ClassType.Priest:
                DressUpPriestSet();
                break;
            case Define.ClassType.Mage:
                DressUpMageSet();
                break;
            case Define.ClassType.Ninja:
                DressUpNinjaSet();
                break;
            default:
                DressUpNoneSet();
                break;
        }

    }
    void DressUpNoneSet()
    {
        _clothes["NoneBody"].SetActive(true);

        _classSkill = gameObject.GetOrAddComponent<EmptySkill>();
    }


    void DressUpWarriorSet()
    {
        _clothes["WarriorBody"].SetActive(true);
        _clothes["WarriorHat"].SetActive(true);

        _classSkill = gameObject.GetOrAddComponent<WarriorClassSkill>();
    }
    void DressUpPriestSet()
    {
        _clothes["PriestBody"].SetActive(true);
        _clothes["PriestHat"].SetActive(true);

        _classSkill = gameObject.GetOrAddComponent<EmptySkill>();
    }
    void DressUpMageSet()
    {
        _clothes["MageBody"].SetActive(true);
        _clothes["MageHat"].SetActive(true);

        _classSkill = gameObject.GetOrAddComponent<EmptySkill>();
    }
    void DressUpNinjaSet()
    {
        _clothes["NinjaBody"].SetActive(true);
        _clothes["NinjaHair"].SetActive(true);
        _clothes["NinjaMask"].SetActive(true);

        _classSkill = gameObject.GetOrAddComponent<EmptySkill>();
    }




}
