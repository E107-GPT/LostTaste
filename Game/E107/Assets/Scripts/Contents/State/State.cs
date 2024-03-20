using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}

public abstract class State : IState
{
    protected BaseController _controller;
    public State(BaseController controller)
    {
        _controller = controller;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}


// 아마 필요 없어질 부분


//public abstract class State<T> where T : class
//{
//    해당 상태를 시작할 때 1회 호출
//    public abstract void Enter(T entity);

//    해당 상태를 업데이트할 때 매 프레임 호출
//    public abstract void Execute(T entity);

//    해당 상태를 종료할 때 1회 호출
//    public abstract void Exit(T entity);
//}