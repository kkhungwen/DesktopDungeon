using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimeKingDetailsSO_", menuName = "Scriptable Objects/Slime King Details")]
public class SlimeKingDetailsSO : ScriptableObject, IEnemyDetails
{
    public BaseAttributeArray attributeDataArray;

    public IInstantiatedBeing CreateInstantiateEnemy(Vector2 position, int level)
    {
        EnemyData slimeData = new EnemyData(this);
        return InstantiatedSlimeKing.CreateInstantiatedObject(slimeData, position);
    }

    public IInstantiatedBeing CreateInstantiateEnemy(Vector2 position, EnemyData enemyData)
    {
        return InstantiatedSlimeKing.CreateInstantiatedObject(enemyData, position);
    }
}
