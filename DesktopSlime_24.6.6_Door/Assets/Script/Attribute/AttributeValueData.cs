using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributeValueData
{
    public AttributeValueData(AttributeTypeSO attributeType, float value)
    {
        this.attributeType = attributeType;
        this.value = value;
    }

    [field: SerializeField] public AttributeTypeSO attributeType { get; private set; }
    public float value;

    public static void AddAttributeDataToList(List<AttributeValueData> list, AttributeValueData add)
    {
        foreach (AttributeValueData attributeValueData in list)
            if (attributeValueData.attributeType == add.attributeType)
            {
                attributeValueData.value += add.value;
                return;
            }

        AttributeValueData newAttributeValueData = new(add.attributeType,add.value);
        list.Add(newAttributeValueData);
    }
}
