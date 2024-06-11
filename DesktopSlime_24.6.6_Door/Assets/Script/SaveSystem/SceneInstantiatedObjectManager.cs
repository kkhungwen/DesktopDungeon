using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneInstantiatedObjectManager : MonoBehaviour, ISaveable
{
    private void OnEnable()
    {
        SingletonReference.Instance.saveLoadManager.RegisterSaveable(this);
    }

    private void OnDisable()
    {
        SingletonReference.Instance.saveLoadManager.DeregisterSaveable(this);
    }

    public void Load(GameSave gameSave)
    {
        DestroySceneInstantiatedObject();

        foreach (DataSavePosition dataSavePosition in gameSave.sceneInstantiatedObjectSave)
        {
            ISaveableData saveableData = dataSavePosition.dataSave.CreateData();
            Vector2 position = new Vector2(dataSavePosition.position.x, dataSavePosition.position.y);
            saveableData.CreateInstantiatedObject(position);
        }
    }

    public void Save(GameSave gameSave)
    {
        gameSave.sceneInstantiatedObjectSave = new();

        foreach (ISaveableInstantiatedObject saveableInstantiatedObject in FindObjectsOfType<MonoBehaviour>().OfType<ISaveableInstantiatedObject>())
        {
            IDataSave dataSave = saveableInstantiatedObject.GetSaveableData().CreateDataSave();
            Vector3 position = saveableInstantiatedObject.GetPosition();
            DataSavePosition dataSavePosition = new(dataSave, position);

            gameSave.sceneInstantiatedObjectSave.Add(dataSavePosition);
        }
    }

    private void DestroySceneInstantiatedObject()
    {
        foreach (ISaveableInstantiatedObject saveableInstantiatedObject in FindObjectsOfType<MonoBehaviour>().OfType<ISaveableInstantiatedObject>())
        {
            saveableInstantiatedObject.Destroy();
        }
    }
}

[System.Serializable]
public class DataSavePosition
{
    public DataSavePosition(IDataSave dataSave, Vector3 position)
    {
        this.dataSave = dataSave;
        this.position = new SerializableVector3(position.x, position.y, position.z);
    }

    public IDataSave dataSave;
    public SerializableVector3 position;
}
