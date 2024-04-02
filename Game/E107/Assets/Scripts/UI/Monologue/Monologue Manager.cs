using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 보스 처치 후 독백 UI 활성화 및 비활성화를 관리하는 클래스입니다.
/// </summary>
public class MonologueManager: MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 보스 컨트롤러
    private DrillDuckController drillDuckController; // 드릴덕
    private CrocodileController crocodileController; // 크로커다일
    private IceKingController iceKingController; // 아이스킹
    private MonsterKingController monsterKingController; // 아이스킹
    private bool isMonologuePanelOpen = false;
    private bool isDrillDuckPanelOpened = false;
    private bool isCrocodilePanelOpened = false;
    private bool isIceKingPanelOpened = false;
    private bool isMonsterKingPanelOpened = false;

    // 독백 UI 활성화 시 비활성화 할 패널
    [Header("[ 독백 UI 활성화 시 비활성화 할 패널 ]")]
    public GameObject HUD; // HUD
    public GameObject chatWindow; // 채팅 창
    public GameObject chatBackground; // 채팅 배경

    // 독백 UI
    [Header("[ 독백 UI ]")]
    public GameObject monologuePanel; // 독백 UI

    // 독백 UI 내부 요소
    [Header("[ 독백 UI 내부 요소 ]")]
    public TextMeshProUGUI nicknameText; // 플레이어 닉네임 텍스트
    public TextMeshProUGUI monologueText; // 독백 텍스트
    public Button monologueCloseButton; // 확인 버튼


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Start()
    {
        // 사용자 닉네임 텍스트 업데이트
        UpdateNicknameText();

        monologueCloseButton.onClick.AddListener(CloseMonologue);
    }

    void Update()
    {
        // 독백 UI 업데이트
        UpdateMonologuePanel();

        // 독백 UI 닫기
        if (Input.GetKeyDown(KeyCode.E) && isMonologuePanelOpen)
        {
            CloseMonologue();
        }
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 사용자 닉네임 정보를 업데이트하는 메서드
    void UpdateNicknameText()
    {
        // UserInfo 인스턴스 가져오기
        UserInfo userInfo = UserInfo.GetInstance();

        // 닉네임 정보 가져오기
        string nickname = userInfo.getNickName();

        // 가져온 정보를 TextMeshProUGUI에 적용
        nicknameText.text = nickname;
    }

    // 독백 UI를 업데이트하는 메서드
    void UpdateMonologuePanel()
    {
        // 보스 GameObject를 찾고 독백 텍스트를 업데이트
        if (GameObject.Find("DrillDuck(Clone)") != null && !isDrillDuckPanelOpened)
        {
            drillDuckController = GameObject.Find("DrillDuck(Clone)").GetComponent<DrillDuckController>();

            // 독백 텍스트 업데이트
            monologueText.text = "신 맛이 돌아왔다.";

            // 보스가 사망할 경우 독백 UI를 활성화
            int Hp = drillDuckController.Stat.Hp;
            if (Hp <= 0)
            {
                OpenMonologue();
                isDrillDuckPanelOpened = true;
            }
        }
        else if (GameObject.Find("Crocodile(Clone)") != null && !isCrocodilePanelOpened)
        {
            crocodileController = GameObject.Find("Crocodile(Clone)").GetComponent<CrocodileController>();

            // 독백 텍스트 업데이트
            monologueText.text = "짠 맛이 돌아왔다.";

            // 보스가 사망할 경우 독백 UI를 활성화
            int Hp = crocodileController.Stat.Hp;
            if (Hp <= 0)
            {
                OpenMonologue();
                isCrocodilePanelOpened = true;
            }
        }
        else if (GameObject.Find("IceKing(Clone)") != null && !isIceKingPanelOpened)
        {
            iceKingController = GameObject.Find("IceKing(Clone)").GetComponent<IceKingController>();

            // 독백 텍스트 업데이트
            monologueText.text = "단 맛이 돌아왔다.";

            // 보스가 사망할 경우 독백 UI를 활성화
            int Hp = iceKingController.Stat.Hp;
            if (Hp <= 0)
            {
                OpenMonologue();
                isIceKingPanelOpened = true;
            }
        }
        else if (GameObject.Find("MonsterKing(Clone)") != null && !isMonsterKingPanelOpened)
        {
            monsterKingController = GameObject.Find("MonsterKing(Clone)").GetComponent<MonsterKingController>();

            // 독백 텍스트 업데이트
            monologueText.text = "모든 맛이 돌아왔다.";

            // 보스가 사망할 경우 독백 UI를 활성화
            int Hp = monsterKingController.Stat.Hp;
            if (Hp <= 0)
            {
                OpenMonologue();
                isMonsterKingPanelOpened = true;
            }
        }
    }

    // 독백 UI를 활성화하는 메서드
    void OpenMonologue()
    {
        // 독백 UI 활성화
        monologuePanel.SetActive(true);
        isMonologuePanelOpen = true;

        // 다른 UI 비활성화
        HUD.SetActive(false);
        chatWindow.SetActive(false);
        chatBackground.SetActive(false);
    }

    // 독백 UI를 비활성화하는 메서드
    void CloseMonologue()
    {
        // 독백 UI 활성화
        monologuePanel.SetActive(false);
        isMonologuePanelOpen = false;

        // 다른 UI 비활성화
        HUD.SetActive(true);
        chatWindow.SetActive(true);
    }
}
