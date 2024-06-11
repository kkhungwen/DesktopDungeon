using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TakeHeal : MonoBehaviour
{
    public event Action<float> OnHeal;

    [SerializeField] private LifeSteal lifeSteal;

    private void Awake()
    {
        lifeSteal.OnLifeSteal += LifeSteal_OnLifeSteal;
    }

    private void LifeSteal_OnLifeSteal(float amount)
    {
        OnHeal?.Invoke(amount);
    }
}
