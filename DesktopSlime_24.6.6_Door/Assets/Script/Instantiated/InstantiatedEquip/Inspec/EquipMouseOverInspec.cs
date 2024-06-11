using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EquipInspecUI))]
public class EquipMouseOverInspec : MonoBehaviour, IInspec
{
    public static EquipMouseOverInspec CreateEquipInspec(Transform parentTransform, Vector2 localPosition, EquipData equipData)
    {
        GameObject instantiate = Instantiate(GameResources.Instance.equipInspecPrefab, parentTransform);
        instantiate.transform.localPosition = localPosition;

        if (instantiate.TryGetComponent(out EquipMouseOverInspec equipInspec))
        {
            equipInspec.SetUp(equipData);
            return equipInspec;
        }
        else
        {
            Debug.Log("cannot get component EquipInspec");
            return null;
        }
    }

    private EquipInspecUI equipInspecUI;
    private EquipData equipData;

    [SerializeField] private InputHandler backgroundInputHandler;

    private void Awake()
    {
        equipInspecUI = GetComponent<EquipInspecUI>();

        backgroundInputHandler.onPointerClick += BackgroundInputHandler_onPointerClick;
    }

    private void BackgroundInputHandler_onPointerClick()
    {
        equipInspecUI.SetSortingOrderToTop();
    }

    private void OnDisable()
    {
        equipData.OnUpdateEquipData -= EquipData_OnUpdateEquipData;
    }

    public void SetUp(EquipData equipData)
    {
        this.equipData = equipData;
        equipData.OnUpdateEquipData += EquipData_OnUpdateEquipData;
        UpdateUI();
    }

    private void EquipData_OnUpdateEquipData(EquipData equipData)
    {
        UpdateUI();
    }

    public void SetSortingOrderToTop()
    {
        equipInspecUI.SetSortingOrderToTop();
    }

    public void UpdateUI()
    {
        equipInspecUI.UpdateUI(equipData);
        equipInspecUI.SetSortingOrderToTop();
    }

    public void Destroy()
    {
        equipData.OnUpdateEquipData -= EquipData_OnUpdateEquipData;

        Destroy(gameObject);
    }
}
