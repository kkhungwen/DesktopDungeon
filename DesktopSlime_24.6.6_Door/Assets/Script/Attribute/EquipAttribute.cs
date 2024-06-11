using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipAttribute
{
    [field: SerializeField] public AttributeTypeSO attributeType { get; private set; }

    [field: SerializeField] public float min_LevelValue { get; private set; }

    [field: SerializeField] public float max_LevelValue { get; private set; }

    public float GetValue(int level)
    {
        float value = min_LevelValue + (max_LevelValue - min_LevelValue) * ((float)level / (float)Settings.max_ItemLevel);

        return value;
    }
}
