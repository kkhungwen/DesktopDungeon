using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedDoor : MonoBehaviour, ISaveableInstantiatedObject, IDropable
{
    public static InstantiatedDoor CreateInstantiatedDoor(DoorData doorData, Vector2 position)
    {
        GameObject instantiate = Instantiate(GameResources.Instance.instantiatedDoorPrefab, position, Quaternion.identity);
        if (instantiate.TryGetComponent(out InstantiatedDoor instantiatedDoor))
        {
            instantiatedDoor.InitializeInstance(doorData);
            return instantiatedDoor;
        }
        else
        {
            Debug.Log("cannot cast prefab to InstantiatedDoor");
            return null;
        }
    }

    [SerializeField] private InstantiatedObjectInspecControl objectInspecControl;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private SingleSpriteAnimationControl spriteAnimationControl;
    private DoorData doorData;

    public void InitializeInstance(DoorData doorData)
    {
        this.doorData = doorData;
        objectInspecControl.SetUp(doorData);
        stateMachine.StartStateMachine();
    }

    public DoorData GetDoorData()
    {
        if (doorData != null)
            return doorData;

        Debug.Log("no DoorData");
        return null;
    }

    public ISaveableData GetSaveableData()
    {
        return GetDoorData();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public object GetDropObject()
    {
        return GetDoorData();
    }
}
