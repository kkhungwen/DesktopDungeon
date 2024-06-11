using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

[RequireComponent(typeof(Rigidbody2D))]
public class InstantiatedEquip : MonoBehaviour , ISaveableInstantiatedObject, IDropable
{
    public static InstantiatedEquip CreateInstantiatedObject(EquipData equipData, Vector2 position, float popOutStrength)
    {
        GameObject obj = Instantiate(GameResources.Instance.instantiatedEquipPrefab, position, Quaternion.identity);
        if (obj.TryGetComponent(out InstantiatedEquip instantiatedEquip))
        {
            instantiatedEquip.InitializeInstance(equipData);
            instantiatedEquip.PopOut(popOutStrength);
            return instantiatedEquip;
        }
        else
        {
            Debug.Log("Cant get component InstantiatedItem in instantiatedItemPrefab");
            return null;
        }
    }

    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private InstantiatedObjectInspecControl objectInspecControl;
    [SerializeField] private DropOn_InstantiatedEquip dropOn;
    [SerializeField] private DragObject dragObject;

    private EquipData equipData;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dragObject.OnDropObject += DragObject_OnDropObject;
    }

    private void DragObject_OnDropObject()
    {
        Destroy();
    }

    public void InitializeInstance(EquipData equipData)
    {
        this.equipData = equipData;
        spriteRenderer.sprite = equipData.GetInstantiatedSprite();
        objectInspecControl.SetUp(equipData);
        stateMachine.StartStateMachine();
        dropOn.SetUp(equipData);
    }

    public void PopOut(float strength)
    {
        float popOutX_Range = 1;
        float popOutY = 1;
        float randomPopOutX = Random.Range(popOutX_Range, -popOutX_Range);

        rb.velocity = new Vector2(randomPopOutX, popOutY) * strength;
    }

    public EquipData GetEquipData()
    {
        if (equipData != null)
            return equipData;

        Debug.Log("No equipData");
        return null;
    }

    public ISaveableData GetSaveableData()
    {
        return GetEquipData();
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
        return GetEquipData();
    }
}
