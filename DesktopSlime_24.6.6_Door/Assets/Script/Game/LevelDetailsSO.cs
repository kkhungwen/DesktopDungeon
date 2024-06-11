using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

[CreateAssetMenu(fileName = "LevelDewtailsSO_", menuName = "Scriptable Objects/Game Play/Level Details")]
public class LevelDetailsSO : ScriptableObject
{
    public int enemyLevel;
    public int enemyClearRequiredAmount;
    public EnemyDetailsArray[] enemyWaveArray;
    public EnemyDetailsArray bossWave;
}

[System.Serializable]
public class EnemyDetailsArray
{
    public SerializableInterface<IEnemyDetails>[] enemyDetailsArray;
}
