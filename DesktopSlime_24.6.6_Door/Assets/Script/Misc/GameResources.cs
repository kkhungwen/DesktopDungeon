using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }

    [Space(10f)]
    [Header("PREFAB REFFERENCE")]
    public GameObject instantiatedHumanoidPrefab;
    public GameObject instantiatedEquipPrefab;
    public GameObject instantiatedChestPrefab;
    public GameObject instantiatedDoorPrefab;
    public GameObject instantiatedSlimePrefab;
    public GameObject instanttiatedSlimeKingPrefab;
    public GameObject humanoidInspec_ClickPrefab;
    public GameObject doorInspec_Click_Prefab;
    public GameObject doorInspec_Hover_Prefab;
    public GameObject equipInspecPrefab;
    public GameObject chestInspecPrefab;

    [Space(10f)]
    [Header("LEVELS")]
    public LevelDetailsSO[] mainLevelDetailsArray;

    [Space(10f)]
    // All instance should have these attribute
    [Header("INSTANCE ATTRIBUTE TYPE ARRAY")]
    public AttributeTypeSO[] instanceBaseAttributeTypeArray;

    [Space(10f)]
    // Reference use as enum 
    [Header("ATTRIBUTE TYPE REFERENCE")]
    public AttributeTypeSO attack_Arribute;
    public AttributeTypeSO defence_Attribute;
    public AttributeTypeSO health_Attibute;
    public AttributeTypeSO attackSpeed_Attribute;
    public AttributeTypeSO moveSpeed_Attribute;
    public AttributeTypeSO damageOutput_Attribute;
    public AttributeTypeSO damageTaken_Attribute;
    public AttributeTypeSO healthRegen_Attribute;
    public AttributeTypeSO lifeSteal_Attribute;

    [Space(10f)]
    // Reference use as enum 
    [Header("WEAPON TYPE REFERENCE")]
    public WeaponTypeSO swing_Weapon;
    public WeaponTypeSO stab_Weapon;
    public WeaponTypeSO bow_Weapon;

    [Space(10f)]
    // Reference use as enum 
    [Header("EQUIP TYPE REFERENCE")]
    public EquipTypeSO weapon_Equip;
    public EquipTypeSO head_Equip;
    public EquipTypeSO body_Equip;

    // Tags 
    [Header("TAGS")]
    [Space(10f)]
    public TagSO dead_Tag;
    public TagSO player_Tag;
    public TagSO enemy_Tag;

    [Header("SHADERS")]
    public Shader customColorSlimeShader;

    [Header("WEAPON DAMAGE RATIO")]
    [Space(10f)]
    public DamageRatio weaponDamageRatio;

    [Header("ITEM BONUS STAT UPGRADE")]
    [Space(10f)]
    public AttributeValueData[] bonusAttributeDataArray;
    [Space(10f)]
    public AffixTypeSO[] bonusAffixTypeArray;
}
