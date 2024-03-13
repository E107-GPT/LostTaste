using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseEntity : MonoBehaviour
{
    /*
    몬스터 ID 관리
    EnemyBaseEntity를 상속받는 모든 게임 object는 ID 번호를 부여받는다.
    0부터 시작해서 1씩 증가한다.
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

    private string enemyEntityName; // 에이전트 이름
    private string personalColor;   // 에이전트 색상( TEXT 출력용 )


    // 자식 클래스에서 base.Setup()으로 호출!!
    public virtual void Setup(string name)
    {
        // id, 이름, 색상 설정
        ID = enemy_ID;
        enemyEntityName = name;
        int color = Random.Range(0, 1000000);
        personalColor = $"#{color.ToString("X6")}";
    }

    // GameController에서 모든 에이전트를 Updated()로 동작한다.
    public abstract void Updated();

    // console에 [ 이름 : "대사" ] 출력
    public void PrintText(string text)
    {
        Debug.Log($"<color={personalColor}><b>{enemyEntityName}</b></color> : {text}");
    }
}
