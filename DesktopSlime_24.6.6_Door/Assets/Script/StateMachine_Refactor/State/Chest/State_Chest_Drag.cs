using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Chest_Drag : State
{
    private SM_Chest stateMachine;

    private void Awake()
    {
        stateMachine = GetComponent<SM_Chest>();
    }

    public override void StateLateUpdate()
    {
        stateMachine.dragObject.DragOnObject(transform.position, stateMachine.GetChestData());
    }

    public override void EnterState()
    {
        stateMachine.clickInput_Object.OnLeftMouseUp += ClickInput_Object_OnLeftMouseUp;

        stateMachine.followMousePosition.StartFollow(Vector2.zero);
    }

    public override void ExitState()
    {
        stateMachine.clickInput_Object.OnLeftMouseUp -= ClickInput_Object_OnLeftMouseUp;

        stateMachine.followMousePosition.EndFollow();
    }

    private void ClickInput_Object_OnLeftMouseUp(Vector2 position)
    {
        stateMachine.dragObject.DropObject(stateMachine.GetChestData());

        stateMachine.SetState(stateMachine.state_Idle);
    }
}
