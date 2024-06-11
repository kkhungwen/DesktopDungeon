using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_Being))]
[DisallowMultipleComponent]
public class State_Drag : State
{
    private StateRequiredReference_Being requiredReference;
    private Vector2 centerPointOffset = new(0, 0.5f);

    private void Awake()
    {
        requiredReference = GetComponent<StateRequiredReference_Being>();
    }

    public override void EnterState()
    {
        requiredReference.playerClickInput.OnLeftMouseUp += PlayerClickInput_OnLeftMouseUp;
        requiredReference.health.OnEmptyHealth += Health_OnEmptyHealth;

        requiredReference.knockBack.isAble = false;
        requiredReference.followMousePosition.StartFollow(centerPointOffset);
    }

    public override void ExitState()
    {
        requiredReference.playerClickInput.OnLeftMouseUp -= PlayerClickInput_OnLeftMouseUp;
        requiredReference.health.OnEmptyHealth -= Health_OnEmptyHealth;

        requiredReference.knockBack.isAble = true;
        requiredReference.followMousePosition.EndFollow();
    }

    private void Health_OnEmptyHealth()
    {
        if (!requiredReference.stateMachine.IsStateCurrent(this))
            return;

        requiredReference.stateMachine.SetState(requiredReference.state_Die);
    }

    private void PlayerClickInput_OnLeftMouseUp(Vector2 obj)
    {
        if (!requiredReference.stateMachine.IsStateCurrent(this))
            return;

        requiredReference.stateMachine.SetState(requiredReference.state_Fall);
    }
}
