using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{

    Dictionary<string, GameObject> _clothes = new Dictionary<string, GameObject>();
    Skill _classSkill;
    PlayerStat playerStat;
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
        playerStat = gameObject.GetComponent<PlayerController>().Stat;

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
        // stat

        playerStat.MaxHp = 100;
        playerStat.Hp = 100;


        _clothes["NoneBody"].SetActive(true);

        _classSkill = gameObject.GetOrAddComponent<EmptySkill>();
    }


    void DressUpWarriorSet()
    {
        playerStat.MaxHp = 300;
        playerStat.Hp = 300;

        _clothes["WarriorBody"].SetActive(true);
        _clothes["WarriorHat"].SetActive(true);

        _classSkill = gameObject.GetOrAddComponent<WarriorClassSkill>();
    }
    void DressUpPriestSet()
    {
        playerStat.MaxHp = 150;
        playerStat.Hp = 150;
        playerStat.MaxMp = 200;
        playerStat.Mp = 200;

        _clothes["PriestBody"].SetActive(true);
        _clothes["PriestHat"].SetActive(true);

        _classSkill = gameObject.GetOrAddComponent<PriestClassSkill>();
    }
    void DressUpMageSet()
    {

        playerStat.MaxHp = 100;
        playerStat.Hp = 100;
        playerStat.MaxMp = 300;
        playerStat.Mp = 300;

        _clothes["MageBody"].SetActive(true);
        _clothes["MageHat"].SetActive(true);

        _classSkill = gameObject.GetOrAddComponent<MageClassSkill>();
    }
    void DressUpNinjaSet()
    {
        playerStat.MaxHp = 100;
        playerStat.Hp = 100;

        playerStat.MaxMp = 150;
        playerStat.Mp = 150;
        playerStat.MoveSpeed = 6.5f;



        _clothes["NinjaBody"].SetActive(true);
        _clothes["NinjaHair"].SetActive(true);
        _clothes["NinjaMask"].SetActive(true);

        _classSkill = gameObject.GetOrAddComponent<NinjaClassSkill>();
    }




}
