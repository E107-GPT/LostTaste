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


// �Ƹ� �ʿ� ������ �κ�


//public abstract class State<T> where T : class
//{
//    �ش� ���¸� ������ �� 1ȸ ȣ��
//    public abstract void Enter(T entity);

//    �ش� ���¸� ������Ʈ�� �� �� ������ ȣ��
//    public abstract void Execute(T entity);

//    �ش� ���¸� ������ �� 1ȸ ȣ��
//    public abstract void Exit(T entity);
//}