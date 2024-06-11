using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_Being))]
[DisallowMultipleComponent]
public class State_Move : State
{
    private StateRequiredReference_Being requiredReference;

    private float targetPositionX;
    private float moveSpeed;

    private void Awake()
    {
        requiredReference = GetComponent<StateRequiredReference_Being>();
    }

    public override void StateUpdate()
    {
        // If not grounded enter state fall
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

            if (!requiredReference.battleVision.IsInPreferredMoveToPosition(transform.position.x, out float preferredPositionX))
            {
                targetPositionX = preferredPositionX;

                // Change face direction
                requiredReference.faceDirection.ChangeFaceDirection(targetPositionX > transform.position.x);

                return;
            }
            else
            {
                ResetVelocityToZero();
                requiredReference.stateMachine.SetState(requiredReference.state_Idle);
                return;
            }
        }

        // If arrive at position enter state Idle
        if (requiredReference.patrolVision.IsArrivePatrolTarget(transform.position.x))
        {
            ResetVelocityToZero();

            requiredReference.stateMachine.SetState(requiredReference.state_Idle);
            return;
        }
        else
        {
            // Update target position
            targetPositionX = requiredReference.patrolVision.patrolTargetX;

            // Change face direction
            requiredReference.faceDirection.ChangeFaceDirection(targetPositionX > transform.position.x);
        }
    }

    public override void StateFixedUpdate()
    {
        MoveToPosition(targetPositionX, moveSpeed);
    }

    public override void EnterState()
    {
        requiredReference.playerClickInput.OnStartLeftDrag += PlayerClickInput_OnStartLeftDrag;
        requiredReference.takeDamage.OnTakeDamage += TakeDamage_OnTakeDamage;
        requiredReference.health.OnEmptyHealth += Health_OnEmptyHealth;

        // Set moveSpeed
        moveSpeed = requiredReference.formula.GetAttributeValueModified(GameResources.Instance.moveSpeed_Attribute);

        // Set target position
        requiredReference.patrolVision.SetRandomPatrolTargetX();
        targetPositionX = requiredReference.patrolVision.patrolTargetX;
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

    private void MoveToPosition(float targetPositionX, float moveSpeed)
    {
        if (targetPositionX - transform.position.x > 0)
        {
            requiredReference.moveByVelocity.MoveRigidBody(Vector2.right, moveSpeed, true, false);
            return;
        }

        if (targetPositionX - transform.position.x < 0)
        {
            requiredReference.moveByVelocity.MoveRigidBody(Vector2.left, moveSpeed, true, false);
            return;
        }
    }

    private void ResetVelocityToZero()
    {
        requiredReference.moveByVelocity.MoveRigidBody(Vector2.zero, 0, false, false);
    }
}
