using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedChest : MonoBehaviour, ISaveableInstantiatedObject, IDropable
{
    public static InstantiatedChest CreateInstantiatedChest(ChestData chestData, Vector2 position)
    {
        GameObject instantiate = Instantiate(GameResources.Instance.instantiatedChestPrefab, position, Quaternion.identity);
        if (instantiate.TryGetComponent<InstantiatedChest>(out InstantiatedChest instantiateChest))
        {
            instantiateChest.InitializeInstance(chestData);
            return instantiateChest;
        }
        else
        {
            Debug.Log("cannot cast prefab to InstantiatedChest");
            return null;
        }
    }

    [SerializeField] private InstantiatedObjectInspecControl objectInspecControl;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private SingleSpriteAnimationControl spriteAnimationControl;
    [SerializeField] private DragObject dragObject;

    private ChestData chestData;

    private void Awake()
    {
        dragObject.OnDropObject += DragObject_OnDropObject;
    }

    private void DragObject_OnDropObject()
    {
        Destroy();
    }

    public void InitializeInstance(ChestData chestData)
    {
        this.chestData = chestData;
        objectInspecControl.SetUp(chestData);
        stateMachine.StartStateMachine();
        spriteAnimationControl.SetUp(chestData.chestDetails.animationArray);
    }

    public ChestData GetChestData()
    {
        if (chestData != null)
            return chestData;

        Debug.Log("no ChestData");
        return null;
    }

    public ISaveableData GetSaveableData()
    {
        return GetChestData();
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
        return GetChestData();
    }
}
