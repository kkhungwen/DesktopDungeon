using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_Being))]
[RequireComponent(typeof(Animator))]
public class Animate_Humanoid : MonoBehaviour
{
    StateRequiredReference_Being stateRequiredReference;
    Animator anima;

    private void Awake()
    {
        stateRequiredReference = GetComponent<StateRequiredReference_Being>();
        anima = GetComponent<Animator>();
    }

    private void Start()
    {
        stateRequiredReference.state_Idle.OnEnterState += State_Idle_OnEnterState;
        stateRequiredReference.state_Move.OnEnterState += State_Move_OnEnterState;
        stateRequiredReference.state_Drag.OnEnterState += State_Drag_OnEnterState;
        stateRequiredReference.state_Fall.OnEnterState += State_Fall_OnEnterState;
        stateRequiredReference.state_Die.OnEnterState += State_Die_OnEnterState;
    }

    private void State_Die_OnEnterState()
    {
        anima.Play(Settings.die);
    }

    private void State_Fall_OnEnterState()
    {
        anima.Play(Settings.fall);
    }

    private void State_Drag_OnEnterState()
    {
        anima.Play(Settings.drag);
    }

    private void State_Move_OnEnterState()
    {
        anima.Play(Settings.move);
    }

    private void State_Idle_OnEnterState()
    {
        anima.Play(Settings.idle);
    }
}
