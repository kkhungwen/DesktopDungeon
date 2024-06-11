using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestData : IInspecable, IInventoryObjectData, ISaveableData
{
    public ChestData(ChestDetailsSO chestDetails)
    {
        Inventory = new ChestInventory(new IInventoryObjectData[chestDetails.inventorySize]);
        this.chestDetails = chestDetails;
    }

    public ChestData(ChestDetailsSO chestDetails, IInventoryObjectData[] inveentoryObjectDataArray)
    {
        Inventory = new ChestInventory(inveentoryObjectDataArray);
        this.chestDetails = chestDetails;
    }

    public IDataSave CreateDataSave()
    {
        ChestDataSave chestDataSave = new ChestDataSave(this);
        return chestDataSave;
    }

    public ChestDetailsSO chestDetails { get; private set; }
    public ChestInventory Inventory { get; private set; }

    public void CreateInstantiatedObject(Vector2 position)
    {
        InstantiatedChest.CreateInstantiatedChest(this, position);
    }

    public Sprite GetIconSprite()
    {
        return chestDetails.iconSprite;
    }

    public bool CreatMouseOverInspec(Transform transform, Vector2 localPosition, out IInspec inspec)
    {
        inspec = null;
        return false;
    }

    public bool CreateClickInspec(Transform transform, Vector2 localPosition, out IInspec inspec)
    {
        inspec = ChestInspec.CreateChestInpec(transform, localPosition, this);
        return true;
    }
}
