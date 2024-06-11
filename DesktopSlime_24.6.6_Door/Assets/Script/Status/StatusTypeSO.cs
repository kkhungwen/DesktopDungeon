using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusType_", menuName = "Scriptable Objects/Status Type")]
public class StatusTypeSO : ScriptableObject
{
    [Space(10f)]
    [Header("DURATION")]
    public float duration;

    [Space(10f)]
    [Header("STACK")]
    public int maxStack;
    public bool resetDurationOnStack;

    [Space(10f)]
    [Header("ATTRIBUTE MODIFYER")]
    public AttributeModifyData[] attributeModifyDataArray;
}
