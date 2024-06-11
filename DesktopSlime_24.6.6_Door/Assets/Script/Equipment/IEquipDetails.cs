using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipDetails
{
    public string GetName();
    public string GetDescription();
    public EquipTypeSO GetEquipType();
    public AffixTypeSO[] GetAffixTypeArray();
    public Sprite GetInstantiatedSprite();
    public Sprite GetIconSprite();
    public EquipAttribute GetEquipAttribute();
}
