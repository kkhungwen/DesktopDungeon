using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class StateRequiredReference_Being : MonoBehaviour
{
    public StateMachine stateMachine;

    public State state_Idle;
    public State state_Move;
    public State state_Drag;
    public State state_Fall;
    public State state_Attack;
    public State state_Stagger;
    public State state_Die;

    public SerializableInterface<IInstantiatedBeing> intantiatedBeing;
    public PlayerClickInput playerClickInput;
    public TagData tagData;
    public Formula formula;
    public TakeDamage takeDamage;
    public KnockBack knockBack;
    public GroundCheck groundCheck;
    public MoveByVelocity moveByVelocity;
    public FollowMousePosition followMousePosition;
    public FaceDirection faceDirection;
    public PatrolVision patrolVision;
    public BattleVision battleVision;
    public SerializableInterface<IWeapon> weapon;
    public Health health;
    public Collider2D collisionCollider;
    public Tags tags;
}
