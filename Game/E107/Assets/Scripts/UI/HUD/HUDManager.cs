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
    public TextMeshProUGUI playerManaText; // �÷��̾� ���� �ؽ�Ʈ
    public Slider playerManaSlider; // �÷��̾� ���� �� �����̴�
    public PlayerController playerController; // �÷��̾� ��Ʈ�ѷ�

    // ���� ����
    [Header("[ ���� ���� ]")]
    public GameObject bossStatus;
    public TextMeshProUGUI bossNameText; // ���� �̸� �ؽ�Ʈ
    public TextMeshProUGUI bossHealthText; // ���� ü�� �ؽ�Ʈ
    public Slider bossHealthSlider; // ���� ü�� �� �����̴�

    // ���� ��Ʈ�ѷ�
    [Header("[ ���� ��Ʈ�ѷ� ]")]
    public DrillDuckController bossController; // �帱��

    // �˾� â
    [Header("[ �˾� â ]")]
    public GameObject GameOverWindow; // ���� ���� â

    // ���� ���� �ð� �ʱ�ȭ
    private float gameTime = 0;

    // ���� �� ȣ��Ǵ� Start �޼���
    void Start()
    {
        // �÷��̾� GameObject�� ã�Ƽ� PlayerController ������Ʈ�� playerController ������ �Ҵ�
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // �� �����Ӹ��� ȣ��Ǵ� Update �޼���
    void Update()
    {
        // ���� �ð� ������Ʈ
        UpdateGameTime();

        // ��� ���� ������Ʈ
        // UpdateGoldDisplay();

        // ����� ���� ������Ʈw
        UpdateUserInfoDisplay();

        // �÷��̾� ���� ������Ʈ
        UpdatePlayerStatus();

        // ���� ���� ������Ʈ
        UpdateBossStatus();
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

        // �÷��̾��� ���� ������ ���� �ٿ� �ݿ�
        int Mp = playerController.Stat.Mp;
        int MaxMp = playerController.Stat.MaxMp;
        playerManaSlider.value = (float)Mp / MaxMp;
        playerManaText.text = string.Format("{0:0} / {1:0}", Mp, MaxMp);

        // �÷��̾ ����� ��� ���� ���� â�� Ȱ��ȭ
        if (Hp <= 0)
        {
            GameOverWindow.SetActive(true);
        }
    }

    // ���� ���¸� ������Ʈ �ϴ� �޼���
    void UpdateBossStatus()
    {
        // ���� GameObject�� ���� ��� �޼��带 ����
        if (GameObject.Find("DrillDuck(Clone)") == null) return;

        // ���� GameObject�� ã�Ƽ� BossController ������Ʈ�� bossController ������ �Ҵ�
        bossController = GameObject.Find("DrillDuck(Clone)").GetComponent<DrillDuckController>();

        // ������ �̸� ������ TextMeshProUGUI�� ����
        bossNameText.text = "Drill Duck";

        // ������ ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = bossController.Stat.Hp;
        int MaxHp = bossController.Stat.MaxHp;
        bossHealthSlider.value = (float)Hp / MaxHp;
        bossHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // ������ ����� ��� ���� ���� â�� ��Ȱ��ȭ
        if (Hp <= 0)
        {
            bossStatus.SetActive(false);
        }
    }
}

