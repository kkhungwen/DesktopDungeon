using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_InsEquip_Idle : State
{
    private SM_InstantiatedEquip stateMachine;

    private void Awake()
    {
        stateMachine = GetComponent<SM_InstantiatedEquip>();
    }

    public override void EnterState()
    {
        stateMachine.clickInput_Object.OnStartLeftDrag += ClickInput_Object_OnStartLeftDrag;

        stateMachine.InstantiatedObjectInspecControl.SetAbleMouseOverInpec(true);
    }

    public override void ExitState()
    {
        stateMachine.clickInput_Object.OnStartLeftDrag -= ClickInput_Object_OnStartLeftDrag;

        stateMachine.InstantiatedObjectInspecControl.SetAbleMouseOverInpec(false);
    }

    private void ClickInput_Object_OnStartLeftDrag()
    {
        stateMachine.SetState(stateMachine.state_Drag);
    }

    private void ClickInput_Panel_OnStartLeftDrag()
    {
        stateMachine.SetState(stateMachine.state_Drag);
    }
}
