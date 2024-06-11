using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestInspecUI : InspecUI
{
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private RectTransform backGroundTransform;
    [SerializeField] private BoxCollider2D clickCollider;
    private InventorySlotUI[] inventorySlotArray;

    public void UpdateChestUI(ChestInventory inventory)
    {
        if (inventorySlotArray == null || inventorySlotArray.Length != inventory.inventorySize)
            CreateInventorySlot(inventory);

        foreach(InventorySlotUI inventorySlot in inventorySlotArray)
            inventorySlot.UpdateInventorySlotUI();
    }

    public void CreateInventorySlot(ChestInventory inventory)
    {
        ClearInventorySlot();

        inventorySlotArray = new InventorySlotUI[inventory.inventorySize];

        for (int i = 0; i < inventory.inventorySize; i++)
        {
            GameObject instantiatedSlot = Instantiate(inventorySlotPrefab, backGroundTransform);
            instantiatedSlot.SetActive(true);

            InventorySlotUI inventorySlot = instantiatedSlot.GetComponent<InventorySlotUI>();

            if (inventorySlot == null)
                Debug.Log("cannot get component InventorySlot");

            inventorySlot.SetUp(i, inventory);
            inventorySlotArray[i] = inventorySlot;

            inventorySlot.OnInteract += InventorySlot_OnInteract;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(backGroundTransform);

        clickCollider.size = backGroundTransform.sizeDelta;
        clickCollider.offset = new Vector2(0, clickCollider.size.y / 2);
    }

    private void InventorySlot_OnInteract()
    {
        SetSortingOrderToTop();
    }

    private void ClearInventorySlot()
    {
        if (inventorySlotArray == null)
            return;

        for (int i = inventorySlotArray.Length - 1; i >= 0; i--)
        {
            inventorySlotArray[i].OnInteract -= InventorySlot_OnInteract;

            Destroy(inventorySlotArray[i].gameObject);
        }

        inventorySlotArray = null;
    }
}
