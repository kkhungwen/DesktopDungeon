using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class Event_EndAttack : MonoBehaviour
{
    public event Action OnEndAttack;

    public void CallEndAttack()
    {
        OnEndAttack?.Invoke();
    }
}
