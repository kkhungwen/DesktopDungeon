using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class AffixListener_CommandersRally : MonoBehaviour
{
    private AffixHolder affixHolder;
    [SerializeField] private AreaHitbox areaHitbox;
    [SerializeField] private SerializableInterface<IWeapon> weapon;
    [SerializeField] private AffixTypeSO commandersRally_AffixType;
    [SerializeField] private StatusTypeSO rally_StatusType;
    [SerializeField] private float effectRadious;

    int affixCount;

    private void Awake()
    {
        affixHolder = GetComponentInParent<AffixHolder>();

        affixHolder.OnAffixUpdate += AffixHolder_OnAffixUpdate;
        weapon.Value.OnAttack += Value_OnAttack;
    }

    private void AffixHolder_OnAffixUpdate(AffixTypeSO affixType, int count)
    {
        if (affixType != commandersRally_AffixType)
            return;

        affixCount = count;
    }

    private void Value_OnAttack()
    {
        if (affixCount <= 0)
            return;

        AddStatusToAllyInRadious();
    }

    private void AddStatusToAllyInRadious()
    {
        List<HurtBox> hurtBoxeList = areaHitbox.GetHurtBoxList(false, true, effectRadious);

        foreach (HurtBox hurtBox in hurtBoxeList)
        {
            hurtBox.AddStatus(rally_StatusType, 1);
        }
    }
}
