using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorDetailsSO_", menuName = "Scriptable Objects/Armor Details")]
public class ArmorDetailsSO : ScriptableObject, IEquipDetails
{
    [Space(10f)]
    [Header("STRING")]
    public string nameString;
    public string descriptionString;

    [Space(10f)]
    [Header("AFFIX")]
    public AffixTypeSO[] affixTypeArray;

    [Space(10f)]
    [Header("IDENTITY")]
    public EquipTypeSO equipType;

    // the type of equip attribute will be determind by equipments
    [Space(10f)]
    [Header("EQUIP ATTRIBUTE")]
    public EquipAttribute equipAttribute;

    [Space(10f)]
    [Header("SPRITE")]
    public Sprite iconSprite;
    public Sprite instantiatedSprite;
    public SpriteArray[] spriteAnimationArray;
    public Sprite handSprite;
    public bool enableBaseSprite;
    [Tooltip("Check if body armor has its own hand sprite")]
    public bool isReplaceHandSprite;

    public string GetName() => nameString;
    public string GetDescription() => descriptionString;
    public EquipTypeSO GetEquipType() => equipType;
    public AffixTypeSO[] GetAffixTypeArray() => affixTypeArray;
    public Sprite GetInstantiatedSprite() => instantiatedSprite;
    public EquipAttribute GetEquipAttribute() => equipAttribute;
    public Sprite GetIconSprite() => iconSprite;
}
