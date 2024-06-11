using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;
using System;

public class AffixListener_AddStatusToSelf : MonoBehaviour
{
    public event Action<StatusTypeSO, int> OnAddStatusToSelf;

    private AffixHolder affixManager;
    [SerializeField] private TakeDamage takeDamage;
    [SerializeField] private SerializableInterface<IWeapon> weapon;

    private Dictionary<AffixTypeSO,int> onAttackAdd_AffixCountDic = new();
    private Dictionary<AffixTypeSO,int> onGetHitAdd_AffixCountDic = new();

    private void Awake()
    {
        affixManager = GetComponentInParent<AffixHolder>();

        weapon.Value.OnAttack += Weapon_OnAttack;
        takeDamage.OnTakeDamage += TakeDamage_OnTakeDamage;

        affixManager.OnAffixUpdate += AffixManager_OnAffixUpdate;
    }

    private void AffixManager_OnAffixUpdate(AffixTypeSO affixType, int count)
    {
        if (affixType.onAttackAddStatusToSelf && affixType.statusType != null)
            UpdateAffixDic(onAttackAdd_AffixCountDic,affixType, count);

        if (affixType.onGetHitAddStatusToSelf && affixType.statusType != null)
            UpdateAffixDic(onGetHitAdd_AffixCountDic, affixType, count);
    }

    private void UpdateAffixDic(Dictionary<AffixTypeSO,int> dic, AffixTypeSO affixType, int count)
    {
        if (!dic.ContainsKey(affixType))
        {
            dic.Add(affixType, count);
            return;
        }

        dic[affixType] = count;
    }

    private void Weapon_OnAttack()
    {
        foreach (KeyValuePair<AffixTypeSO,int> pair in onAttackAdd_AffixCountDic)
        {
            OnAddStatusToSelf?.Invoke(pair.Key.statusType,pair.Key.statusStack*pair.Value);
        }
    }

    private void TakeDamage_OnTakeDamage(TakedamageEventArgs args)
    {
        if (!args.isDirect)
            return;

        foreach (KeyValuePair<AffixTypeSO, int> pair in onGetHitAdd_AffixCountDic)
        {
            OnAddStatusToSelf?.Invoke(pair.Key.statusType, pair.Key.statusStack * pair.Value);
        }
    }

}
