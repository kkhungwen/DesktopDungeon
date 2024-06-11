using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropOn_Humanoid : MonoBehaviour, IDropOnObject
{
    public event Action<OnDropOnEventArgs> OnDropOn;
    public event Action OnDragOn;
    public bool isDebug;

    public bool DragOn(object dragObject)
    {
        EquipData equipData = dragObject as EquipData;
        if (equipData != null)
        {
            if (isDebug)
                Debug.Log("Drag on equip data");

            OnDragOn?.Invoke();
            return true;
        }

        return false;
    }

    public bool DropOn(object dragObject)
    {
        EquipData equipData = dragObject as EquipData;
        if(equipData != null)
        {
            if (isDebug)
                Debug.Log("Drop on equip data");

            OnDropOn?.Invoke(new OnDropOnEventArgs { equipData = equipData });
            return true;
        }

        return false;
    }
}

public class OnDropOnEventArgs : EventArgs
{
    public EquipData equipData;
}
