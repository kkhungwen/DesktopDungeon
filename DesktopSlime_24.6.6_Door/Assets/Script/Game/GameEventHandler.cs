using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventHandler : MonoBehaviour
{
    public event Action OnEnemyDie;
    public event Action OnLevelManagerUpdate;
    public event Action OnEnterBossFight;
    public event Action OnBossButtonPressed;
    public event Action OnLoadLevel;
    public event Action OnChangeLevel;
    public event Action<int> OnLevelButtonPressed;

    public void CallChangeLevel()
    {
        OnChangeLevel?.Invoke();
    }

    public void CallLoadLevel()
    {
        OnLoadLevel?.Invoke();
    }

    public void CallLevelButtonPressed(int levelIndex)
    {
        OnLevelButtonPressed?.Invoke(levelIndex);
    }

    public void CallEnemyDie()
    {
        OnEnemyDie?.Invoke();
    }

    public void CallLevelManagerUpdate()
    {
        OnLevelManagerUpdate?.Invoke();
    }

    public void CallBossButtonPressed()
    {
        OnBossButtonPressed?.Invoke();
    }

    public void CallEnterBossFight()
    {
        OnEnterBossFight?.Invoke();
    }
}
