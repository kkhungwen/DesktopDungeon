using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Slime : MonoBehaviour, IWeapon
{
    [SerializeField] private DamageDataCreator damageDataCreator;
    [SerializeField] private Formula formula;
    [SerializeField] private TagData tagData;
    [SerializeField] private MoveByVelocity moveByVelocity;
    [SerializeField] private BattleVision battleVision;
    [SerializeField] private WeaponDetailsSO slimeWeaponDetails;
    [SerializeField] private Hitbox_ContinuousDamage hitbox_ContinuousDamage;
    [SerializeField] private float attackTimeMin = 0.5f;
    [SerializeField] private float attackTimeMax = 0.8f;

    public event Action OnAttack;
    public event Action OnEndAttack;

    private float attackTime;
    private float updateDamageDataInterval = 0.5f;
    private float updateDamageDataTime;
    private float startAttackTime;
    private float resetCoolDownTime;
    private float attackCooldown = 0;
    private bool isAttacking = false;

    private void Update()
    {
        if (Time.time >= updateDamageDataTime + updateDamageDataInterval)
        {
            updateDamageDataTime = Time.time;
            hitbox_ContinuousDamage.SetUpHitBox(GetDamageData(), tagData.hostileTagList);
        }


        if (isAttacking)
        {
            if (Time.time >= startAttackTime + attackTime)
                AttackEnd();
        }
    }

    public bool Attack()
    {
        if (isAttacking)
            return false;

        // Attack
        isAttacking = true;
        startAttackTime = Time.time;
        attackTime = UnityEngine.Random.Range(attackTimeMin,attackTimeMax);

        Vector2 targetPosition = battleVision.GetCurrentTargetEnemyPoint();
        Vector2 velocity = HelperUtils.GetTrajectoryVelocity(transform.position, targetPosition, attackTime);
        moveByVelocity.MoveRigidBody(velocity, 1, false, false);

        OnAttack?.Invoke();
        return true;
    }

    public void InturruptAttack()
    {
        if (!isAttacking)
            return;

        // End Attack

        isAttacking = false;

        ResetCoolDown();
    }

    public void AttackEnd()
    {
        isAttacking = false;

        // Reset attack cooldown
        ResetCoolDown();

        // Call end attack event
        OnEndAttack?.Invoke();
    }

    private void ResetCoolDown()
    {
        attackCooldown = formula.GetAttackCooldownTime(1);
        resetCoolDownTime = Time.time;
    }

    public DamageData GetDamageData() => damageDataCreator.GetWeaponDamageData(slimeWeaponDetails.knockBackStrength);

    public List<TagSO> GetEnemyTagList() => tagData.hostileTagList;

    public float GetModifiedAttackTime(float defaultAttackTime) => 0;

    public WeaponDetailsSO GetWeaponDetails() => slimeWeaponDetails;

    public bool IsAttacking() => isAttacking;

    public bool IsCooldown() => Time.time < resetCoolDownTime + attackCooldown;

    public void EquipWeapon(WeaponDetailsSO weaponDetails)
    {

    }

    public void UnEquipWeapon()
    {

    }
}
