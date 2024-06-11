using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HumanoidEquipInventory : IInventory
{
    public event Action<EquipData> OnAdd;
    public event Action<EquipData> OnRemove;
    public event Action<IInventoryObjectData[]> OnUpdateInventory;

    public HumanoidEquipInventory(EquipData[] equipDataArray)
    {
        if (equipDataArray[0] != null && equipDataArray[0].GetEquipType() != GameResources.Instance.weapon_Equip)
            Debug.Log("weapon data wrong equip type");
        if (equipDataArray[1] != null && equipDataArray[1].GetEquipType() != GameResources.Instance.head_Equip)
            Debug.Log("head data wrong equip type");
        if (equipDataArray[2] != null && equipDataArray[2].GetEquipType() != GameResources.Instance.body_Equip)
            Debug.Log("body data worng equip type");

        this.equipDataArray = equipDataArray;
    }

    public HumanoidEquipInventory()
    {
        equipDataArray = new IInventoryObjectData[3];
    }

    public IInventoryObjectData[] equipDataArray { get; private set; }

    public bool DragOn(int index, IInventoryObjectData inventoryObjectData)
    {
        EquipData equipData = inventoryObjectData as EquipData;
        if (equipData == null)
            return false;

        switch (index)
        {
            case 0:
                if (equipData.GetEquipType() != GameResources.Instance.weapon_Equip)
                    return false;
                return true;
            case 1:
                if (equipData.GetEquipType() != GameResources.Instance.head_Equip)
                    return false;
                return true;
            case 2:
                if (equipData.GetEquipType() != GameResources.Instance.body_Equip)
                    return false;
                return true;
        }

        return false;
    }

    public bool TryAddObjectData(int index, IInventoryObjectData inventoryObjectData)
    {
        EquipData equipData = inventoryObjectData as EquipData;
        if (equipData == null)
            return false;

        switch (index)
        {
            case 0:
                if (equipDataArray[0] != null)
                    return false;
                if (equipData.GetEquipType() != GameResources.Instance.weapon_Equip)
                    return false;

                equipDataArray[0] = equipData;
                OnAdd?.Invoke(equipData);
                OnUpdateInventory?.Invoke(equipDataArray);
                return true;

            case 1:
                if (equipDataArray[1] != null)
                    return false;
                if (equipData.GetEquipType() != GameResources.Instance.head_Equip)
                    return false;

                equipDataArray[1] = equipData;
                OnAdd?.Invoke(equipData);
                OnUpdateInventory?.Invoke(equipDataArray);
                return true;

            case 2:
                if (equipDataArray[2] != null)
                    return false;
                if (equipData.GetEquipType() != GameResources.Instance.body_Equip)
                    return false;

                equipDataArray[2] = equipData;
                OnAdd?.Invoke(equipData);
                OnUpdateInventory?.Invoke(equipDataArray);
                return true;
        }

        return false;
    }

    public bool TryGetObjectData(int index, out IInventoryObjectData inventoryObjectData)
    {
        inventoryObjectData = null;

        if (index < 0 || index >= equipDataArray.Length)
            return false;

        if (equipDataArray[index] == null)
            return false;

        inventoryObjectData = equipDataArray[index];
        return true;
    }

    public bool TryRemoveObjectData(int index)
    {
        if (equipDataArray[index] != null)
        {
            EquipData removeData = equipDataArray[index] as EquipData;
            equipDataArray[index] = null;

            OnRemove?.Invoke(removeData);
            OnUpdateInventory?.Invoke(equipDataArray);
        }
        return true;
    }

    public bool TrySwapObjectData(int swapIndex, int targetIndex)
    {
        return false;
    }

    public bool TryGetEquipData(EquipTypeSO equipType, out EquipData equipData)
    {
        equipData = null;

        if (equipType == GameResources.Instance.weapon_Equip)
        {
            if (equipDataArray[0] == null)
                return false;

            equipData = equipDataArray[0] as EquipData;
            return true;
        }

        if (equipType == GameResources.Instance.head_Equip)
        {
            if (equipDataArray[1] == null)
                return false;

            equipData = equipDataArray[1] as EquipData;
            return true;
        }

        if (equipType == GameResources.Instance.body_Equip)
        {
            if (equipDataArray[2] == null)
                return false;

            equipData = equipDataArray[2] as EquipData;
            return true;
        }

        return false;
    }

    public bool TryAddEquipData(EquipData equipData)
    {
        if (equipData.GetEquipType() == GameResources.Instance.weapon_Equip)
        {
            if (equipDataArray[0] != null)
                return false;

            equipDataArray[0] = equipData;
            OnAdd?.Invoke(equipData);
            OnUpdateInventory?.Invoke(equipDataArray);
            return true;
        }
        if (equipData.GetEquipType() == GameResources.Instance.head_Equip)
        {
            if (equipDataArray[1] != null)
                return false;

            equipDataArray[1] = equipData;
            OnAdd?.Invoke(equipData);
            OnUpdateInventory?.Invoke(equipDataArray);
            return true;
        }
        if (equipData.GetEquipType() == GameResources.Instance.body_Equip)
        {
            if (equipDataArray[2] != null)
                return false;

            equipDataArray[2] = equipData;
            OnAdd?.Invoke(equipData);
            OnUpdateInventory?.Invoke(equipDataArray);
            return true;
        }
        return false;
    }

    public bool TryRemoveEquipData(EquipTypeSO equipType)
    {
        if (equipType == GameResources.Instance.weapon_Equip)
        {
            if (equipDataArray[0] != null)
            {
                EquipData removeData = equipDataArray[0] as EquipData;
                equipDataArray[0] = null;

                OnRemove?.Invoke(removeData);
                OnUpdateInventory?.Invoke(equipDataArray);
            }

            return true;
        }
        if (equipType == GameResources.Instance.head_Equip)
        {
            if (equipDataArray[1] != null)
            {
                EquipData removeData = equipDataArray[1] as EquipData;
                equipDataArray[1] = null;

                OnRemove?.Invoke(removeData);
                OnUpdateInventory?.Invoke(equipDataArray);
            }
            return true;
        }
        if (equipType == GameResources.Instance.body_Equip)
        {
            if (equipDataArray[2] != null)
            {
                EquipData removeData = equipDataArray[2] as EquipData;
                equipDataArray[2] = null;

                OnRemove?.Invoke(removeData);
                OnUpdateInventory?.Invoke(equipDataArray);
            }
            return true;
        }
        return false;
    }
}
