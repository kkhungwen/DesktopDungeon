using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveableData 
{
    public void CreateInstantiatedObject(Vector2 position);

    public IDataSave CreateDataSave();
}
