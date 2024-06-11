using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Door : StateMachine
{
    [HideInInspector] public State_Chest_Idle state_Idle;
    [HideInInspector] public State_Chest_Drag state_Drag;

    [Header("REQUIRED REFERENCE")]
    [Space(10f)]
    public DragObject dragObject;
    public PlayerClickInput clickInput_Object;
    public MoveByVelocity moveByVelocity;
    public FollowMousePosition followMousePosition;
    public InstantiatedChest instantiatedChest;

    private void Awake()
    {
        state_Idle = GetComponent<State_Chest_Idle>();
        state_Drag = GetComponent<State_Chest_Drag>();
    }

    public override void StartStateMachine()
    {
        SetState(state_Idle);
    }

    public ChestData GetChestData()
    {
        return instantiatedChest.GetChestData();
    }
}
