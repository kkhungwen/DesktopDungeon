using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class PlayerClickInput : MonoBehaviour, IClickable
{
    public event Action<Vector2> OnLeftClick;
    public event Action OnStartLeftDrag;
    public event Action<Vector2> OnLeftDrag;
    public event Action<Vector2> OnLeftMouseUp;
    public event Action OnEnter;
    public event Action OnOver;
    public event Action OnExit;

    // test
    public bool isDebug = false;

    public Vector2 GetWorldPosition()
    {
        return transform.position;
    }

    public void LeftClick(Vector2 position)
    {
        OnLeftClick?.Invoke(position);
    }

    public void StartLeftDrag()
    {
        OnStartLeftDrag?.Invoke();
    }

    public void LeftDrag(Vector2 position, Vector2 positionRelative)
    {
        OnLeftDrag?.Invoke(position);
    }

    public void LeftMouseUp(Vector2 position, Vector2 positionRelative)
    {
        OnLeftMouseUp?.Invoke(position);

        // test
        if (isDebug) { Debug.Log("left mouse up"); }
    }

    public void Enter()
    {
        OnEnter?.Invoke();
    }

    public void Exit()
    {
        OnExit?.Invoke();
    }

    public void Over()
    {
        OnOver?.Invoke();
    }
}
