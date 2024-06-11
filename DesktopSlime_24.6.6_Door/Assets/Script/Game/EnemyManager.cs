using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public int currentEnemyCount { get; private set; }

    private void Awake()
    {
        SingletonReference.Instance.gameEventHandler.OnEnterBossFight += GameEventHandler_OnEnterBossFight;
        SingletonReference.Instance.gameEventHandler.OnLoadLevel += GameEventHandler_OnLevelChange;
    }

    private void GameEventHandler_OnLevelChange()
    {
        ClearEnemy();
    }

    private void GameEventHandler_OnEnterBossFight()
    {
        ClearEnemy();
    }

    public void SpawnEnemyWave(IEnemyDetails[] enemyDetailsArray, int level)
    {
        Vector2 workingSpaceLowerBoundPosition = SingletonReference.Instance.boundaries.GetWorkingSpaceLowerBoundPosition();
        Vector2 workingSpaceUpperBoundPosition = SingletonReference.Instance.boundaries.GetWorkingSpaceUpperBoundPosition();
        float spawnPositionX = UnityEngine.Random.Range(workingSpaceLowerBoundPosition.x, workingSpaceUpperBoundPosition.x);
        float spawnPositionY = workingSpaceLowerBoundPosition.y;
        Vector2 spawnPosition = new Vector2(spawnPositionX, spawnPositionY);

        foreach (IEnemyDetails enemyDetails in enemyDetailsArray)
        {
            SpawnEnemy(enemyDetails, spawnPosition, level);
        }
    }

    public void SpawnEnemyWave(IEnemyDetails[] enemyDetailsArray, Vector2 spawnPosition, int level)
    {
        foreach (IEnemyDetails enemyDetails in enemyDetailsArray)
        {
            SpawnEnemy(enemyDetails, spawnPosition, level);
        }
    }

    public void ClearEnemy()
    {
        foreach (IInstantiatedBeing instantiatedBeing in FindObjectsOfType<MonoBehaviour>().OfType<IInstantiatedBeing>())
        {
            if (instantiatedBeing.IsEnemy())
            {
                instantiatedBeing.Die();
            }
        }

        if (currentEnemyCount != 0)
            Debug.Log("enemy count not zero");

        currentEnemyCount = 0;
    }

    private void SpawnEnemy(IEnemyDetails enemyDetails, Vector2 spawnPosition, int level)
    {
        IInstantiatedBeing instantiateEnemy = enemyDetails.CreateInstantiateEnemy(spawnPosition, level);

        instantiateEnemy.AddEnemyTag();

        instantiateEnemy.OnDie += InstantiateEnemy_OnDie;

        currentEnemyCount++;
    }

    private void InstantiateEnemy_OnDie()
    {
        if (currentEnemyCount <= 0)
            Debug.Log("enemy count lesser then 0");

        currentEnemyCount--;

        SingletonReference.Instance.gameEventHandler.CallEnemyDie();
    }
}
