using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipData : IInventoryObjectData, IInspecable, ISaveableData
{
    public EquipData(IEquipDetails equipDetails)
    {
        if (equipDetails == null)
            Debug.Log("equipDetails annot be null");
        this.equipDetails = equipDetails;
        itemLevel = 0;
        forgeLevel = 0;
        bonusAttributeValueDataList = new();
        bonusAffixTypeList = new();
    }

    public EquipData(IEquipDetails equipDetails, int itemLevel, int forgeLevel, List<AttributeValueData> bonusAttributeValueDataList, List<AffixTypeSO> bonusAffixTypeList)
    {
        if (equipDetails == null)
            Debug.Log("equipDetails annot be null");
        this.equipDetails = equipDetails;
        this.itemLevel = itemLevel;
        this.forgeLevel = forgeLevel;
        this.bonusAttributeValueDataList = bonusAttributeValueDataList;
        this.bonusAffixTypeList = bonusAffixTypeList;
    }

    public IDataSave CreateDataSave()
    {
        EquipDataSave equipDataSave = new EquipDataSave(this);
        return equipDataSave;
    }

    public void CreateInstantiatedObject(Vector2 position)
    {
        InstantiatedEquip.CreateInstantiatedObject(this, position, 0);
    }

    public event Action<EquipData> OnUpdateEquipData;

    public IEquipDetails equipDetails { get; private set; }
    public int itemLevel { get; private set; }
    public int forgeLevel { get; private set; }
    private List<AttributeValueData> bonusAttributeValueDataList;
    private List<AffixTypeSO> bonusAffixTypeList;

    public bool Upgrade()
    {
        if (itemLevel >= Settings.max_ItemLevel)
            return false;

        if (itemLevel >= (forgeLevel + 1) * Settings.itemLevelPerForge)
            return false;

        itemLevel++;

        // add bonus stat every 3 item level
        if (itemLevel % Settings.itemLevelPerBonusStat == 0)
            AddBonusAttribute();

        OnUpdateEquipData?.Invoke(this);

        return true;
    }

    public bool Forge(EquipData equipData)
    {
        if (equipData.equipDetails != equipDetails)
            return false;

        if (forgeLevel >= Settings.max_ForgeLevel)
            return false;

        if (equipData.forgeLevel != forgeLevel)
            return false;

        if (itemLevel < (forgeLevel + 1) * Settings.itemLevelPerForge)
            return false;

        forgeLevel++;

        AddBonusAffix();

        OnUpdateEquipData?.Invoke(this);

        return true;
    }

    private void AddBonusAttribute()
    {
        AttributeValueData[] bonusAttributeValueDataArray = GameResources.Instance.bonusAttributeDataArray;

        if (bonusAttributeValueDataArray.Length == 0)
            return;

        int index = UnityEngine.Random.Range(0, bonusAttributeValueDataArray.Length);

        foreach (AttributeValueData attributeValueData in bonusAttributeValueDataList)
        {
            if (attributeValueData.attributeType != bonusAttributeValueDataArray[index].attributeType)
                continue;

            attributeValueData.value += bonusAttributeValueDataArray[index].value;
            return;
        }

        AttributeValueData newAttributeValueData = new AttributeValueData(bonusAttributeValueDataArray[index].attributeType, bonusAttributeValueDataArray[index].value);
        bonusAttributeValueDataList.Add(newAttributeValueData);
    }

    private void AddBonusAffix()
    {
        AffixTypeSO[] bonusAffixTypeArray = GameResources.Instance.bonusAffixTypeArray;

        if (bonusAffixTypeArray.Length == 0)
            return;

        int index = UnityEngine.Random.Range(0, bonusAffixTypeArray.Length);
        bonusAffixTypeList.Add(bonusAffixTypeArray[index]);
    }

    public IEnumerable<AttributeValueData> GetAttribute()
    {
        foreach (AttributeValueData attributeValueData in bonusAttributeValueDataList)
            yield return attributeValueData;

        EquipAttribute equipAttribute = equipDetails.GetEquipAttribute();
        yield return new AttributeValueData(equipAttribute.attributeType, equipAttribute.GetValue(itemLevel));
    }

    public List<AttributeValueData> GetBonusAttributeList() => bonusAttributeValueDataList;

    public AttributeValueData GetEquipAttribute()
    {
        EquipAttribute equipAttribute = equipDetails.GetEquipAttribute();
        return new AttributeValueData(equipAttribute.attributeType, equipAttribute.GetValue(itemLevel));
    }

    public IEnumerable<AffixTypeSO> GetAffix()
    {
        foreach (AffixTypeSO affixType in bonusAffixTypeList)
            yield return affixType;

        foreach (AffixTypeSO affixType in equipDetails.GetAffixTypeArray())
            yield return affixType;
    }

    public List<AffixTypeSO> GetBonusAffix()
    {
        return bonusAffixTypeList;
    }

    public WeaponDetailsSO GetWeaponDetails()
    {
        WeaponDetailsSO weaponDetails = equipDetails as WeaponDetailsSO;

        if (weaponDetails == null)
        {
            Debug.Log("can't cast equip details to weapon details");
            return null;
        }

        return weaponDetails;
    }

    public ArmorDetailsSO GetArmorDetails()
    {
        ArmorDetailsSO armorDetails = equipDetails as ArmorDetailsSO;

        if (armorDetails == null)
        {
            Debug.Log("cant cast equip details to armor setails");
            return null;
        }

        return armorDetails;
    }

    public EquipTypeSO GetEquipType() => equipDetails.GetEquipType();

    public Sprite GetInstantiatedSprite() => equipDetails.GetInstantiatedSprite();

    public Sprite GetIconSprite() => equipDetails.GetIconSprite();

    public bool CreatMouseOverInspec(Transform transform, Vector2 localPosition, out IInspec inspec)
    {
        inspec = EquipMouseOverInspec.CreateEquipInspec(transform, localPosition, this);
        return true;
    }

    public bool CreateClickInspec(Transform transform, Vector2 localPosition, out IInspec inspec)
    {
        inspec = null;
        return false;
    }
}
