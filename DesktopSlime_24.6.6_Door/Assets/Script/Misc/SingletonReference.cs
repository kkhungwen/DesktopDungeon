using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonReference : SingletonMonoBehaviour<SingletonReference>
{
    public GameEventHandler gameEventHandler;
    public Boundaries boundaries;
    public ObjectPoolManager objectPoolManager;
    public SaveLoadManager saveLoadManager;
    public ScriptableObjectIDManager scriptableObjectIDManager;
    public EnemyManager enemyManager;
    public LevelManager levelManager;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(boundaries), boundaries);
        HelperUtils.ValidateCheckNullValue(this, nameof(objectPoolManager), objectPoolManager);
    }
#endif
    #endregion Validation
}
