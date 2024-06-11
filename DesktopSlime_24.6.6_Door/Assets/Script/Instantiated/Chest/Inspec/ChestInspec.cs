using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestInspec : MonoBehaviour, IInspec
{
    public static ChestInspec CreateChestInpec(Transform parentTransform, Vector2 localPosition, ChestData chestData)
    {
        GameObject instantiate = Instantiate(GameResources.Instance.chestInspecPrefab, parentTransform);
        instantiate.transform.localPosition = localPosition;

        if (instantiate.TryGetComponent(out ChestInspec chestInspec))
        {
            chestInspec.SetUp(chestData);
            return chestInspec;
        }
        else
        {
            Debug.Log("cannot get component ChestInspec");
            return null;
        }
    }

    [SerializeField] private InputHandler backgroundInputHandler;
    [SerializeField] private ChestInspecUI chestInspecUI;

    private ChestData chestData;

    private void Awake()
    {
        backgroundInputHandler.onPointerClick += BackgroundInputHandler_onPointerClick;
    }

    private void BackgroundInputHandler_onPointerClick()
    {
        chestInspecUI.SetSortingOrderToTop();
    }

    private void OnDisable()
    {
        chestData.Inventory.OnUpdateInventory -= Inventory_OnUpdateInventory;
    }

    public void SetUp(ChestData chestData)
    {
        this.chestData = chestData;
        chestData.Inventory.OnUpdateInventory += Inventory_OnUpdateInventory;
        UpdateUI();
    }

    private void Inventory_OnUpdateInventory(IInventoryObjectData[] obj)
    {
        chestInspecUI.UpdateChestUI(chestData.Inventory);
    }

    public void Destroy()
    {
        chestData.Inventory.OnUpdateInventory -= Inventory_OnUpdateInventory;
        Destroy(gameObject);
    }

    public void UpdateUI()
    {
        chestInspecUI.UpdateChestUI(chestData.Inventory);
        chestInspecUI.SetSortingOrderToTop();
    }

    public void SetSortingOrderToTop()
    {
        chestInspecUI.SetSortingOrderToTop();
    }
}
