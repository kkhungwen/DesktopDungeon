using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private GameObject lockPanel;
    [SerializeField] private Button levelButton;
    private LevelDetailsSO LevelDetails;
    private int levelIndex;

    private void Awake()
    {
        levelButton.onClick.AddListener(LevelButtonPressed);
    }

    private void LevelButtonPressed()
    {
        SingletonReference.Instance.gameEventHandler.CallLevelButtonPressed(levelIndex);
    }

    public void UpdateUI(LevelDetailsSO levelDetails, bool isUnlock, int levelIndex)
    {
        this.LevelDetails = levelDetails;
        this.levelIndex = levelIndex;
        this.levelName.text = levelDetails.name;
        lockPanel.SetActive(!isUnlock);
    }
}
