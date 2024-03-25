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
    public TextMeshProUGUI nicknameText; // �г��� �ؽ�Ʈ

    // �÷��̾� ����
    [Header("[ �÷��̾� ���� ]")]
    public TextMeshProUGUI playerHealthText; // �÷��̾� ü�� �ؽ�Ʈ
    public Slider playerHealthSlider; // �÷��̾� ü�� �� �����̴�
    public TextMeshProUGUI playerManaText; // �÷��̾� ���� �ؽ�Ʈ
    public Slider playerManaSlider; // �÷��̾� ���� �� �����̴�
    public PlayerController playerController; // �÷��̾� ��Ʈ�ѷ�

    // �˾� â
    [Header("[ �˾� â ]")]
    public GameObject AdventureResultWindow; // ���� ��� â

    private bool gameOverDisplayed = false; // ���� ���� â�� ǥ�õǾ����� ����

    // ���� �� ȣ��Ǵ� Start �޼���
    void Start()
    {
        // ����� ���� ������Ʈ
        UpdateUserInfoDisplay();

        // �÷��̾� GameObject�� ã�Ƽ� PlayerController ������Ʈ�� playerController ������ �Ҵ�
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // �� �����Ӹ��� ȣ��Ǵ� Update �޼���
    void Update()
    {
        // �÷��̾� ���� ������Ʈ
        UpdatePlayerStatus();
    }

    // ����� ������ UI�� ������Ʈ�ϴ� �޼���
    void UpdateUserInfoDisplay()
    {
        // UserInfo �ν��Ͻ� ��������
        UserInfo userInfo = UserInfo.GetInstance();

        // �г��� ���� ��������
        string nickname = userInfo.getNickName();

        // ������ ������ TextMeshProUGUI�� ����
        nicknameText.text = nickname;
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
        if (Hp <= 0 && !gameOverDisplayed)
        {
            AdventureResultWindow.SetActive(true);
            gameOverDisplayed = true; // ���� ���� â�� ǥ�õǾ����� ǥ��
        }
    }
}

