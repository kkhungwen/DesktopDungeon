using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipDataSave: IDataSave
{
    public EquipDataSave(EquipData equipData)
    {
        equipDetailsID = SingletonReference.Instance.scriptableObjectIDManager.GetScriptableObjectID(equipData.equipDetails as ScriptableObject);
        itemLevel = equipData.itemLevel;
        forgeLevel = equipData.forgeLevel;

        bonusAttributeIDValueList = new();
        foreach (AttributeValueData attributeValueData in equipData.GetBonusAttributeList())
        {
            int attributeID = SingletonReference.Instance.scriptableObjectIDManager.GetScriptableObjectID(attributeValueData.attributeType);
            AttributeIDValueSave attributeIDValueSave = new(attributeID, attributeValueData.value);
            bonusAttributeIDValueList.Add(attributeIDValueSave);
        }

        bonusAffixTypeIDList = new();
        foreach (AffixTypeSO affixType in equipData.GetBonusAffix())
        {
            int affixID = SingletonReference.Instance.scriptableObjectIDManager.GetScriptableObjectID(affixType);
            bonusAffixTypeIDList.Add(affixID);
        }
    }

    public int equipDetailsID;
    public int itemLevel;
    public int forgeLevel;
    public List<AttributeIDValueSave> bonusAttributeIDValueList;
    public List<int> bonusAffixTypeIDList;

    public EquipData CreateEquipData()
    {
        IEquipDetails equipDetails = SingletonReference.Instance.scriptableObjectIDManager.GetScriptableObject<IEquipDetails>(equipDetailsID);

        List<AttributeValueData> bonusAttributeValueDataList = new();
        foreach (AttributeIDValueSave attributeIDValueSave in bonusAttributeIDValueList)
        {
            AttributeTypeSO attributeType = SingletonReference.Instance.scriptableObjectIDManager.GetScriptableObject<AttributeTypeSO>(attributeIDValueSave.attributeID);
            AttributeValueData attributeValueData = new(attributeType, attributeIDValueSave.value);
            bonusAttributeValueDataList.Add(attributeValueData);
        }

        List<AffixTypeSO> bonusAffixTypeList = new();
        foreach (int affixTypeID in bonusAffixTypeIDList)
        {
            AffixTypeSO affixType = SingletonReference.Instance.scriptableObjectIDManager.GetScriptableObject<AffixTypeSO>(affixTypeID);
            bonusAffixTypeList.Add(affixType);
        }

        EquipData equipData = new(equipDetails, itemLevel, forgeLevel, bonusAttributeValueDataList, bonusAffixTypeList);

        return equipData;
    }

    public ISaveableData CreateData()
    {
        EquipData equipData = CreateEquipData();
        return equipData;
    }
}

[System.Serializable]
public class AttributeIDValueSave
{
    public AttributeIDValueSave(int attributeID, float value)
    {
        this.attributeID = attributeID;
        this.value = value;
    }

    public int attributeID;
    public float value;
}
