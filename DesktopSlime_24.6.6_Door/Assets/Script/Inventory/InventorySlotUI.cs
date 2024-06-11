using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class InventorySlotUI : MonoBehaviour, IDropOnObject, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action OnInteract;

    public IInventory Inventory { get; private set; }
    public int Index { get; private set; }

    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Image IconImage;
    [SerializeField] private Image dragGhost;
    [SerializeField] private Vector2 inspecOffset;

    private IInspec inspec;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnInteract?.Invoke();

        if (!Inventory.TryGetObjectData(Index, out IInventoryObjectData inventoryObjectData))
            return;

        IconImage.sprite = emptySprite;
        dragGhost.sprite = inventoryObjectData.GetIconSprite();

        CloseInspec();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Inventory.TryGetObjectData(Index, out IInventoryObjectData inventoryObjectData))
            return;

        dragGhost.transform.position = HelperUtils.GetMouseWorldPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!Inventory.TryGetObjectData(Index, out IInventoryObjectData inventoryObjectData))
            return;

        dragGhost.transform.localPosition = Vector2.zero;
        dragGhost.sprite = emptySprite;
        IconImage.sprite = inventoryObjectData.GetIconSprite();

        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out InventorySlotUI inventorySlot) && inventorySlot != this)
        {
            // if drag to self inventory slot
            if (inventorySlot.Inventory == Inventory)
            {
                Inventory.TrySwapObjectData(Index, inventorySlot.Index);
                return;
            }
            // if drag to other inventory slot
            else
            {
                if (inventorySlot.TryAddInventoryObject(inventoryObjectData))
                    Inventory.TryRemoveObjectData(Index);

                return;
            }
        }

        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            inventoryObjectData.CreateInstantiatedObject(HelperUtils.GetMouseWorldPosition());
            Inventory.TryRemoveObjectData(Index);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnInteract?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Inventory.TryGetObjectData(Index, out IInventoryObjectData inventoryObjectData))
            return;

        OpenInspec(inventoryObjectData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseInspec();
    }

    public bool DragOn(object dragObject)
    {
        // if cannot cast object to IInventoryObject
        IInventoryObjectData inventoryObjectData = dragObject as IInventoryObjectData;
        if (inventoryObjectData == null)
            return false;

        return Inventory.DragOn(Index, inventoryObjectData);
    }

    public bool DropOn(object dragObject)
    {
        // if cannot cast object to IInventoryObject
        IInventoryObjectData inventoryObjectData = dragObject as IInventoryObjectData;
        if (inventoryObjectData == null)
            return false;

        // if already contains object
        if (!Inventory.TryAddObjectData(Index, inventoryObjectData))
            return false;

        OnInteract?.Invoke();
        return true;
    }

    public void SetUp(int index, IInventory inventory)
    {
        IconImage.sprite = emptySprite;
        dragGhost.sprite = emptySprite;

        this.Inventory = inventory;
        this.Index = index;

        UpdateInventorySlotUI();
    }

    public bool TryAddInventoryObject(IInventoryObjectData inventoryObjectData)
    {
        if (Inventory.TryAddObjectData(Index, inventoryObjectData))
            return true;
        else
            return false;
    }

    public void UpdateInventorySlotUI()
    {
        if (Inventory.TryGetObjectData(Index, out IInventoryObjectData inventoryObjectData))
        {
            IconImage.sprite = inventoryObjectData.GetIconSprite();
        }
        else
        {
            IconImage.sprite = emptySprite;
        }
    }

    private void OpenInspec(IInventoryObjectData inventoryObjectData)
    {
        if (inspec != null)
            return;

        inventoryObjectData.CreatMouseOverInspec(transform, inspecOffset, out inspec);
    }

    private void CloseInspec()
    {
        if (inspec == null)
            return;

        inspec.Destroy();
        inspec = null;
    }
}
