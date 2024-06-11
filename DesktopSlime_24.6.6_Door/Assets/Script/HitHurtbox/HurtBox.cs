using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HurtBox : MonoBehaviour
{
    public event Action<StatusTypeSO, int> OnAddStatus;

    [SerializeField] private TakeDamage takeDamage;

    public void GetAttack(DamageData damageData, out float damage)
    {
        takeDamage.TakeDirectDamage(damageData, out damage);
    }

    public void AddStatus(StatusTypeSO statusType, int stack)
    {
        OnAddStatus?.Invoke(statusType, stack);
    }
}

public class HurtBoxTriggerEventArgs : EventArgs
{
    public DamageData damageData;
}