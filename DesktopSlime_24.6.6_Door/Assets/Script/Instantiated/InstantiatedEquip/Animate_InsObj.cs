using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_StillObject))]
[RequireComponent(typeof(Animator))]
public class Animate_InsObj : MonoBehaviour
{
    private StateRequiredReference_StillObject stateRequiredReference;
    private Animator animator;

    private void Awake()
    {
        stateRequiredReference = GetComponent<StateRequiredReference_StillObject>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stateRequiredReference.state_Idle.OnEnterState += State_Idle_OnEnterState;
        stateRequiredReference.state_Drag.OnEnterState += State_Drag_OnEnterState;
    }

    private void State_Idle_OnEnterState()
    {
        animator.Play(Settings.idle);
    }

    private void State_Drag_OnEnterState()
    {
        animator.Play(Settings.drag);
    }
}
