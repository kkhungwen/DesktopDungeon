using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_Being))]
[DisallowMultipleComponent]
public class State_Stagger : State
{
    private StateRequiredReference_Being requiredReference;

    private float staggerTime = 0.5f;
    private float startTime;

    private void Awake()
    {
        requiredReference = GetComponent<StateRequiredReference_Being>();
    }

    public override void StateUpdate()
    {
        if (Time.time >= startTime + staggerTime)
        {
            requiredReference.stateMachine.SetState(requiredReference.state_Idle);
            return;
        }
    }

    public override void EnterState()
    {
        requiredReference.health.OnEmptyHealth += Health_OnEmptyHealth;

        startTime = Time.time;
    }

    public override void ExitState()
    {
        requiredReference.health.OnEmptyHealth -= Health_OnEmptyHealth;
    }

    private void Health_OnEmptyHealth()
    {
        if (!requiredReference.stateMachine.IsStateCurrent(this))
            return;

        requiredReference.stateMachine.SetState(requiredReference.state_Die);
    }
}
