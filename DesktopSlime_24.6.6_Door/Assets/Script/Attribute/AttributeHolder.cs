using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeHolder : MonoBehaviour
{
    private Dictionary<AttributeTypeSO, BaseAttribute> baseAttributeDic = new();

    public void SetUp(BaseAttribute[] baseAttributeArray)
    {
        CreateAttributeDictionary(baseAttributeArray);
    }

    private void CreateAttributeDictionary(BaseAttribute[] baseAttributeArray)
    {
        baseAttributeDic = new();

        for (int i = 0; i < baseAttributeArray.Length; i++)
        {
            if (!baseAttributeDic.TryAdd(baseAttributeArray[i].attributeType, baseAttributeArray[i]))
                Debug.Log("Duplicate attribute type in attribute data array");
        }
    }

    public bool GetAttributeValueRaw(AttributeTypeSO attributeType, out float value)
    {
        value = 0;
        if (baseAttributeDic.TryGetValue(attributeType, out BaseAttribute baseAttribute))
        {
            value = baseAttribute.GetAttributeValue(0);
            return true;
        }

        Debug.Log("dose not contain attribute type " + attributeType + " in dic");
        return false;
    }
}
