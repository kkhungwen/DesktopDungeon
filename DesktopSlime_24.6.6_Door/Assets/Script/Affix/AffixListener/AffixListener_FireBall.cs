using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class AffixListener_FireBall : MonoBehaviour
{
    private AffixHolder affixHolder;
    [SerializeField] private AbilityCooldownManager abilityCooldownManager;
    [SerializeField] private FaceDirection faceDirection;
    [SerializeField] private ProjectileCreator projectileCreator;
    [SerializeField] private ProjectileDetailsSO fireBallDetails;
    [SerializeField] private DamageDataCreator damageDataCreator;
    [SerializeField] private SerializableInterface<IWeapon> weapon;
    [SerializeField] private AffixTypeSO fireBall_AffixType;
    [SerializeField] private DamageRatio firBallDamageRatio;
    [SerializeField] private float abilityCooldownTime;
    [SerializeField] private float fireBallRange;
    [SerializeField] private float knockBackStrength;

    private AbilityCooldown abilityCooldown;
    private int affixCount;

    private void Awake()
    {
        affixHolder = GetComponentInParent<AffixHolder>();

        affixHolder.OnAffixUpdate += AffixHolder_OnAffixUpdate;
        weapon.Value.OnAttack += Value_OnAttack;

        abilityCooldown = abilityCooldownManager.CreateAbilityCooldown(abilityCooldownTime);
    }

    private void AffixHolder_OnAffixUpdate(AffixTypeSO affixType, int count)
    {
        if (affixType != fireBall_AffixType)
            return;

        Debug.Log("count");

        affixCount = count;
    }

    private void Value_OnAttack()
    {
        if (affixCount <= 0)
            return;

        if (abilityCooldown.isCooldown)
            return;
        abilityCooldown.ResetCooldownTime();

        CastFireBall();
    }

    private void CastFireBall()
    {
        Debug.Log("FirBall!!");

        DamageData damageData = damageDataCreator.GetAbilityDamageData(firBallDamageRatio, knockBackStrength);

        Vector2 direction;
        if (faceDirection.isRight)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        projectileCreator.CreateProjectile(transform.position, fireBallDetails, direction, fireBallRange, damageData);
    }
}
