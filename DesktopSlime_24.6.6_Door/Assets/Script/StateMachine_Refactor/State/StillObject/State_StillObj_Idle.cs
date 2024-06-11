using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_StillObject))]
public class State_StillObj_Idle : State
{
    private StateRequiredReference_StillObject stateRequiredReference;

    private void Awake()
    {
        stateRequiredReference = GetComponent<StateRequiredReference_StillObject>();
    }

    public override void EnterState()
    {
        stateRequiredReference.clickInput_Object.OnStartLeftDrag += ClickInput_Object_OnStartLeftDrag;

        stateRequiredReference.InstantiatedObjectInspecControl.SetAbleMouseOverInpec(true);
    }

    public override void ExitState()
    {
        stateRequiredReference.clickInput_Object.OnStartLeftDrag -= ClickInput_Object_OnStartLeftDrag;

        stateRequiredReference.InstantiatedObjectInspecControl.SetAbleMouseOverInpec(false);
    }

    private void ClickInput_Object_OnStartLeftDrag()
    {
        stateRequiredReference.stateMachine.SetState(stateRequiredReference.state_Drag);
    }
}
