using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffixListener_AddStatusToAttackTarget : MonoBehaviour
{
    private AffixHolder affixHolder;
    [SerializeField] private DamageDataCreator damageDataCreator;
    private Dictionary<AffixTypeSO, int> affixCountDic;

    private void Awake()
    {
        affixCountDic = new();

        affixHolder = GetComponentInParent<AffixHolder>();

        affixHolder.OnAffixUpdate += AffixHolder_OnAffixUpdate;
    }

    private void AffixHolder_OnAffixUpdate(AffixTypeSO affixType, int count)
    {
        if (!affixType.addStatusToAttackTarget)
            return;

        if (affixCountDic.ContainsKey(affixType))
            affixCountDic[affixType] = count;
        else
            affixCountDic.Add(affixType, count);

        List<StatusStackData> statusStackDataList = new List<StatusStackData>();
        foreach (KeyValuePair<AffixTypeSO, int> pair in affixCountDic)
        {
            StatusStackData statusStackData = new StatusStackData(pair.Key.statusType, pair.Value * pair.Key.statusStack);
            statusStackDataList.Add(statusStackData);
        }

        damageDataCreator.UpdateStatusPassingDataStack(this, statusStackDataList);
    }
}
