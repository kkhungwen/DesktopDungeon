using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Chest_Idle : State
{
    private SM_Chest stateMachine;

    private void Awake()
    {
        stateMachine = GetComponent<SM_Chest>();
    }

    public override void EnterState()
    {
        stateMachine.clickInput_Object.OnStartLeftDrag += ClickInput_Object_OnStartLeftDrag;
    }

    public override void ExitState()
    {
        stateMachine.clickInput_Object.OnStartLeftDrag -= ClickInput_Object_OnStartLeftDrag;
    }

    private void ClickInput_Object_OnStartLeftDrag()
    {
        stateMachine.SetState(stateMachine.state_Drag);
    }
}
