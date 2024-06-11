using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SM_Base : StateMachine
{
    [SerializeField] private State startState;

    public override void StartStateMachine()
    {
        SetState(startState);
    }
}
