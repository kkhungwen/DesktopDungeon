using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class State : MonoBehaviour
{
    public event Action OnEnterState;
    public event Action OnExitState;

    public virtual void EnterState()
    {
        
    }

    public virtual void ExitState()
    {

    }

    public void CallEnterState()
    {
        OnEnterState?.Invoke();
    }

    public void CallExitState()
    {
        OnExitState?.Invoke();
    }


    public virtual void StateUpdate()
    {

    }

    public virtual void StateFixedUpdate()
    {

    }

    public virtual void StateLateUpdate()
    {

    }
}
