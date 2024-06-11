using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttribute
{
    [field: SerializeField] public AttributeTypeSO attributeType { get; private set; }

    [field: SerializeField] public float min_LevelValue { get; private set; }

    [field: SerializeField] public float max_LevelValue { get; private set; }


    [field: SerializeField] public AnimationCurve normalized_LevelValueCurve;

    public float GetAttributeValue(int level)
    {
        float value = min_LevelValue + (max_LevelValue - min_LevelValue) * normalized_LevelValueCurve.Evaluate(level / Settings.max_BaseAttributeLevel);
        return value;
    }
}
