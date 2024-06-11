using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttributeTypeSO_", menuName = "Scriptable Objects/Attribute Type")]
public class AttributeTypeSO : ScriptableObject
{
    [field: SerializeField] public string attributeName;
}
