using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable 
{
    public void Save(GameSave gameObjectSave);
    public void Load(GameSave gameObjectSave);
}
