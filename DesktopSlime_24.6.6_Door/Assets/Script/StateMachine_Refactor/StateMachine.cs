using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    [Header("CURRENT STATE")]
    [Space(10f)]
    [SerializeField]private State currentState = null;

    public void SetState(State stateToSet)
    {
        if(currentState!= null)
        {
            currentState.ExitState();
            currentState.CallExitState();
        }

        currentState = stateToSet;
        currentState.EnterState();
        currentState.CallEnterState();
    }

    private void Update()
    {
        if (currentState != null)
            currentState.StateUpdate();
    }

    private void LateUpdate()
    {
        if (currentState != null)
            currentState.StateLateUpdate();
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.StateFixedUpdate();
    }

    public virtual void StartStateMachine()
    {
        
    }

    public bool IsStateCurrent(State stateToCheck)
    {
        return stateToCheck == currentState;
    }
}
