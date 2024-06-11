using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(State_InsEquip_Idle))]
[RequireComponent(typeof(State_InsEquip_Drag))]
public class SM_InstantiatedEquip : StateMachine
{
    [HideInInspector] public State_InsEquip_Idle state_Idle;
    [HideInInspector] public State_InsEquip_Drag state_Drag;

    [Header("REQUIRED REFERENCE")]
    [Space(10f)]
    public PlayerClickInput clickInput_Object;
    public MoveByVelocity moveByVelocity;
    public FollowMousePosition followMousePosition;
    public DragObject dragObject;
    public InstantiatedObjectInspecControl InstantiatedObjectInspecControl;
    public InstantiatedEquip instantiatedEquip;

    private void Awake()
    {
        state_Idle = GetComponent<State_InsEquip_Idle>();
        state_Drag = GetComponent<State_InsEquip_Drag>();
    }

    public override void StartStateMachine()
    {
        SetState(state_Idle);
    }

    public EquipData GetInsEquipData()
    {
        return instantiatedEquip.GetEquipData();
    }
}
