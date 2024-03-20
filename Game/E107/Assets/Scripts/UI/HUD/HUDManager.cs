using UnityEngine;
using UnityEngine.UI;
using TMPro;

// HUD(Head-Up Display)�� �����ϴ� Ŭ����
public class HUDManager : MonoBehaviour
{
    // UI�� ǥ�õ� ���� �ð�, ���, �г���, ���� ������ ���� TextMeshProUGUI ���� ����
    public TextMeshProUGUI gameTimeText;
    // public TextMeshProUGUI goldText;
    public TextMeshProUGUI nicknameText;
    public TextMeshProUGUI jellyText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI drillDuckHealthText;
    public TextMeshProUGUI drillDuckNameText;

    public Slider playerHealthSlider;
    public Slider drillDuckHealthSlider;

    // �÷��̾�� ���� ���� ���� ����
    public PlayerController playerController;
    public DrillDuckController drillDuckController;

    // �˾� â
    public GameObject GameOverWindow;

    private float gameTime = 0;

    // ���� �� ȣ��Ǵ� Start �޼ҵ�
    void Start()
    {
        playerHealthSlider.value = 1;
        drillDuckHealthSlider.value = 1;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // �� �����Ӹ��� ȣ��Ǵ� Update �޼ҵ�
    void Update()
    {
        // drillDuckController = GameObject.Find("DrillDuck(Clone)").GetComponent<DrillDuckController>();
        UpdateGameTime(); // ���� �ð� ������Ʈ
        // UpdateGoldDisplay(); // ��� ���� ������Ʈ
        UpdateUserInfoDisplay(); // ����� ���� ������Ʈ
        UpdatePlayerHealthBar(); // ü�� �� ������Ʈ
        UpdateDrillDuckHealthBar();
    }

    // ���� �ð��� ������Ʈ�ϴ� �޼ҵ�
    void UpdateGameTime()
    {
        gameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        gameTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // ����� ����(�г���, ���� ��)�� UI�� ������Ʈ�ϴ� �޼ҵ�
    void UpdateUserInfoDisplay()
    {
        // UserInfo �ν��Ͻ� ��������
        UserInfo userInfo = UserInfo.GetInstance();

        // �г��Ӱ� ���� ���� ��������
        string nickname = userInfo.getNickName();
        int jelly = userInfo.getJelly();

        // ������ ������ TextMeshProUGUI�� ����
        nicknameText.text = nickname;
        jellyText.text = jelly.ToString();
    }

    // �÷��̾� ü�� �ٸ� ������Ʈ �ϴ� �޼ҵ�
    void UpdatePlayerHealthBar()
    {
        // �÷��̾��� ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = playerController.Stat.Hp;
        int MaxHp = playerController.Stat.MaxHp;
        playerHealthSlider.value = (float)Hp / MaxHp;
        playerHealthText.text = string.Format("{0:00} / {1:00}", Hp, MaxHp);

        if (Hp < 0)
        {
            GameOverWindow.SetActive(true);
            playerHealthText.text = string.Format("0 / {0:00}", MaxHp);
        }
    }

    //�帱 �� ü�� �ٸ� ������Ʈ �ϴ� �޼ҵ�
    void UpdateDrillDuckHealthBar()
    {
        if (GameObject.Find("DrillDuck(Clone)") == null) return;
        drillDuckController = GameObject.Find("DrillDuck(Clone)").GetComponent<DrillDuckController>();
        // �帱 ���� ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = drillDuckController.Stat.Hp;
        int MaxHp = drillDuckController.Stat.MaxHp;
        drillDuckHealthSlider.value = (float)Hp / MaxHp;
        drillDuckHealthText.text = string.Format("{0:00} / {1:00}", Hp, MaxHp);
        drillDuckNameText.text = "Drill Duck";

        if (Hp < 0)
        {
            drillDuckHealthText.text = string.Format("0 / {0:00}", MaxHp);
        }
    }
}

