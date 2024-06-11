using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorData : IInspecable, ISaveableData
{
    public IDataSave CreateDataSave()
    {
        throw new System.NotImplementedException();
    }

    public void CreateInstantiatedObject(Vector2 position)
    {
        InstantiatedDoor.CreateInstantiatedDoor(this, position);
    }

    public bool CreateClickInspec(Transform transform, Vector2 localPosition, out IInspec inspec)
    {
        inspec = DoorInspec_Click.CreateDoorInspec(transform, localPosition, this);
        return true;
    }

    public bool CreatMouseOverInspec(Transform transform, Vector2 localPosition, out IInspec inspec)
    {
        inspec = DoorInspec_Hover.CreateDoorInspec(transform, localPosition, this);
        return true;
    }
}
