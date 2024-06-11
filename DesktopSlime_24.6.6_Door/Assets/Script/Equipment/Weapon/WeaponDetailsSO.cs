using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetailsSO_", menuName = "Scriptable Objects/Weapon Details")]
public class WeaponDetailsSO : ScriptableObject, IEquipDetails
{
    [Space(10f)]
    [Header("STRING")]
    public string nameString;
    [TextArea(15, 20)]
    public string descriptionString;

    [Space(10f)]
    [Header("AFFIX")]
    public AffixTypeSO[] affixTypeArray;

    [Space(10f)]
    [Header("IDENTITY")]
    public EquipTypeSO equipType;
    public WeaponTypeSO weaponType;

    [Space(10f)]
    [Header("EQUIP ATTRIBUTE")]
    public EquipAttribute equipAttribute;
    public float attackSpeedRatio;

    [Space(10f)]
    [Header("SPRITE")]
    public Sprite iconSprite;
    public Sprite activeSprite;
    public Sprite instantiatedSprite;
    public Sprite attackSprite;

    [Space(10f)]
    [Header("KNOCK BACK")]
    public float knockBackStrength;

    [Space(10f)]
    [Header("COMMON CONFIGUREATIONS")]
    [Tooltip("Weapon holder will try to be in preferd distance range to target")]
    public float preferredDistance;
    public float preferrdDistanceRange;

    [Space(10f)]
    [Tooltip("Weapon holder will try to attack when target is in attack range")]
    public float attackRange;

    [Space(10f)]
    [Header("HIT BOX WEAPON CONFIGUREATIONS")]
    public float width_HitBox;
    public float height_HitBox;

    [Space(10f)]
    [Header("BOW WEAPON CONFIGUREATIONS")]
    public Sprite arrowSprite;

    [Space(10f)]
    [Header("PROJECTILE")]
    public ProjectileDetailsSO projectileDetails;
    public float projectileRange;

    /// <summary>
    /// INTERFACES METHODS
    /// </summary>
    public string GetName() => nameString;
    public string GetDescription() => descriptionString;
    public EquipTypeSO GetEquipType() => equipType;
    public AffixTypeSO[] GetAffixTypeArray() => affixTypeArray;
    public Sprite GetInstantiatedSprite() => instantiatedSprite;
    public EquipAttribute GetEquipAttribute() => equipAttribute;
    public Sprite GetIconSprite() => iconSprite;
}
