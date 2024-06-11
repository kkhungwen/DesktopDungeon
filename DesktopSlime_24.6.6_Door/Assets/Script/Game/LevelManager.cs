using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class LevelManager : MonoBehaviour
{
    public LevelDetailsSO[] levelDetailsArray { get; private set; }
    public bool[] levelUnlockArray { get; private set; }
    public bool[] levelClearedArray { get; private set; }
    public int enemyClearRequiredAmount { get; private set; }
    public int enemyClearedAmount { get; private set; }

    public bool IsEnemyCleared() => enemyClearedAmount >= enemyClearRequiredAmount;
    public bool IsLevelCleared() => levelClearedArray[currentLevelIndex];

    private IEnemyDetails[][] enemyWaveArray;
    private IEnemyDetails[] bossWaveArray;
    private int maxActiveEnemy = 5;
    private float waveInterval = 5f;

    private int currentLevelIndex;
    private int waveIndex = 0;
    private float spawnWaveTime;
    private bool isActive = false;
    private bool isBossFight;

    private void Awake()
    {
        SingletonReference.Instance.gameEventHandler.OnEnemyDie += GameEventHandler_OnEnemyDie;
        SingletonReference.Instance.gameEventHandler.OnBossButtonPressed += GameEventHandler_OnBossButtonPressed;
        SingletonReference.Instance.gameEventHandler.OnLevelButtonPressed += GameEventHandler_OnLevelButtonPressed;
    }

    private void GameEventHandler_OnLevelButtonPressed(int levelIndex)
    {
        ChangeLevel(levelIndex);
    }

    private void GameEventHandler_OnBossButtonPressed()
    {
        if (!IsEnemyCleared())
            return;

        SingletonReference.Instance.gameEventHandler.CallEnterBossFight();

        InstantiatedDoor door = FindObjectOfType<InstantiatedDoor>();
        if (door != null)
        {
            SingletonReference.Instance.enemyManager.SpawnEnemyWave(bossWaveArray, door.transform.position, levelDetailsArray[currentLevelIndex].enemyLevel);
        }

        isBossFight = true;
    }

    private void GameEventHandler_OnEnemyDie()
    {
        if (isBossFight)
        {
            if (SingletonReference.Instance.enemyManager.currentEnemyCount <= 0)
            {
                Debug.Log("win");
                // level cleared

                levelClearedArray[currentLevelIndex] = true;
                isBossFight = false;

                // Unlock level
                if (currentLevelIndex < levelDetailsArray.Length - 1)
                {
                    levelUnlockArray[currentLevelIndex + 1] = true;
                    SingletonReference.Instance.gameEventHandler.CallLevelManagerUpdate();
                }
            }
        }

        if (enemyClearedAmount < enemyClearRequiredAmount)
        {
            enemyClearedAmount++;
            SingletonReference.Instance.gameEventHandler.CallLevelManagerUpdate();
        }
    }


    private void Start()
    {
        CreateLevelArray();
        currentLevelIndex = 0;
        levelUnlockArray[currentLevelIndex] = true;
        LoadLevel(currentLevelIndex);
    }

    private void Update()
    {
        if (!isActive)
            return;

        if (isBossFight)
            return;

        if (SingletonReference.Instance.enemyManager.currentEnemyCount >= maxActiveEnemy)
            return;

        if (Time.time >= spawnWaveTime + waveInterval)
        {
            spawnWaveTime = Time.time;

            if (waveIndex >= enemyWaveArray.Length)
                waveIndex = 0;

            SingletonReference.Instance.enemyManager.SpawnEnemyWave(enemyWaveArray[waveIndex], levelDetailsArray[currentLevelIndex].enemyLevel);
            waveIndex++;
        }
    }

    private void ChangeLevel(int levelIndex)
    {
        SingletonReference.Instance.gameEventHandler.CallChangeLevel();

        LoadLevel(levelIndex);
    }

    private void LoadLevel(int levelIndex)
    {
        if (!levelUnlockArray[levelIndex])
            return;

        LevelDetailsSO levelDetails = levelDetailsArray[levelIndex];

        enemyWaveArray = new IEnemyDetails[levelDetails.enemyWaveArray.Length][];
        for (int i = 0; i < enemyWaveArray.Length; i++)
        {
            enemyWaveArray[i] = new IEnemyDetails[levelDetails.enemyWaveArray[i].enemyDetailsArray.Length];
            for (int j = 0; j < levelDetails.enemyWaveArray[i].enemyDetailsArray.Length; j++)
            {
                enemyWaveArray[i][j] = levelDetails.enemyWaveArray[i].enemyDetailsArray[j].Value;
            }
        }

        bossWaveArray = new IEnemyDetails[levelDetails.bossWave.enemyDetailsArray.Length];
        for (int i = 0; i < bossWaveArray.Length; i++)
        {
            bossWaveArray[i] = levelDetails.bossWave.enemyDetailsArray[i].Value;
        }

        enemyClearRequiredAmount = levelDetails.enemyClearRequiredAmount;
        waveIndex = 0;
        enemyClearedAmount = 0;
        spawnWaveTime = Time.time;
        isActive = true;
        isBossFight = false;

        SingletonReference.Instance.gameEventHandler.CallLevelManagerUpdate();
        SingletonReference.Instance.gameEventHandler.CallLoadLevel();
    }

    private void CreateLevelArray()
    {
        levelDetailsArray = GameResources.Instance.mainLevelDetailsArray;
        levelUnlockArray = new bool[GameResources.Instance.mainLevelDetailsArray.Length];
        levelClearedArray = new bool[GameResources.Instance.mainLevelDetailsArray.Length];
    }
}
