using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class Event_UpdateVision : MonoBehaviour
{
    public event Action<EventArgs_UpdateVision> OnUpdateVision;

    public void CallUpdateVision(EventArgs_UpdateVision args)
    {
        OnUpdateVision?.Invoke(args);
    }
}

public class EventArgs_UpdateVision : EventArgs
{
    public RaycastHit2D[] right_VisbleArray;
    public RaycastHit2D[] left_VisibleArray;

    public float visionDistance = 0f;
}
