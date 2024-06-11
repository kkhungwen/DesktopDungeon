using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_Being))]
[DisallowMultipleComponent]
public class State_Attack : State
{
    private StateRequiredReference_Being requiredReference;

    private void Awake()
    {
        requiredReference = GetComponent<StateRequiredReference_Being>();
    }

    public override void EnterState()
    {
        // Subscribe to event
        requiredReference.weapon.Value.OnEndAttack += Weapon_OnEndAttack;
        requiredReference.health.OnEmptyHealth += Health_OnEmptyHealth;

        // Call weapon attack
        if (!requiredReference.weapon.Value.Attack())
        {
            requiredReference.stateMachine.SetState(requiredReference.state_Idle);
            return;
        }

        // Set face direction
        requiredReference.faceDirection.ChangeFaceDirection(requiredReference.battleVision.isTargetEnemyRight);
    }


    public override void ExitState()
    {
        // Unsubscribe to event
        requiredReference.weapon.Value.OnEndAttack -= Weapon_OnEndAttack;
        requiredReference.health.OnEmptyHealth -= Health_OnEmptyHealth;

        requiredReference.weapon.Value.InturruptAttack();
    }


    private void Health_OnEmptyHealth()
    {
        if (!requiredReference.stateMachine.IsStateCurrent(this))
            return;

        requiredReference.stateMachine.SetState(requiredReference.state_Die);
    }

    private void Weapon_OnEndAttack()
    {
        if (!requiredReference.stateMachine.IsStateCurrent(this))
            return;

        requiredReference.stateMachine.SetState(requiredReference.state_Idle);
    }
}
