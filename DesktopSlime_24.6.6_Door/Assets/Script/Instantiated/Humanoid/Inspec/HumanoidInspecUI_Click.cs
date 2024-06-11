using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidInspecUI_Click : InspecUI
{
    [SerializeField] InventorySlotUI weaponInventorySlot;
    [SerializeField] InventorySlotUI headInventorySlot;
    [SerializeField] InventorySlotUI bodyInventorySlot;
    [SerializeField] private BoxCollider2D clickCollider;
    [SerializeField] private RectTransform backGroundTransform;

    public void SetUp(IInventory inventory)
    {
        weaponInventorySlot.SetUp(0, inventory);
        headInventorySlot.SetUp(1, inventory);
        bodyInventorySlot.SetUp(2, inventory);

        weaponInventorySlot.OnInteract += InventorySlot_OnInteract;
        headInventorySlot.OnInteract += InventorySlot_OnInteract;
        bodyInventorySlot.OnInteract += InventorySlot_OnInteract;

        LayoutRebuilder.ForceRebuildLayoutImmediate(backGroundTransform);

        clickCollider.size = backGroundTransform.sizeDelta;
        clickCollider.offset = new Vector2(0, clickCollider.size.y / 2);
    }

    public void UpdateUI()
    {
        weaponInventorySlot.UpdateInventorySlotUI();
        headInventorySlot.UpdateInventorySlotUI();
        bodyInventorySlot.UpdateInventorySlotUI();
    }

    private void InventorySlot_OnInteract()
    {
        SetSortingOrderToTop();
    }
}
