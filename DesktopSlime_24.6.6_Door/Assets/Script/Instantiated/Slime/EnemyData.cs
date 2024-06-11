using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyData : ISaveableData
{
    public EnemyData(IEnemyDetails enemyDetails)
    {
        this.enemyDetails = enemyDetails;
        level = 0;
    }

    public EnemyData(IEnemyDetails enemyDetails, int level)
    {
        this.enemyDetails = enemyDetails;
        this.level = level;
    }

    public IEnemyDetails enemyDetails { get; private set; }

    public int level { get; private set; }

    public IDataSave CreateDataSave()
    {
        return null;
    }

    public void CreateInstantiatedObject(Vector2 position)
    {
        enemyDetails.CreateInstantiateEnemy(position, this);
    }
}
