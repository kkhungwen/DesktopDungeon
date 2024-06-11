using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanoidDataSave : IDataSave
{
    public int humanoidDetailsID;
    public IDataSave[] equipDataSaveArray;

    public HumanoidDataSave(HumanoidData humanoidData)
    {
        if (humanoidData.humanoidDetails != null)
            humanoidDetailsID = SingletonReference.Instance.scriptableObjectIDManager.GetScriptableObjectID(humanoidData.humanoidDetails);
        else
            Debug.Log("try to save HumanoidData with null details");

        equipDataSaveArray = new EquipDataSave[humanoidData.equipInventory.equipDataArray.Length];
        for (int i = 0; i < humanoidData.equipInventory.equipDataArray.Length; i++)
        {
            if (humanoidData.equipInventory.equipDataArray[i] == null)
                continue;

            equipDataSaveArray[i] = humanoidData.equipInventory.equipDataArray[i].CreateDataSave();
        }
    }

    public ISaveableData CreateData()
    {
        return CreateHumanoidData();
    }

    public HumanoidData CreateHumanoidData()
    {
        HumanoidDetailsSO humanoidDetails = SingletonReference.Instance.scriptableObjectIDManager.GetScriptableObject<HumanoidDetailsSO>(humanoidDetailsID);

        EquipData[] equipDataArray = new EquipData[equipDataSaveArray.Length];

        for (int i = 0; i < equipDataSaveArray.Length; i++)
        {
            if (equipDataSaveArray[i] == null)
                continue;

            equipDataArray[i] = equipDataSaveArray[i].CreateData() as EquipData;
            if (equipDataArray[i] == null)
                Debug.Log("connot cast IaSavableData to EquipData");
        }

        HumanoidData humanoidData = new(humanoidDetails, equipDataArray);

        return humanoidData;
    }
}
