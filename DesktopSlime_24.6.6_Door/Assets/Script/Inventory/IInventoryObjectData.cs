using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryObjectData 
{
    public Sprite GetIconSprite();
    public void CreateInstantiatedObject(Vector2 position);

    public bool CreatMouseOverInspec(Transform transform, Vector2 localPosition, out IInspec inspec);

    public IDataSave CreateDataSave();
}
