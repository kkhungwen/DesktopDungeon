using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable 
{
    public Vector2 GetWorldPosition();

    public void LeftClick(Vector2 position);

    public void StartLeftDrag();

    public void LeftDrag(Vector2 position, Vector2 positionRelative);

    public void LeftMouseUp(Vector2 position, Vector2 positionRelative);

    public void Enter();

    public void Exit();

    public void Over();
}
