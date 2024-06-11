using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDataCreator : MonoBehaviour
{
    [SerializeField] private TagData tagData;
    [SerializeField] private Formula formula;
    [SerializeField] private Dictionary<object, Dictionary<StatusTypeSO, int>> sourceStatusCountDic = new();

    public DamageData GetWeaponDamageData(WeaponDetailsSO weaponDetails)
    {
        float damage = formula.GetDamageOutput(GameResources.Instance.weaponDamageRatio);

        // Create damage data
        DamageData damageData = new DamageData()
        {
            passingStatusCountDic = GetPassingStatusCountDic(),
            damage = damage,
            targetTagList = tagData.hostileTagList,
            knockBackStrength = weaponDetails.knockBackStrength
        };

        return damageData;
    }

    public DamageData GetWeaponDamageData(float knockBackStrength)
    {
        float damage = formula.GetDamageOutput(GameResources.Instance.weaponDamageRatio);

        // Create damage data
        DamageData damageData = new DamageData()
        {
            passingStatusCountDic = GetPassingStatusCountDic(),
            damage = damage,
            targetTagList = tagData.hostileTagList,
            knockBackStrength = knockBackStrength
        };

        return damageData;
    }

    public DamageData GetAbilityDamageData(DamageRatio damageRatio, float knockBackStrength)
    {
        float damage = formula.GetDamageOutput(damageRatio);

        // Create damage data
        DamageData damageData = new DamageData()
        {
            damage = damage,
            targetTagList = tagData.hostileTagList,
            knockBackStrength = knockBackStrength
        };

        return damageData;
    }

    public void UpdateStatusPassingDataStack(object sourceObject, List<StatusStackData> statusStackDataList)
    {
        if (sourceStatusCountDic.ContainsKey(sourceObject))
            sourceStatusCountDic.Remove(sourceObject);

        Dictionary<StatusTypeSO, int> statusCountDic = new();

        foreach (StatusStackData statusStackData in statusStackDataList)
        {
            if (!statusCountDic.ContainsKey(statusStackData.statusType))
                statusCountDic.Add(statusStackData.statusType, statusStackData.stack);
            else
                statusCountDic[statusStackData.statusType] += statusStackData.stack;
        }

        sourceStatusCountDic.Add(sourceObject, statusCountDic);
    }

    private Dictionary<StatusTypeSO, int> GetPassingStatusCountDic()
    {
        Dictionary<StatusTypeSO, int> passingStatusCountDic = new();

        foreach (KeyValuePair<object, Dictionary<StatusTypeSO, int>> p in sourceStatusCountDic)
        {
            foreach (KeyValuePair<StatusTypeSO, int> q in p.Value)
            {
                if (!passingStatusCountDic.TryAdd(q.Key, q.Value))
                    passingStatusCountDic[q.Key] += q.Value;
            }
        }

        return passingStatusCountDic;
    }
}

// Data to pass to damage target
public class DamageData
{
    // For hitbox collider 
    public List<TagSO> targetTagList;

    // For hit target
    public Dictionary<StatusTypeSO, int> passingStatusCountDic;
    public Vector2 damagePosition;
    public float damage;
    public float knockBackStrength;
}

public class StatusStackData
{
    public StatusStackData(StatusTypeSO statusType, int stack)
    {
        this.statusType = statusType;
        this.stack = stack;
    }

    public StatusTypeSO statusType;
    public int stack;
}

