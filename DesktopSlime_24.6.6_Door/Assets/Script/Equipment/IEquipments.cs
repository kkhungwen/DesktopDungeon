using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IEquipments 
{
    public event Action<OnEquipEventArgs> OnEquip;
    public event Action<OnUnEquipEventArgs> OnUnEquip;
    public event Action<List<AttributeValueData>> OnEquipmentsAttributeChange;
    public event Action<List<AffixTypeSO>> OnEquipmentsAffixChange;
}

public class OnEquipEventArgs : EventArgs
{
    public EquipData equipData;
}

public class OnUnEquipEventArgs : EventArgs
{
    public EquipData equipData;
}
