using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossButton : MonoBehaviour
{
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private Button bossButton;


    private void Awake()
    {
        SingletonReference.Instance.gameEventHandler.OnLevelManagerUpdate += GameEventHandler_OnLevelManagerUpdate;
        bossButton.onClick.AddListener(ButtonPressed);
    }

    private void OnDestroy()
    {
        SingletonReference.Instance.gameEventHandler.OnLevelManagerUpdate -= GameEventHandler_OnLevelManagerUpdate;
    }

    private void Start()
    {
        buttonObject.SetActive(false);

        SetUpButton();
    }

    private void GameEventHandler_OnLevelManagerUpdate()
    {
        SetUpButton();
    }

    private void SetUpButton()
    {
        if (!SingletonReference.Instance.levelManager.IsEnemyCleared() || SingletonReference.Instance.levelManager.IsLevelCleared())
            buttonObject.SetActive(false);
        else
            buttonObject.SetActive(true);
    }

    public void ButtonPressed()
    {
        SingletonReference.Instance.gameEventHandler.CallBossButtonPressed();

        buttonObject.SetActive(false);
    }
}
