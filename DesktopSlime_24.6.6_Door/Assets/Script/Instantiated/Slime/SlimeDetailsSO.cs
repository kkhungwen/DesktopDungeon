using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimeDetailsSO_", menuName = "Scriptable Objects/Slime Details")]
public class SlimeDetailsSO : ScriptableObject, IEnemyDetails
{
    public BaseAttributeArray attributeDataArray;

    public Sprite sprite;

    public Color[] colorArray;

    public IInstantiatedBeing CreateInstantiateEnemy(Vector2 position, int level)
    {
        EnemyData slimeData = new EnemyData(this);
        return InstantiatedSlime.CreateInstantiatedObject(slimeData, position);
    }
    public IInstantiatedBeing CreateInstantiateEnemy(Vector2 position, EnemyData enemyData)
    {
        return InstantiatedSlime.CreateInstantiatedObject(enemyData, position);
    }
}
