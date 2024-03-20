using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// HUD(Head-Up Display)�� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class HUDManager : MonoBehaviour
{
    // ���� ����
    [Header("[ ���� ���� ]")]
    public TextMeshProUGUI gameTimeText; // ���� �ð� �ؽ�Ʈ
    // public TextMeshProUGUI goldText;
    public TextMeshProUGUI jellyText; // ���� �ؽ�Ʈ
    public TextMeshProUGUI nicknameText; // �г��� �ؽ�Ʈ

    // �÷��̾� ����
    [Header("[ �÷��̾� ���� ]")]
    public TextMeshProUGUI playerHealthText; // �÷��̾� ü�� �ؽ�Ʈ
    public Slider playerHealthSlider; // �÷��̾� ü�� �� �����̴�
    public PlayerController playerController; // �÷��̾� ��Ʈ�ѷ�

    // Drill Duck ����
    [Header("[ Drill Duck ���� ]")]
    public TextMeshProUGUI drillDuckNameText; // Drill Duck �̸� �ؽ�Ʈ
    public TextMeshProUGUI drillDuckHealthText; // Drill Duck ü�� �ؽ�Ʈ
    public Slider drillDuckHealthSlider; // Drill Duck  ü�� �� �����̴�
    public DrillDuckController drillDuckController; // Drill Duck ��Ʈ�ѷ�

    // �˾� â
    [Header("[ �˾� â ]")]
    public GameObject GameOverWindow; // ���� ���� â

    // ���� ���� �ð� �ʱ�ȭ
    private float gameTime = 0;

    // ���� �� ȣ��Ǵ� Start �޼���
    void Start()
    {
        // Player GameObject�� ã�Ƽ� PlayerController ������Ʈ�� playerController ������ �Ҵ�
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // �� �����Ӹ��� ȣ��Ǵ� Update �޼���
    void Update()
    {
        // ���� �ð� ������Ʈ
        UpdateGameTime();

        // ��� ���� ������Ʈ
        // UpdateGoldDisplay();

        // ����� ���� ������Ʈ
        UpdateUserInfoDisplay();

        // �÷��̾� ���� ������Ʈ
        UpdatePlayerStatus();

        // Drill Duck ���� ������Ʈ
        UpdateDrillDuckStatus();
    }

    // ���� �ð��� ������Ʈ�ϴ� �޼���
    void UpdateGameTime()
    {
        gameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        gameTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // ����� ����(�г���, ���� ��)�� UI�� ������Ʈ�ϴ� �޼���
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

    // �÷��̾� ���¸� ������Ʈ �ϴ� �޼���
    void UpdatePlayerStatus()
    {
        // �÷��̾��� ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = playerController.Stat.Hp;
        int MaxHp = playerController.Stat.MaxHp;
        playerHealthSlider.value = (float)Hp / MaxHp;
        playerHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // �÷��̾ ����� ��� ���� ���� â�� Ȱ��ȭ�ϰ� ü���� 0���� ����
        if (Hp < 0)
        {
            GameOverWindow.SetActive(true);
            playerHealthText.text = string.Format("0 / {0:0}", MaxHp);
        }
    }

    // Drill Duck ���¸� ������Ʈ �ϴ� �޼���
    void UpdateDrillDuckStatus()
    {
        // Drill Duck GameObject�� ���� ��� �޼��带 ����
        if (GameObject.Find("DrillDuck(Clone)") == null) return;

        // Drill Duck GameObject�� ã�Ƽ� DrillDuckController ������Ʈ�� drillDuckController ������ �Ҵ�
        drillDuckController = GameObject.Find("DrillDuck(Clone)").GetComponent<DrillDuckController>();

        // Drill Duck�� ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = drillDuckController.Stat.Hp;
        int MaxHp = drillDuckController.Stat.MaxHp;
        drillDuckHealthSlider.value = (float)Hp / MaxHp;
        drillDuckHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // Drill Duck�� �̸� ������ TextMeshProUGUI�� ����
        drillDuckNameText.text = "Drill Duck";

        // Drill Duck�� ����� ��� ü���� 0���� ����
        if (Hp < 0)
        {
            drillDuckHealthText.text = string.Format("0 / {0:0}", MaxHp);
        }
    }
}

