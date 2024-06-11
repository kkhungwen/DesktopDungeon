using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AffixType_", menuName ="Scriptable Objects/Affix Type")]
public class AffixTypeSO : ScriptableObject
{
    [Space(10f)]
    [Header("ATTRIBUTE MODIFYER")]
    public AttributeModifyData[] attributeModifyDataArray;

    [Space(10f)]
    [Header("ADD STATUS")]
    public bool onAttackAddStatusToSelf;
    public bool onGetHitAddStatusToSelf;
    public bool addStatusToAttackTarget;
    public StatusTypeSO statusType;
    public int statusStack;
}
