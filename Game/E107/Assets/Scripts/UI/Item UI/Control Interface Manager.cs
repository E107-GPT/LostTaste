using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 컨트롤 인터페이스 매니저는 아이템 스킬과 직업 스킬의 아이콘과 쿨타임을 표시하는 기능을 제공합니다.
/// </summary>
public class ControlInterfaceManager : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 인벤토리 매니저가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수
    private Item[] _playerInventory; // 플레이어의 인벤토리 배열

    // 아이템 1
    [Header("[ 아이템 1 ]")]
    public GameObject firstItemRightSkillPanel; // 아이템 1 오른쪽 스킬 패널
    public Image firstItemRightSkillIcon; // 아이템 1 오른쪽 스킬 아이콘

    // 아이템 2
    [Header("[ 아이템 2 ]")]
    public GameObject secondItemRightSkillPanel; // 아이템 2 오른쪽 스킬 패널
    public Image secondItemRightSkillIcon; // 아이템 2 오른쪽 스킬 아이콘

    // 스킬 없음 아이콘
    [Header("[ 스킬 없음]")]
    public GameObject skillNonePanel; // 스킬 없음 패널


    // ------------------------------------------------ Life Cylce ------------------------------------------------

    void Update()
    {
        // 컨트롤 인터페이스 업데이트
        UpdateControlInterface();

        // 사용중인 아이템 강조 표시
        //ItemChange();
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 컨트롤 인터페이스를 업데이트하는 메서드
    void UpdateControlInterface()
    {
        // PlayerController 컴포넌트를 찾아서 참조합니다.
        _playerController = GameObject.FindObjectOfType<PlayerController>();

        if (_playerController != null)
        {
            // PlayerController의 인벤토리에 접근합니다.
            _playerInventory = _playerController.Inventory;
        }
        else
        {
            Debug.LogError("PlayerController 컴포넌트를 찾을 수 없습니다.");
        }

        Item firstItem = _playerInventory[1];
        Item secondItem = _playerInventory[2];

        // 아이템 1 오른쪽 스킬 아이콘 업데이트
        firstItemRightSkillIcon.sprite = firstItem.RightSkill.Icon;

        // 아이템 2 오른쪽 스킬 아이콘 업데이트
        secondItemRightSkillIcon.sprite = secondItem.RightSkill.Icon;

        // 무기 교체
        if (Input.GetKey(KeyCode.Alpha1))
        {
            // 스킬이 없을 경우 오른쪽 스킬 아이콘을 스킬 없음 아이콘으로 표시
            if (firstItem.RightSkill.SkillCoolDownTime.ToString() == "Infinity")
            {
                skillNonePanel.SetActive(true);
                firstItemRightSkillPanel.SetActive(false);
            }
            else
            {
                skillNonePanel.SetActive(false);
                firstItemRightSkillPanel.SetActive(true);
            }

            secondItemRightSkillPanel.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            // 스킬이 없을 경우 오른쪽 스킬 아이콘을 스킬 없음 아이콘으로 표시
            if (secondItem.RightSkill.SkillCoolDownTime.ToString() == "Infinity")
            {
                skillNonePanel.SetActive(true);
                secondItemRightSkillPanel.SetActive(false);
            }
            else
            {
                skillNonePanel.SetActive(false);
                secondItemRightSkillPanel.SetActive(true);
            }

            firstItemRightSkillPanel.SetActive(false);
        }
    }

    // 사용 중인 아이템에 따라 스킬 아이콘을 활성화/비활성화 하는 메서드
    //void ItemChange()
    //{
    //    // 무기 교체
    //    if (Input.GetKey(KeyCode.Alpha1))
    //    {
    //        // 스킬이 없을 경우 오른쪽 스킬 아이콘을 스킬 없음 아이콘으로 표시
    //        if (firstItem.RightSkill.SkillCoolDownTime.ToString() == "Infinity")
    //        {
    //            skillNonePanel.SetActive(true);
    //            firstItemRightSkillPanel.SetActive(false);
    //        }
    //        else
    //        {
    //            skillNonePanel.SetActive(false);
    //            firstItemRightSkillPanel.SetActive(true);
    //        }
            
    //        secondItemRightSkillPanel.SetActive(false);
    //    }
    //    else if (Input.GetKey(KeyCode.Alpha2))
    //    {
    //        // 스킬이 없을 경우 오른쪽 스킬 아이콘을 스킬 없음 아이콘으로 표시
    //        if (secondItem.RightSkill.SkillCoolDownTime.ToString() == "Infinity")
    //        {
    //            skillNonePanel.SetActive(true);
    //            secondItemRightSkillPanel.SetActive(false);
    //        }
    //        else
    //        {
    //            skillNonePanel.SetActive(false);
    //            secondItemRightSkillPanel.SetActive(true);
    //        }

    //        firstItemRightSkillPanel.SetActive(false);
    //    }
    //}
}