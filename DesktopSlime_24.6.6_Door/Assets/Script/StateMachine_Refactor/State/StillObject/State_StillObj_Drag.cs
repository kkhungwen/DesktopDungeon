using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_StillObject))]
public class State_StillObj_Drag : State
{
    private StateRequiredReference_StillObject stateRequiredReference;

    private void Awake()
    {
        stateRequiredReference = GetComponent<StateRequiredReference_StillObject>();
    }

    public override void StateLateUpdate()
    {
        stateRequiredReference.dragObject.DragOnObject(transform.position, stateRequiredReference.dropable.Value.GetDropObject());
    }

    public override void EnterState()
    {
        stateRequiredReference.clickInput_Object.OnLeftMouseUp += ClickInput_Object_OnLeftMouseUp;

        stateRequiredReference.followMousePosition.StartFollow(Vector2.zero);
    }

    public override void ExitState()
    {
        stateRequiredReference.clickInput_Object.OnLeftMouseUp -= ClickInput_Object_OnLeftMouseUp;

        stateRequiredReference.followMousePosition.EndFollow();
    }

    private void ClickInput_Object_OnLeftMouseUp(Vector2 position)
    {
        stateRequiredReference.dragObject.DropObject(stateRequiredReference.dropable.Value.GetDropObject());

        stateRequiredReference.stateMachine.SetState(stateRequiredReference.state_Idle);
    }
}
