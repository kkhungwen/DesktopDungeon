using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DoorInspec_Hover_UI))]
public class DoorInspec_Hover : MonoBehaviour, IInspec
{
    public static DoorInspec_Hover CreateDoorInspec(Transform parentTransform, Vector2 localPosition, DoorData doorData)
    {
        GameObject instantiate = Instantiate(GameResources.Instance.doorInspec_Hover_Prefab, parentTransform);
        instantiate.transform.localPosition = localPosition;

        if (instantiate.TryGetComponent(out DoorInspec_Hover doorInspec))
        {
            doorInspec.SetUp(doorData);
            return doorInspec;
        }
        else
        {
            Debug.Log("cannot get component DoorInspec_HOver");
            return null;
        }
    }

    private DoorInspec_Hover_UI doorInspecUI;

    private DoorData doorData;

    private void OnEnable()
    {
        SingletonReference.Instance.gameEventHandler.OnLevelManagerUpdate += GameEventHandler_OnLevelManagerUpdate;
    }
    private void OnDisable()
    {
        SingletonReference.Instance.gameEventHandler.OnLevelManagerUpdate -= GameEventHandler_OnLevelManagerUpdate;
    }

    private void Awake()
    {
        doorInspecUI = GetComponent<DoorInspec_Hover_UI>();
    }

    private void GameEventHandler_OnLevelManagerUpdate()
    {
        UpdateUI();
    }

    public void SetUp(DoorData doorData)
    {
        this.doorData = doorData;
        UpdateUI();
        SetSortingOrderToTop();
    }

    private void UpdateUI()
    {
        int requiredAmount = SingletonReference.Instance.levelManager.enemyClearRequiredAmount;
        int clearedAmount = SingletonReference.Instance.levelManager.enemyClearedAmount;

        doorInspecUI.UpdateDoorUI(requiredAmount, clearedAmount);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void SetSortingOrderToTop()
    {
        doorInspecUI.SetSortingOrderToTop();
    }
}
