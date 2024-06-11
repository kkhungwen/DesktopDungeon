using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DoorInspec_Click_UI))]
public class DoorInspec_Click : MonoBehaviour, IInspec
{
    public static DoorInspec_Click CreateDoorInspec(Transform parentTransform, Vector2 localPosition, DoorData doorData)
    {
        GameObject instantiate = Instantiate(GameResources.Instance.doorInspec_Click_Prefab, parentTransform);
        instantiate.transform.localPosition = localPosition;

        if (instantiate.TryGetComponent(out DoorInspec_Click doorInspec))
        {
            doorInspec.SetUp(doorData);
            return doorInspec;
        }
        else
        {
            Debug.Log("cannot get component DoorInspec_Click");
            return null;
        }
    }

    private DoorInspec_Click_UI doorInspecUI;
    [SerializeField] private InputHandler backgroundInputHandler;

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
        doorInspecUI = GetComponent<DoorInspec_Click_UI>();

        backgroundInputHandler.onPointerClick += BackgroundInputHandler_onPointerClick;
    }

    private void GameEventHandler_OnLevelManagerUpdate()
    {
        UpdateUI(SingletonReference.Instance.levelManager.levelDetailsArray, SingletonReference.Instance.levelManager.levelUnlockArray);
    }

    private void BackgroundInputHandler_onPointerClick()
    {
        doorInspecUI.SetSortingOrderToTop();
    }

    public void SetUp(DoorData doorData)
    {
        this.doorData = doorData;
        UpdateUI(SingletonReference.Instance.levelManager.levelDetailsArray, SingletonReference.Instance.levelManager.levelUnlockArray);
    }

    private void UpdateUI(LevelDetailsSO[] levelDetailsArray, bool[] levelUnlockArray)
    {
        doorInspecUI.UpdateDoorUI(levelUnlockArray, levelDetailsArray);
    }

    public void Destroy()
    {
        backgroundInputHandler.onPointerClick -= BackgroundInputHandler_onPointerClick;

        Destroy(gameObject);
    }

    public void SetSortingOrderToTop()
    {
        doorInspecUI.SetSortingOrderToTop();
    }
}
