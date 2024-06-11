using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class DropOn_InstantiatedEquip : MonoBehaviour, IDropOnObject
{
    public event Action OnDropOn;

    private EquipData equipData;

    public void SetUp(EquipData equipData)
    {
        this.equipData = equipData;
    }

    public bool DragOn(object dragObject)
    {
        return false;
    }

    public bool DropOn(object dragObject)
    {
        EquipData dragEquipData = dragObject as EquipData;

        if (dragEquipData == null)
            return false;

        if (equipData.Forge(dragEquipData))
        {
            OnDropOn?.Invoke();
            return true;
        }

        return false;
    }
}
