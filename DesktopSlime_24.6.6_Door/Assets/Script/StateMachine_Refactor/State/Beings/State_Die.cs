using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateRequiredReference_Being))]
[DisallowMultipleComponent]
public class State_Die : State
{
    private StateRequiredReference_Being requiredReference;

    private void Awake()
    {
        requiredReference = GetComponent<StateRequiredReference_Being>();
    }

    public override void EnterState()
    {
        requiredReference.knockBack.isAble = false;

        requiredReference.tagData.AddSelfTag(GameResources.Instance.dead_Tag);

        requiredReference.intantiatedBeing.Value.Die();
    }

    public override void ExitState()
    {
        requiredReference.knockBack.isAble = true;

        requiredReference.tagData.RemoveSelfTag(GameResources.Instance.dead_Tag);
    }

    public override void StateUpdate()
    {
        // test
        if (Input.GetKeyDown(KeyCode.R))
        {
            requiredReference.health.FullHealth();
            requiredReference.stateMachine.SetState(requiredReference.state_Idle);
        }
        // test
    }
}
