using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageRatio
{
    [field: SerializeField] public AttributeRatio[] attributeRatioArray { get; private set; }
}

[System.Serializable]
public class AttributeRatio
{
    [field: SerializeField] public AttributeTypeSO attributeType { get; private set; }

    [field: SerializeField] public float ratio { get; private set; }
}
