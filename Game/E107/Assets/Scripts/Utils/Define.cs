using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum MonsterState    // 피격 당하면 공격을 가한 플레이어를 추적하는 기능
    {
        IDLE = 0,
        // PATROL,
        CHASE,
        ATTACK,
        DIE,
        GLOBAL,
    }

    public enum State
    {
        Die,
        Moving,
        Skill,
        Idle,
        Dash,
    }

    public enum Sound
    {
        BGM,
        Effect,
        MaxCount
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }
    public enum UIEvent
    {
        Click,
        Drag,
    }
    public enum MouseEvent
    {
        Press,
        Click
    }
    public enum CameraMode
    {
        QuarterVeiw
    }
}
