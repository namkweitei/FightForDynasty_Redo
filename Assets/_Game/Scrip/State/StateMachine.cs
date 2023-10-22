using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateMachine
{
    //cai nay de biet duoc no dang o state nao
    public string name;

    public delegate void StateAction(ref Action onEnter, ref Action onExecute, ref Action onExit);
    private Action onEnter, onExecute, onExit;

    public void Execute()
    {
        onExecute?.Invoke();
    }

    public void ChangeState(StateAction stateAction)
    {
        onExit?.Invoke();
        stateAction.Invoke(ref onEnter, ref onExecute, ref onExit);
        onEnter?.Invoke();
        //cai nay de biet duoc no dang o state nao
        name = stateAction.Method.Name;
    }
}


