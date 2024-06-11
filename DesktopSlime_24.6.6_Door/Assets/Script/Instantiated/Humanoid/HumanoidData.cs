using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidData : IInspecable, ISaveableData
{
    public HumanoidData(HumanoidDetailsSO humanoidDetails)
    {
        this.humanoidDetails = humanoidDetails;
        equipInventory = new();
    }

    public HumanoidData(HumanoidDetailsSO humanoidDetails, EquipData[] equipDataArray)
    {
        this.humanoidDetails = humanoidDetails;
        equipInventory = new(equipDataArray);
    }

    public HumanoidDetailsSO humanoidDetails { get; private set; }
    public HumanoidEquipInventory equipInventory;

    public void CreateInstantiatedObject(Vector2 position)
    {
        InstantiatedHumanoid.CreateInstantiatedObject(this, position);
    }

    public bool CreateClickInspec(Transform transform, Vector2 localPosition, out IInspec inspec)
    {
        inspec = HumanoidInspec_Click.CreateHumanoidInspec_Click(transform,localPosition,this);
        return true;
    }

    public bool CreatMouseOverInspec(Transform transform, Vector2 localPosition, out IInspec inspec)
    {
        inspec = null;
        return false;
    }

    public IDataSave CreateDataSave()
    {
        HumanoidDataSave humanoidDataSave = new(this);
        return humanoidDataSave;
    }
}
