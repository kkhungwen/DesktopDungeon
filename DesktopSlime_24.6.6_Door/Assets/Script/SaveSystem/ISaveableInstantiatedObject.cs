using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveableInstantiatedObject
{
    public ISaveableData GetSaveableData();

    public Vector3 GetPosition();

    public void Destroy();
}
