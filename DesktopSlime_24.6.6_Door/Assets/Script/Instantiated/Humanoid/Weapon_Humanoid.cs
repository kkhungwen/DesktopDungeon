using System.Collections.Generic;
using UnityEngine;
using System;
using TNRD;

[DisallowMultipleComponent]
[RequireComponent(typeof(Attack_Swing))]
[RequireComponent(typeof(Attack_Bow))]
[RequireComponent(typeof(Attack_Stab))]
public class Weapon_Humanoid : MonoBehaviour, IWeapon
{
    public event Action OnAttack;
    // Event to notify attack caller that the attack was finished
    public event Action OnEndAttack;

    [Header("REQUIRED REFERENCE")]
    [Space(10f)]
    [SerializeField] private WeaponDetailsSO defaultWeaponDetails;
    [SerializeField] private SerializableInterface<IEquipments> equipments;
    [SerializeField] private DamageDataCreator damageDataCreator;
    [SerializeField] private Formula formula;
    [SerializeField] private TagData tagData;
    [SerializeField] private SpriteRenderer animated_Sprite_Weapon;

    private Attack_Swing attack_Swing;
    private Attack_Bow attack_Bow;
    private Attack_Stab attack_Stab;

    private WeaponDetailsSO weaponDetails;
    private IAttack currentAttack;
    private float resetCoolDownTime;
    private float attackCooldown = 0;
    private bool isAttacking = false;

    private void Awake()
    {
        attack_Swing = GetComponent<Attack_Swing>();
        attack_Bow = GetComponent<Attack_Bow>();
        attack_Stab = GetComponent<Attack_Stab>();

        equipments.Value.OnEquip += Equipments_OnEquip;
        equipments.Value.OnUnEquip += Equipments_OnUnEquip;
    }

    public void SetUp()
    {
        EquipDefaultWeapon();
    }

    private void Equipments_OnUnEquip(OnUnEquipEventArgs args)
    {
        if (args.equipData.GetEquipType() != GameResources.Instance.weapon_Equip)
            return;

        UnEquipWeapon();
        EquipDefaultWeapon();
    }

    private void Equipments_OnEquip(OnEquipEventArgs args)
    {
        if (args.equipData.GetEquipType() != GameResources.Instance.weapon_Equip)
            return;
        EquipWeapon(args.equipData.GetWeaponDetails());
    }

    private void Update()
    {
        if (currentAttack != null && isAttacking)
            currentAttack.WeaponUpdate();
    }

    public void EquipWeapon(WeaponDetailsSO weaponDetails)
    {
        this.weaponDetails = weaponDetails;

        // Set down current attack
        if (currentAttack != null)
            currentAttack.SetDownAttack();

        // Update current attack
        if (weaponDetails.weaponType == GameResources.Instance.swing_Weapon)
        {
            currentAttack = attack_Swing;
        }
        else if (weaponDetails.weaponType == GameResources.Instance.bow_Weapon)
        {
            currentAttack = attack_Bow;
        }
        else if (weaponDetails.weaponType == GameResources.Instance.stab_Weapon)
        {
            currentAttack = attack_Stab;
        }
        else
        {
            Debug.Log("weaponDetails should contains a weapon type");
        }

        // Set up current attack
        currentAttack.SetUpAttack(weaponDetails);

        // Set up sprite
        animated_Sprite_Weapon.sprite = weaponDetails.activeSprite;

        // Reset attack cooldown
        ResetCoolDown();
    }

    public void UnEquipWeapon()
    {
        // Set down current attack
        if (currentAttack != null)
        {
            currentAttack.SetDownAttack();
            currentAttack = null;
        }

        // Set up sprite
        animated_Sprite_Weapon.sprite = null;

        weaponDetails = null;
    }

    // Called by attack state to start an attack
    public bool Attack()
    {
        if (isAttacking)
            return false;

        currentAttack.Attack();

        isAttacking = true;
        OnAttack?.Invoke();
        return true;
    }

    // Inturrupt current attack
    public void InturruptAttack()
    {
        if (!isAttacking)
            return;

        currentAttack.EndAttack();

        isAttacking = false;

        // Reset attack cooldown
        ResetCoolDown();
    }

    // Called by current attack to end an attack
    public void AttackEnd()
    {
        isAttacking = false;

        // Reset attack cooldown
        ResetCoolDown();

        // Call end attack event
        OnEndAttack?.Invoke();
    }

    private void EquipDefaultWeapon()
    {
        EquipWeapon(defaultWeaponDetails);
    }

    private void ResetCoolDown()
    {
        attackCooldown = formula.GetAttackCooldownTime(weaponDetails.attackSpeedRatio);
        resetCoolDownTime = Time.time;
    }

    public float GetModifiedAttackTime(float defaultAttackTime) => formula.GetModifiedAttackTime(defaultAttackTime);

    public WeaponDetailsSO GetWeaponDetails() => weaponDetails;

    public DamageData GetDamageData() => damageDataCreator.GetWeaponDamageData(weaponDetails);

    public bool IsCooldown() => Time.time < resetCoolDownTime + attackCooldown;

    public bool IsAttacking() => isAttacking;

    public List<TagSO> GetEnemyTagList() => tagData.hostileTagList;
}
