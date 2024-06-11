using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Attack_Bow : MonoBehaviour, IAttack
{
    private Animator anima;
    private IWeapon weapon;

    [Space(10f)]
    [Header("REQUIRED REFERENCE")]
    [SerializeField] private ProjectileCreator projectileCreator;
    [SerializeField] private Vector2 launchPosition;
    [SerializeField] private SpriteRenderer sprite_Weapon;
    [SerializeField] private SpriteRenderer sprite_HandNoWeapon;
    [SerializeField] private SpriteRenderer sprite_Arrow;

    [Space(10f)]
    [Header("CONFIGURABLES")]
    [SerializeField] private float defaultAttackTime;
    [SerializeField] private float enterTimePortion = 0.5f;

    private float modifiedAttackTime;
    private bool isBowFired;
    private float resetAttackTime = 0;

    private void Awake()
    {
        anima = GetComponentInParent<Animator>();
        weapon = GetComponent<IWeapon>();
    }

    public void WeaponUpdate()
    {
        if (Time.time >= resetAttackTime + modifiedAttackTime * enterTimePortion && !isBowFired)
        {
            if (weapon.GetWeaponDetails().projectileDetails != null)
                projectileCreator.CreateProjectile(transform.position + (Vector3)launchPosition, weapon.GetWeaponDetails().projectileDetails, Vector2.right, weapon.GetWeaponDetails().projectileRange, weapon.GetDamageData());

            sprite_Arrow.enabled = false;

            // Fire bow
            isBowFired = true;

            // Set animator
            anima.Play("Attack_Bow_Exit");

            return;
        }

        if (!isBowFired)
            return;

        if (Time.time >= resetAttackTime + modifiedAttackTime)
        {
            EndAttack();
        }
    }

    public void SetUpAttack(WeaponDetailsSO weaponDetails)
    {
        sprite_Arrow.sprite = weaponDetails.arrowSprite;
        sprite_Arrow.enabled = false;

        EndAttack();
    }

    public void SetDownAttack()
    {
        EndAttack();

        sprite_Arrow.sprite = null;
        sprite_Arrow.enabled = false;
    }

    public void Attack()
    {
        // Set sprite
        sprite_Weapon.sprite = weapon.GetWeaponDetails().attackSprite;
        sprite_HandNoWeapon.sortingOrder = 20;
        sprite_Arrow.enabled = true;

        // Reset parameters
        modifiedAttackTime = weapon.GetModifiedAttackTime(defaultAttackTime);
        resetAttackTime = Time.time;
        isBowFired = false;

        // Set animator
        anima.Play("Attack_Bow_Active");
    }

    public void EndAttack()
    {
        // Set sprite
        sprite_Weapon.sprite = weapon.GetWeaponDetails().activeSprite;
        sprite_HandNoWeapon.sortingOrder = -10;
        sprite_Arrow.enabled = false;

        // Notify weapon the attack is end
        weapon.AttackEnd();
    }
}
