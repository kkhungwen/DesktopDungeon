using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IWeapon
{
    public event Action OnAttack;
    public event Action OnEndAttack;

    //___________________________Called by Equipments______________________________
    public void EquipWeapon(WeaponDetailsSO weaponDetails);

    public void UnEquipWeapon();

    // ___________________________Called by others_________________________________
    public bool Attack();

    public void InturruptAttack();

    public bool IsCooldown();

    public bool IsAttacking();

    // ___________________________Called by attack_________________________________
    public WeaponDetailsSO GetWeaponDetails();
    
    public DamageData GetDamageData();

    public List<TagSO> GetEnemyTagList();

    public float GetModifiedAttackTime(float defaultAttackTime);

    public void AttackEnd();
}
