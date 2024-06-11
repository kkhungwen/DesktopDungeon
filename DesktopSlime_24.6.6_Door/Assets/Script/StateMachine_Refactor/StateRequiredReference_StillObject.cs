using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class StateRequiredReference_StillObject : MonoBehaviour
{
    public State state_Idle;
    public State state_Drag;
    public StateMachine stateMachine;

    public PlayerClickInput clickInput_Object;
    public MoveByVelocity moveByVelocity;
    public FollowMousePosition followMousePosition;
    public DragObject dragObject;
    public InstantiatedObjectInspecControl InstantiatedObjectInspecControl;
    public SerializableInterface<IDropable> dropable;
}
