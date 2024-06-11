using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDetails 
{
    public IInstantiatedBeing CreateInstantiateEnemy(Vector2 position, int level);
    public IInstantiatedBeing CreateInstantiateEnemy(Vector2 position, EnemyData enemyData);
}
