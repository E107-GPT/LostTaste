using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

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

    // 모험 결과 창
    [Header("[ 모험 결과 창 ]")]
    public GameObject adventureResultsWindow;
    public GameObject finalStageIcon; // Final Stage 클리어 아이콘
    public GameObject CongratulationsImagePanel; // 축하 이미지
    public TextMeshProUGUI CongratulationsTitleText; // 축하 제목 텍스트
    public TextMeshProUGUI stageClearText; // 스테이지 클리어 텍스트

    // 확인 버튼
    [Header("[ 확인 버튼 ]")]
    public GameObject returnToCampButton; // 캠프 로드 버튼
    public GameObject goToEndingButton; // 엔딩 로드 버튼

    // 독백 UI 활성화 시 비활성화 할 패널
    [Header("[ 독백 UI 활성화 시 비활성화 할 패널 ]")]
    public GameObject HUD; // HUD
    public GameObject chatWindow; // 채팅 창
    public GameObject chatBackground; // 채팅 배경

    // 독백 UI 비활성화 시 비활성화 할 보스 체력 UI
    [Header("[ 독백 UI 비활성화 시 비활성화 할 보스 체력 UI ]")]
    public GameObject drillDuckHealth; // 드릴덕
    public GameObject crocodileHealth; // 크로커다일
    public GameObject iceKingHealth; // 아이스킹
    public GameObject monsterKingHealth; // 몬스터킹

    // 독백 UI
    [Header("[ 독백 UI ]")]
    public GameObject monologuePanel; // 독백 UI

    // 독백 UI 내부 요소
    [Header("[ 독백 UI 내부 요소 ]")]
    public TextMeshProUGUI nicknameText; // 플레이어 닉네임 텍스트
    public TextMeshProUGUI monologueText; // 독백 텍스트
    public TextMeshProUGUI monologueText2; // 독백 텍스트2
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
            monologueText.text = "마왕의 오른팔로 악명 높은 숲의 주인, '드릴덕'을 처치했다...\n과열된 드릴의 연기로 눈이 따가워지면서 날카로운 쓴맛이 입 안에 번져든다...";
            monologueText2.text = "' 미각이 돌아온걸까? '";
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
            monologueText.text = "해변의 수호자, '크로커다일'과의 힘겨운 전투 끝에 마침내 승리했다...\n침과 바닷물이 섞인 액체를 뱉어내는 순간 입 안에서 강한 소금 맛이 느껴진다...";
            monologueText2.text = "' 이런, 바닷물 맛이 반가울 줄이야. '";

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
            monologueText.text = "얼음 궁전의 군주, '아이스킹'이라면, 왠지 단맛이 돌아오지 않았을까?...\n바닥에 떨어진 얼음 조각을 주워 다급히 입안으로 털어 넣자, 상쾌한 신맛이 입안을 가득 채웠다...";
            monologueText2.text = "' 어떻게 만들어진 얼음이길래 신 맛이 나는거냐고! '";

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
            monologueText.text = "최후의 일격으로 마왕을 쓰러뜨린 순간, 온몸에 전율을 느꼈다...\n달콤한 꽃 향기와 함께 달아오른 강렬한 맛이 입안에서 맴돈다...";
            monologueText2.text = "' 이걸로... 끝인가... '";

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

        Invoke("CloseMonologue", 5f);
    }

    // 독백 UI를 비활성화하는 메서드
    public void CloseMonologue()
    {
        // 독백 UI 비활성화
        monologuePanel.SetActive(false);
        isMonologuePanelOpen = false;

        // 다른 UI 활성화
        HUD.SetActive(true);
        chatWindow.SetActive(true);

        // 보스 체력바 비활성화
        drillDuckHealth.SetActive(false); ; // 드릴덕
        crocodileHealth.SetActive(false); ; // 크로커다일
        iceKingHealth.SetActive(false); ; // 아이스킹
        monsterKingHealth.SetActive(false); ; // 몬스터킹

        // 몬스터 킹 처치 후 독백 UI 비활성화 시 모험 결과 창 활성화
        if (isMonsterKingPanelOpened)
        {
            // 모험 결과 창 업데이트 및 활성화
            finalStageIcon.SetActive(true);
            CongratulationsImagePanel.SetActive(true);
            CongratulationsTitleText.text = "마왕을 물리쳤습니다!";
            stageClearText.text = "모든 스테이지를 클리어했습니다!";

            returnToCampButton.SetActive(false);
            goToEndingButton.SetActive(true);
            adventureResultsWindow.SetActive(true);
            if (PhotonNetwork.IsMasterClient)
            {
                HTTPRequest request;
                request = GameObject.Find("gm").GetComponent<HTTPRequest>();
                string roomName = PhotonNetwork.CurrentRoom.Name;
                int memberCount = PhotonNetwork.CurrentRoom.PlayerCount;

                string printRoomName = roomName;
                int lastIndex = printRoomName.LastIndexOf("`");
                if (lastIndex != -1)
                    printRoomName = printRoomName.Substring(0, lastIndex);

                float time = GameObject.Find("Portal-ForestEntrance-Spawn").GetComponent<DungeonEntrance>().GameTime;

                Debug.Log(time);

                // 로그인 요청 보내기
                Dictionary<string, string> requestParam = new Dictionary<string, string>();
                requestParam.Add("partyName", printRoomName);
                requestParam.Add("memberCount", memberCount.ToString());
                requestParam.Add("playTimeSec", time.ToString());
                requestParam.Add("rngSeed", PhotonNetwork.CurrentRoom.CustomProperties["seed"].ToString());

                Debug.Log(requestParam);
                request.POSTCall("adventure", requestParam);

            }
        }
    }
}
