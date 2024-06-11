using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttributeModifyer : MonoBehaviour
{
    public event Action<AttributeModifyEventArgs> OnAttributeModify;

    private Dictionary<AttributeTypeSO, List<ModifyAttribute>> attributeTypeModifyListDic;
    private Dictionary<object, List<ModifyAttribute>> sourceModifyListDic;

    private void Awake()
    {
        attributeTypeModifyListDic = new();
        sourceModifyListDic = new();
    }

    public bool TryGetAttrubuteModifyValue(AttributeTypeSO attributeType, out float addValue, out float multiplyValue)
    {
        addValue = 0;
        multiplyValue = 0;

        if (!attributeTypeModifyListDic.ContainsKey(attributeType))
            return false;

        foreach (ModifyAttribute modifyAttribute in attributeTypeModifyListDic[attributeType])
        {
            if (modifyAttribute.isAdd)
                addValue += modifyAttribute.value;
            else
                multiplyValue += modifyAttribute.value;
        }

        return true;
    }

    public void UpdateModifyAttribute(object source, List<ModifyAttributeData> modifyAttributeDataList)
    {
        List<AttributeTypeSO> changedAttributeType = new();

        if (sourceModifyListDic.ContainsKey(source))
        {
            foreach (ModifyAttribute modifyAttribute in sourceModifyListDic[source])
            {
                modifyAttribute.RemoveFromAttributeTypeKeyList();

                if (!changedAttributeType.Contains(modifyAttribute.attributeType))
                    changedAttributeType.Add(modifyAttribute.attributeType);
            }

            sourceModifyListDic[source].Clear();
        }
        else
        {
            sourceModifyListDic.Add(source, new List<ModifyAttribute>());
        }

        foreach (ModifyAttributeData modifyAttributeData in modifyAttributeDataList)
        {
            if (!attributeTypeModifyListDic.ContainsKey(modifyAttributeData.attributeType))
                attributeTypeModifyListDic.Add(modifyAttributeData.attributeType, new List<ModifyAttribute>());

            ModifyAttribute modifyAttribute = new ModifyAttribute(modifyAttributeData.attributeType, attributeTypeModifyListDic[modifyAttributeData.attributeType], modifyAttributeData.isAdd, modifyAttributeData.value);

            sourceModifyListDic[source].Add(modifyAttribute);
            attributeTypeModifyListDic[modifyAttributeData.attributeType].Add(modifyAttribute);

            if (!changedAttributeType.Contains(modifyAttribute.attributeType))
                changedAttributeType.Add(modifyAttribute.attributeType);
        }

        foreach (AttributeTypeSO attributeType in changedAttributeType)
        {
            OnAttributeModify?.Invoke(new AttributeModifyEventArgs(attributeType));
        }
    }

    public class ModifyAttribute
    {
        public ModifyAttribute(AttributeTypeSO attributeType, List<ModifyAttribute> attributeTypeKeyList, bool isAdd, float value)
        {
            this.attributeType = attributeType;
            this.attributeTypeKeyList = attributeTypeKeyList;
            this.isAdd = isAdd;
            this.value = value;
        }

        private List<ModifyAttribute> attributeTypeKeyList;
        public AttributeTypeSO attributeType { get; private set; }
        // If not add then multiply
        public bool isAdd { get; private set; }
        public float value { get; private set; }

        public void RemoveFromAttributeTypeKeyList()
        {
            attributeTypeKeyList.Remove(this);
        }
    }
}

public class ModifyAttributeData
{
    public ModifyAttributeData(AttributeTypeSO attributeType, bool isAdd, float value)
    {
        this.attributeType = attributeType;
        this.isAdd = isAdd;
        this.value = value;
    }

    public AttributeTypeSO attributeType { get; private set; }
    public bool isAdd { get; private set; }
    public float value { get; private set; }
}

public class AttributeModifyEventArgs
{
    public AttributeModifyEventArgs(AttributeTypeSO attributeType)
    {
        this.attributeType = attributeType;
    }

    public AttributeTypeSO attributeType { get; private set; }
}
