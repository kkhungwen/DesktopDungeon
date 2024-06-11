using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidInspec_Click : MonoBehaviour, IInspec
{
    public static HumanoidInspec_Click CreateHumanoidInspec_Click(Transform parentTransform, Vector2 localPosition, HumanoidData humanoidData)
    {
        GameObject instantiate = Instantiate(GameResources.Instance.humanoidInspec_ClickPrefab, parentTransform);
        instantiate.transform.localPosition = localPosition;

        if (instantiate.TryGetComponent(out HumanoidInspec_Click humanoidInspec))
        {
            humanoidInspec.SetUp(humanoidData);
            return humanoidInspec;
        }
        else
        {
            Debug.Log("cannot get component HumanoidInspec_Click");
            return null;
        }
    }

    [SerializeField] private InputHandler backgroundInputHandler;

    private HumanoidData humanoidData;
    private HumanoidInspecUI_Click clickInspecUI;

    private void Awake()
    {
        clickInspecUI = GetComponent<HumanoidInspecUI_Click>();

        backgroundInputHandler.onPointerClick += BackgroundInputHandler_onPointerClick;
    }

    private void OnDisable()
    {
        humanoidData.equipInventory.OnUpdateInventory -= EquipInventory_OnUpdateInventory;
    }

    private void BackgroundInputHandler_onPointerClick()
    {
        clickInspecUI.SetSortingOrderToTop();
    }

    public void SetUp(HumanoidData humanoidData)
    {
        this.humanoidData = humanoidData;

        clickInspecUI.SetUp(humanoidData.equipInventory);

        humanoidData.equipInventory.OnUpdateInventory += EquipInventory_OnUpdateInventory;

        UpdateUI();
    }

    private void EquipInventory_OnUpdateInventory(IInventoryObjectData[] inventoryObjectDataArray)
    {
        UpdateUI();
    }

    public void Destroy()
    {
        humanoidData.equipInventory.OnUpdateInventory -= EquipInventory_OnUpdateInventory;
        Destroy(gameObject);
    }

    public void SetSortingOrderToTop()
    {
        clickInspecUI.SetSortingOrderToTop();
    }

    public void UpdateUI()
    {
        clickInspecUI.UpdateUI();
        clickInspecUI.SetSortingOrderToTop();
    }
}
