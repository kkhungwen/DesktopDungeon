using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_Being))]
[DisallowMultipleComponent]
public class State_Fall : State
{
    private StateRequiredReference_Being requiredReference;

    private void Awake()
    {
        requiredReference = GetComponent<StateRequiredReference_Being>();
    }

    public override void EnterState()
    {
        requiredReference.health.OnEmptyHealth += Health_OnEmptyHealth;
        requiredReference.takeDamage.OnTakeDamage += TakeDamage_OnTakeDamage;
    }

    public override void ExitState()
    {
        requiredReference.health.OnEmptyHealth -= Health_OnEmptyHealth;
        requiredReference.takeDamage.OnTakeDamage -= TakeDamage_OnTakeDamage;
    }

    public override void StateUpdate()
    {
        if (requiredReference.groundCheck.IsGrounded())
        {
            requiredReference.stateMachine.SetState(requiredReference.state_Idle);
            return;
        }
    }

    private void Health_OnEmptyHealth()
    {
        if (!requiredReference.stateMachine.IsStateCurrent(this))
            return;

        requiredReference.stateMachine.SetState(requiredReference.state_Die);
    }

    private void TakeDamage_OnTakeDamage(TakedamageEventArgs eventArgs)
    {
        if (!requiredReference.stateMachine.IsStateCurrent(this))
            return;

        if (!eventArgs.isDirect)
            return;

        requiredReference.stateMachine.SetState(requiredReference.state_Stagger);
    }
}
