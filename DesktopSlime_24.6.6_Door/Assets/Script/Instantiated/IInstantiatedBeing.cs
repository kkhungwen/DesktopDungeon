using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInstantiatedBeing 
{
    public void Die();

    // Subscribe by enemymanager to notify spawn enemy died
    public event Action OnDie;

    // Idenetify self when clearing enemies
    public bool IsEnemy();
    
    // Called by enemymanager to add enemyTag to TagData
    public void AddEnemyTag();
}
