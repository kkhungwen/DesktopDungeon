using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChestDataSave : IDataSave
{
    public ChestDataSave(ChestData chestData)
    {
        chestDetailsID = SingletonReference.Instance.scriptableObjectIDManager.GetScriptableObjectID(chestData.chestDetails);

        chestSlotSaveList = new();
        for (int i = 0; i < chestData.Inventory.inventorySize; i++)
        {
            if (!chestData.Inventory.TryGetObjectData(i, out IInventoryObjectData inventoryObjectData))
                continue;

            ChestSlotSave chestSlotSave = new ChestSlotSave(i, inventoryObjectData.CreateDataSave());
            chestSlotSaveList.Add(chestSlotSave);
        }
    }

    public ChestData CreateChestData()
    {
        ChestDetailsSO chestDetails = SingletonReference.Instance.scriptableObjectIDManager.GetScriptableObject<ChestDetailsSO>(chestDetailsID);

        IInventoryObjectData[] inventoryObjectDataArray = new IInventoryObjectData[chestDetails.inventorySize];

        foreach (ChestSlotSave chestSlotSave in chestSlotSaveList)
        {
            IInventoryObjectData inventoryObjectData = chestSlotSave.dataSave.CreateData() as IInventoryObjectData;
            if (inventoryObjectData == null)
                Debug.Log("cant cast DataSave created object to IInventoryObject");

            inventoryObjectDataArray[chestSlotSave.index] = inventoryObjectData;
        }

        ChestData chestData = new ChestData(chestDetails, inventoryObjectDataArray);
        return chestData;
    }

    public ISaveableData CreateData()
    {
        ChestData chestData = CreateChestData();
        return chestData;
    }

    public int chestDetailsID;
    public List<ChestSlotSave> chestSlotSaveList;
}

[System.Serializable]
public class ChestSlotSave
{
    public ChestSlotSave(int index, IDataSave dataSave)
    {
        this.index = index;
        this.dataSave = dataSave;
    }

    public int index;
    public IDataSave dataSave;
}
