using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI; // NavMeshAgent ����� ���� �ʿ�
using TMPro;

/// <summary>
/// ������ ����Ǹ� �˾��Ǵ� ���â�� �����ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class AdventureResultWindow : MonoBehaviour
{
    // ���� �ð�
    [Header("[ ���� �ð� ]")]
    public TextMeshProUGUI gameTimeText; // ���� �ð� �ؽ�Ʈ

    void OnEnable()
    {
        float gameTime = DungeonEntrance.Instance.GameTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        gameTimeText.text = string.Format("{0:0}�� {1:00}��", minutes, seconds);
    }
}