using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� Agent�� �����ؼ� ����� �� �ֵ��� Generic
public class StateMachine<T> where T : class
{
    private T ownerEntity;          // StateMachine ������
    private State<T> curState;
    private State<T> previousState; // ���� ����
    private State<T> globalState;   // ���� ����

    // entryState: ó�� ����
    public void Setup(T owner, State<T> entryState)
    {
        ownerEntity = owner;
        curState = null;
        previousState = null;
        globalState = null;

        ChangeState(entryState);
    }

    public void Execute()
    {
        // ���� ���¿ʹ� ������ globalState�� �� ������ �����Ѵ�.
        if (globalState != null)
        {
            globalState.Execute(ownerEntity);
        }

        if (curState != null)
        {
            curState.Execute(ownerEntity);
        }
    }

    public void ChangeState(State<T> newState)
    {
        if (newState == null) return;

        if (curState != null)
        {
            // ���� ���¸� �����Ѵ�.
            previousState = curState;

            curState.Exit(ownerEntity);
        }

        curState = newState;
        curState.Enter(ownerEntity);
    }

    public void SetGlobalState(State<T> newState)
    {
        globalState = newState;
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }
}
