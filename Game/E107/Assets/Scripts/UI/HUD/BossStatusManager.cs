using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ���� ���¸� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class BossStatusManager : MonoBehaviour
{
    // �帱�� ����
    [Header("[ �帱�� ���� ]")]
    public GameObject drillDuckStatus;
    public TextMeshProUGUI drillDuckNameText; // �̸� �ؽ�Ʈ
    public TextMeshProUGUI drillDuckHealthText; // ü�� �ؽ�Ʈ
    public Slider drillDuckHealthSlider; // ü�� �� �����̴�

    // ũ��Ŀ���� ����
    [Header("[ ũ��Ŀ���� ���� ]")]
    public GameObject crocodileStatus;
    public TextMeshProUGUI crocodileNameText; // �̸� �ؽ�Ʈ
    public TextMeshProUGUI crocodileHealthText; // ü�� �ؽ�Ʈ
    public Slider crocodileHealthSlider; // ü�� �� �����̴�

    // ���̽�ŷ ����
    [Header("[ ���̽�ŷ ���� ]")]
    public GameObject iceKingStatus;
    public TextMeshProUGUI iceKingNameText; // �̸� �ؽ�Ʈ
    public TextMeshProUGUI iceKingHealthText; // ü�� �ؽ�Ʈ
    public Slider iceKingHealthSlider; // ü�� �� �����̴�

    // ����ŷ ����
    [Header("[ ����ŷ ���� ]")]
    public GameObject monsterKingStatus;
    public TextMeshProUGUI monsterKingNameText; // �̸� �ؽ�Ʈ
    public TextMeshProUGUI monsterKingHealthText; // ü�� �ؽ�Ʈ
    public Slider monsterKingHealthSlider; // ü�� �� �����̴�

    // ���� ��� â
    [Header("[ ���� ��� â ]")]
    public GameObject adventureResultsWindow;
    public GameObject finalStageIcon; // Final Stage Ŭ���� ������
    public TextMeshProUGUI stageClearText; // �������� Ŭ���� �ؽ�Ʈ

    // ���� ��Ʈ�ѷ�
    [Header("[ ���� ��Ʈ�ѷ� ]")]
    public DrillDuckController drillDuckController; // �帱��
    public CrocodileController crocodileController; // ũ��Ŀ����
    public IceKingController iceKingController; // ���̽�ŷ
    public MonsterKingController monsterKingController; // ���̽�ŷ

    // �� �����Ӹ��� ȣ��Ǵ� Update �޼���
    void Update()
    {
        // �帱�� ���� ������Ʈ
        UpdateDrillDuckStatus();

        // ũ��Ŀ���� ���� ������Ʈ
        UpdateCrocodileStatus();

        // ���̽�ŷ ���� ������Ʈ
        UpdateIceKingStatus();

        // ����ŷ ���� ������Ʈ
        UpdateMonsterKingStatus();
    }

    // �帱�� ���¸� ������Ʈ �ϴ� �޼���
    void UpdateDrillDuckStatus()
    {
        // ���� GameObject�� ���� ��� �޼��带 ����
        if (GameObject.Find("DrillDuck(Clone)") == null) return;

        // ���� GameObject�� ã�Ƽ� BossController ������Ʈ�� bossController ������ �Ҵ�
        drillDuckController = GameObject.Find("DrillDuck(Clone)").GetComponent<DrillDuckController>();

        // ������ �̸� ������ TextMeshProUGUI�� ����
        drillDuckNameText.text = "Drill Duck";

        // ������ ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = drillDuckController.Stat.Hp;
        int MaxHp = drillDuckController.Stat.MaxHp;
        drillDuckHealthSlider.value = (float)Hp / MaxHp;
        drillDuckHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // ������ ����� ��� ���� ���� â�� ��Ȱ��ȭ
        if (Hp <= 0)
        {
            drillDuckStatus.SetActive(false);
        }
    }

    // ũ��Ŀ���� ���¸� ������Ʈ �ϴ� �޼���
    void UpdateCrocodileStatus()
    {
        // ���� GameObject�� ���� ��� �޼��带 ����
        if (GameObject.Find("Crocodile(Clone)") == null) return;

        // ���� GameObject�� ã�Ƽ� BossController ������Ʈ�� bossController ������ �Ҵ�
        crocodileController = GameObject.Find("Crocodile(Clone)").GetComponent<CrocodileController>();

        // ������ �̸� ������ TextMeshProUGUI�� ����
        crocodileNameText.text = "Crocodile";

        // ������ ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = crocodileController.Stat.Hp;
        int MaxHp = crocodileController.Stat.MaxHp;
        crocodileHealthSlider.value = (float)Hp / MaxHp;
        crocodileHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // ������ ����� ��� ���� ���� â�� ��Ȱ��ȭ
        if (Hp <= 0)
        {
            crocodileStatus.SetActive(false);
        }
    }

    // ���̽�ŷ ���¸� ������Ʈ �ϴ� �޼���
    void UpdateIceKingStatus()
    {
        // ���� GameObject�� ���� ��� �޼��带 ����
        if (GameObject.Find("IceKing(Clone)") == null) return;

        // ���� GameObject�� ã�Ƽ� BossController ������Ʈ�� bossController ������ �Ҵ�
        iceKingController = GameObject.Find("IceKing(Clone)").GetComponent<IceKingController>();

        // ������ �̸� ������ TextMeshProUGUI�� ����
        iceKingNameText.text = "Ice King";

        // ������ ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = iceKingController.Stat.Hp;
        int MaxHp = iceKingController.Stat.MaxHp;
        iceKingHealthSlider.value = (float)Hp / MaxHp;
        iceKingHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // ������ ����� ��� ���� ���� â�� ��Ȱ��ȭ
        if (Hp <= 0)
        {
            iceKingStatus.SetActive(false);
        }
    }

    // ����ŷ ���¸� ������Ʈ �ϴ� �޼���
    void UpdateMonsterKingStatus()
    {
        // ���� GameObject�� ���� ��� �޼��带 ����
        if (GameObject.Find("MonsterKing(Clone)") == null) return;

        // ���� GameObject�� ã�Ƽ� BossController ������Ʈ�� bossController ������ �Ҵ�
        monsterKingController = GameObject.Find("MonsterKing(Clone)").GetComponent<MonsterKingController>();

        // ������ �̸� ������ TextMeshProUGUI�� ����
        monsterKingNameText.text = "Monster King";

        // ������ ���� ü���� ü�� �ٿ� �ݿ�
        int Hp = monsterKingController.Stat.Hp;
        int MaxHp = monsterKingController.Stat.MaxHp;
        monsterKingHealthSlider.value = (float)Hp / MaxHp;
        monsterKingHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // ������ ����� ��� ���� ���� â�� ��Ȱ��ȭ
        if (Hp <= 0)
        {
            monsterKingStatus.SetActive(false);

            // ���� ��� â ������Ʈ �� Ȱ��ȭ
            adventureResultsWindow.SetActive(true);
            finalStageIcon.SetActive(true);
            stageClearText.text = "��� ���������� Ŭ�����߽��ϴ�!";
        }
    }
}

