using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 여러 Agent가 참조해서 사용할 수 있도록 Generic
public class StateMachine<T> where T : class
{
    private T ownerEntity;          // StateMachine 소유주
    private State<T> curState;
    private State<T> previousState; // 이전 상태
    private State<T> globalState;   // 전역 상태

    // entryState: 처음 상태
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
        // 현재 상태와는 별도인 globalState를 매 프레임 수행한다.
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
            // 이전 상태를 저장한다.
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
