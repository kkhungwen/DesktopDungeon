using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    public bool TryAddObjectData(int index, IInventoryObjectData inventoryObjectData);
    public bool TrySwapObjectData(int swapIndex, int targetIndex);
    public bool TryRemoveObjectData(int index);
    public bool TryGetObjectData(int index, out IInventoryObjectData inventoryObjectData);
    public bool DragOn(int index, IInventoryObjectData inventoryObjectData);
}
