using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseEntity : MonoBehaviour
{
    /*
    ���� ID ����
    EnemyBaseEntity�� ��ӹ޴� ��� ���� object�� ID ��ȣ�� �ο��޴´�.
    0���� �����ؼ� 1�� �����Ѵ�.
    */
    private static long enemy_ID = 0;

    private long id;
    public long ID
    {
        set
        {
            id = value;
            enemy_ID++;
        }
        get
        {
            return id;
        }
    }

    private string enemyEntityName; // ������Ʈ �̸�
    private string personalColor;   // ������Ʈ ����( TEXT ��¿� )


    // �ڽ� Ŭ�������� base.Setup()���� ȣ��!!
    public virtual void Setup(string name)
    {
        // id, �̸�, ���� ����
        ID = enemy_ID;
        enemyEntityName = name;
        int color = Random.Range(0, 1000000);
        personalColor = $"#{color.ToString("X6")}";
    }

    // GameController���� ��� ������Ʈ�� Updated()�� �����Ѵ�.
    public abstract void Updated();

    // console�� [ �̸� : "���" ] ���
    public void PrintText(string text)
    {
        Debug.Log($"<color={personalColor}><b>{enemyEntityName}</b></color> : {text}");
    }
}
