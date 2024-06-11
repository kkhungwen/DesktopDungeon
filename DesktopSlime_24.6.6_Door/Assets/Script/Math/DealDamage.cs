using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TNRD;

public class DealDamage : MonoBehaviour
{
    public event Action<float> OnDealDirectDamage;

    [SerializeField] private SerializableInterface<IHitBox> weaponHitBox;

    private void Awake()
    {
        weaponHitBox.Value.OnDealDamage += WeaponHitBox_OnDealDamage;
    }

    private void WeaponHitBox_OnDealDamage(float damage)
    {
        OnDealDirectDamage?.Invoke(damage);
    }

    // Called by projectiles to notified damage delt
    public void CallDealDirectDamage(float damage)
    {
        OnDealDirectDamage?.Invoke(damage);
    }
}
