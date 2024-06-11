using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributeModifyData
{
    public AttributeTypeSO attributeType;

    // modify amount
    public float value;

    // else is multiply
    public bool isAdd;
}
