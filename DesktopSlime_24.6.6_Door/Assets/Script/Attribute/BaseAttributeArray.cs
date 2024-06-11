using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseAttributeArraySO_", menuName = "Scriptable Objects/Base Attribute Array")]
public class BaseAttributeArray : ScriptableObject
{
    public BaseAttribute[] baseAttributeArray;

    private void OnValidate()
    {
        foreach (AttributeTypeSO attributeType in GameResources.Instance.instanceBaseAttributeTypeArray)
        {
            bool containsAttribute = false;
            foreach (BaseAttribute baseAttribute in baseAttributeArray)
            {
                if (baseAttribute.attributeType != attributeType)
                    continue;

                containsAttribute = true;
                break;
            }

            if (!containsAttribute)
                Debug.Log(this + " missing : " + attributeType);
        }

        foreach (BaseAttribute baseAttribute in baseAttributeArray)
        {
            foreach (BaseAttribute otherAttribute in baseAttributeArray)
            {
                if (otherAttribute == baseAttribute)
                    continue;

                if (baseAttribute.attributeType == otherAttribute.attributeType)
                    Debug.Log(this + " Contains duplicate attributeType : " + baseAttribute.attributeType);
            }
        }

        foreach (BaseAttribute baseAttribute in baseAttributeArray)
        {
            HelperUtils.ValidateNormalizedCurve(baseAttribute.normalized_LevelValueCurve);
        }

        HelperUtils.ValidateCheckEnumerableValues(this, nameof(baseAttributeArray), baseAttributeArray);
    }
}

