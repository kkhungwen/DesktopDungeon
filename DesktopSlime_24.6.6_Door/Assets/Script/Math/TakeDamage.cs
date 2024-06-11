using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class TakeDamage : MonoBehaviour
{
    public event Action<TakedamageEventArgs> OnTakeDamage;

    [SerializeField] private Formula formula;
    [SerializeField] private StatusListener_Poison statusListener_Poison;

    private void Awake()
    {
        statusListener_Poison.OnDealPoisonDamage += StatusListener_Poison_OnDealPoisonDamage;
    }

    private void StatusListener_Poison_OnDealPoisonDamage(float damage)
    {
        TakeIndirectDamage(damage);
    }

    public void TakeDirectDamage(DamageData damageData, out float damage)
    {
        damage = formula.GetDamageTaken(damageData.damage);

        OnTakeDamage?.Invoke(new TakedamageEventArgs()
        {
            damage = damage,
            damagePosition = damageData.damagePosition,
            isDamageFromRight = damageData.damagePosition.x - transform.position.x > 0,
            isDirect = true,
            knockBackStrength = damageData.knockBackStrength,
            passingStatusCountDic = damageData.passingStatusCountDic
        });
    }

    private void TakeIndirectDamage(float damage)
    {
        OnTakeDamage?.Invoke(new TakedamageEventArgs()
        { 
            damage = damage
        });
    }
}

public class TakedamageEventArgs : EventArgs
{
    public float damage = 0;
    public Vector2 damagePosition;
    public bool isDamageFromRight;
    public bool isDirect = false;
    public float knockBackStrength = 0;
    public Dictionary<StatusTypeSO, int> passingStatusCountDic;
}
