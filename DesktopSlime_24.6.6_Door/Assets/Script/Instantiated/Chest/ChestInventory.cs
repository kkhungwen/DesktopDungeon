using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestInventory : IInventory
{
    public ChestInventory(IInventoryObjectData[] inventoryObjectDataArray)
    {
        this.objectDataArray = inventoryObjectDataArray;
        inventorySize = inventoryObjectDataArray.Length;
    }

    public event Action<IInventoryObjectData[]> OnUpdateInventory;

    public int inventorySize { get; private set; }

    private IInventoryObjectData[] objectDataArray;

    public bool TryAddObjectData(int index, IInventoryObjectData inventoryObjectData)
    {
        if (index > objectDataArray.Length - 1)
        {
            Debug.Log("Index out of range");
            return false;
        }

        if (objectDataArray[index] != null)
        {
            Debug.Log("already contains inventory object");
            return false;
        }

        objectDataArray[index] = inventoryObjectData;

        OnUpdateInventory?.Invoke(objectDataArray);

        return true;
    }

    public bool TrySwapObjectData(int swapIndex, int targetIndex)
    {
        if (objectDataArray[swapIndex] == null)
        {
            Debug.Log("no swap data");
            return false;
        }

        IInventoryObjectData targetObjectData = objectDataArray[targetIndex];

        objectDataArray[targetIndex] = objectDataArray[swapIndex];
        objectDataArray[swapIndex] = targetObjectData;

        OnUpdateInventory?.Invoke(objectDataArray);

        return true;
    }

    public bool TryRemoveObjectData(int index)
    {
        if (index >= objectDataArray.Length || index < 0)
        {
            Debug.Log("index out of range");
            return false;
        }


        objectDataArray[index] = null;

        OnUpdateInventory?.Invoke(objectDataArray);

        return true;
    }

    public bool TryGetObjectData(int index, out IInventoryObjectData inventoryObjectData)
    {
        inventoryObjectData = null;

        if (index >= objectDataArray.Length || index < 0)
        {
            Debug.Log("index out of range");
            return false;
        }

        if (objectDataArray[index] == null)
            return false;

        inventoryObjectData = objectDataArray[index];
        return true;
    }

    public bool DragOn(int index, IInventoryObjectData inventoryObjectData)
    {
        if(!TryGetObjectData(index,out _))
            return true;

        return false;
    }
}
