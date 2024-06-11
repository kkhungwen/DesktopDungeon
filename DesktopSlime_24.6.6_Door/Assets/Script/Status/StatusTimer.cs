using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusTimer
{
    public StatusTimer(float duration)
    {
        this.duration = duration;
    }

    public float duration = 0f;
}
