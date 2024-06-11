using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TNRD;

public class AffixHolder : MonoBehaviour
{
    [SerializeField] private SerializableInterface<IEquipments> equipments;
    private Dictionary<object, List<AffixTypeSO>> sourceAffixDictionary = new();
    public event Action<AffixTypeSO, int> OnAffixUpdate;

    private void Awake()
    {
        if (equipments.Value != null)
            equipments.Value.OnEquipmentsAffixChange += Equipemts_OnEquipmentsAffixChange;
    }

    private void Equipemts_OnEquipmentsAffixChange(List<AffixTypeSO> affixList)
    {
        UpdateAffix(equipments, affixList);
    }

    private void UpdateAffix(object sourceObject, List<AffixTypeSO> affixList)
    {
        List<AffixTypeSO> oldAffixList = sourceAffixDictionary.GetValueOrDefault(sourceObject);

        if (!sourceAffixDictionary.TryAdd(sourceObject, affixList))
        {
            sourceAffixDictionary[sourceObject] = affixList;

            foreach (AffixTypeSO affixType in oldAffixList)
                OnAffixUpdate?.Invoke(affixType, GetAffixCount(affixType));
        }

        foreach (AffixTypeSO affixType in affixList)
            OnAffixUpdate?.Invoke(affixType, GetAffixCount(affixType));
    }

    private int GetAffixCount(AffixTypeSO affixType)
    {
        int count = 0;

        foreach (KeyValuePair<object, List<AffixTypeSO>> pair in sourceAffixDictionary)
            foreach (AffixTypeSO affix in pair.Value)
            {
                if (affix == affixType)
                    count++;
            }

        return count;
    }
}
