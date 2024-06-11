using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InstantiatedSlimeKing : MonoBehaviour, ISaveableInstantiatedObject, IInstantiatedBeing
{
    public static InstantiatedSlimeKing CreateInstantiatedObject(EnemyData enemyData, Vector2 position)
    {
        GameObject obj = Instantiate(GameResources.Instance.instanttiatedSlimeKingPrefab, position, Quaternion.identity);
        if (obj.TryGetComponent(out InstantiatedSlimeKing instantiatedSlimeKing))
        {
            instantiatedSlimeKing.InitializeInstance(enemyData);
            return instantiatedSlimeKing;
        }
        else
        {
            Debug.Log("Cant get component InstantiatedSlimeKing in instantiatedItemPrefab");
            return null;
        }
    }

    public bool IsEnemy() => tags.CompareTag(GameResources.Instance.enemy_Tag);

    public event Action OnDie;

    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private AttributeHolder attributeHolder;
    [SerializeField] private Health health;
    [SerializeField] private Tags tags;
    [SerializeField] private TagData tagData;

    private EnemyData enemyData;
    private SlimeKingDetailsSO slimeKingDetails;

    public void InitializeInstance(EnemyData slimeData)
    {
        this.enemyData = slimeData;
        slimeKingDetails = slimeData.enemyDetails as SlimeKingDetailsSO;
        if (slimeKingDetails == null)
            Debug.Log("cannot cast enemyDetails to slimeKingDetails");

        attributeHolder.SetUp(slimeKingDetails.attributeDataArray.baseAttributeArray);
        health.SetUp();
        stateMachine.StartStateMachine();
    }

    public EnemyData GetEnemyData()
    {
        if (enemyData != null)
            return enemyData;

        Debug.Log("no enemy data");
        return null;
    }

    public ISaveableData GetSaveableData()
    {
        return GetEnemyData();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Die()
    {
        OnDie?.Invoke();
        Destroy();
    }

    public void AddEnemyTag()
    {
        tagData.AddSelfTag(GameResources.Instance.enemy_Tag);
        tagData.AddFriendlyTag(GameResources.Instance.enemy_Tag);
        tagData.AddHostileTag(GameResources.Instance.player_Tag);
    }
}
