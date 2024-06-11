using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_Being))]
[DisallowMultipleComponent]
public class State_Idle : State
{
    private StateRequiredReference_Being requiredReference;

    private const float idleTimeMin = 1f;
    private const float idleTimeMax = 5f;

    private float idleTime = 1f;
    private float startTime;

    private void Awake()
    {
        requiredReference = GetComponent<StateRequiredReference_Being>();
    }

    public override void StateUpdate()
    {
        if (!requiredReference.groundCheck.IsGrounded())
        {
            requiredReference.stateMachine.SetState(requiredReference.state_Fall);
            return;
        }

        if (requiredReference.battleVision.isEnemyInVision)
        {
            if (requiredReference.battleVision.IsInAttackRange(transform.position.x) && !requiredReference.weapon.Value.IsCooldown())
            {
                requiredReference.stateMachine.SetState(requiredReference.state_Attack);
                return;
            }

            if (!requiredReference.battleVision.IsInPreferredPosition(transform.position.x))
            {
                requiredReference.stateMachine.SetState(requiredReference.state_Move);
                return;
            }
        }

        if (Time.time >= startTime + idleTime)
        {
            requiredReference.stateMachine.SetState(requiredReference.state_Move);
            return;
        }
    }

    public override void EnterState()
    {
        requiredReference.playerClickInput.OnStartLeftDrag += PlayerClickInput_OnStartLeftDrag;
        requiredReference.takeDamage.OnTakeDamage += TakeDamage_OnTakeDamage;
        requiredReference.health.OnEmptyHealth += Health_OnEmptyHealth;

        idleTime = Random.Range(idleTimeMin, idleTimeMax);
        startTime = Time.time;
    }

    public override void ExitState()
    {
        requiredReference.playerClickInput.OnStartLeftDrag -= PlayerClickInput_OnStartLeftDrag;
        requiredReference.takeDamage.OnTakeDamage -= TakeDamage_OnTakeDamage;
        requiredReference.health.OnEmptyHealth -= Health_OnEmptyHealth;
    }

    private void Health_OnEmptyHealth()
    {
        if (!requiredReference.stateMachine.IsStateCurrent(this))
            return;

        requiredReference.stateMachine.SetState(requiredReference.state_Die);
    }

    private void PlayerClickInput_OnStartLeftDrag()
    {
        if (!requiredReference.stateMachine.IsStateCurrent(this))
            return;

        requiredReference.stateMachine.SetState(requiredReference.state_Drag);
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
