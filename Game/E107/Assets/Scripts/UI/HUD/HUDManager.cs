using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Slider healthSlider; // ü�� �ٷ� ����� Slider
    [SerializeField] private PlayerStat playerStat; // �÷��̾��� Stat ����

    private float gameTime = 0;

    // ���� �� ȣ��Ǵ� Start �޼ҵ�
    void Start()
    {
        if (playerStat != null)
        {
            // ���� ���� ��, �÷��̾��� �ִ� ü������ ü�� �� �ʱ�ȭ
            healthSlider.value = 1;
        }
    }

    // �� �����Ӹ��� ȣ��Ǵ� Update �޼ҵ�
    void Update()
    {
        UpdateGameTime(); // ���� �ð� ������Ʈ
        // UpdateGoldDisplay(); // ��� ���� ������Ʈ
        UpdateUserInfoDisplay(); // ����� ���� ������Ʈ
        UpdateHealthBar(); // ü�¹� ������Ʈ ȣ��
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

    // ü�� �ٸ� ������Ʈ �ϴ� �޼ҵ�
    void UpdateHealthBar()
    {
        if (playerStat != null)
        {
            // �÷��̾��� ���� ü�� ������ ����Ͽ� ü�� �ٿ� �ݿ�
            healthSlider.value = (float)playerStat.Hp / playerStat.MaxHp;
        }
    }
}

