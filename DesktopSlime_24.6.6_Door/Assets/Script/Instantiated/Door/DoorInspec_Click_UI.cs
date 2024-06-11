using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInspec_Click_UI : InspecUI
{
    [SerializeField] private GameObject levelSelectGameObject;
    private List<LevelSelectUI> levelSelectUIList = new();

    public void UpdateDoorUI(bool[] unlockLevelArray, LevelDetailsSO[] levelDetailsArray)
    {
        if (levelDetailsArray.Length > levelSelectUIList.Count)
            AddLevelSelect(levelDetailsArray.Length - levelSelectUIList.Count);

        for (int i = 0; i < levelDetailsArray.Length; i++)
        {
            levelSelectUIList[i].UpdateUI(levelDetailsArray[i], unlockLevelArray[i], i);
        }
    }

    private void AddLevelSelect(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject instantiate = Instantiate(levelSelectGameObject, levelSelectGameObject.transform.parent);
            LevelSelectUI levelSelectUI = instantiate.GetComponent<LevelSelectUI>();
            levelSelectUIList.Add(levelSelectUI);

            instantiate.SetActive(true);
        }
    }
}
