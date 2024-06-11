using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeSteal : MonoBehaviour
{
    public event Action<float> OnLifeSteal;

    [SerializeField] private Formula formula;
    [SerializeField] private DealDamage dealDamage;

    private void Awake()
    {
        dealDamage.OnDealDirectDamage += DealDamage_OnDealDirectDamage;
    }

    private void DealDamage_OnDealDirectDamage(float damage)
    {
        float lifeStealAmount = damage * formula.GetAttributeValueModified(GameResources.Instance.lifeSteal_Attribute);

        OnLifeSteal?.Invoke(lifeStealAmount);
    }
}
