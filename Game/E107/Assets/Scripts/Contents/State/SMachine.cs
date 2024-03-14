using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMachine
{

    private State _currentState ;
    public State CurState { get { return _currentState; } set { _currentState = value; } }
    public void ChangeState(State newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        
        _currentState?.Execute();
    }

}
