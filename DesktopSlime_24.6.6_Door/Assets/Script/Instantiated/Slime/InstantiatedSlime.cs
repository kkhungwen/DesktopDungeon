using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InstantiatedSlime : MonoBehaviour, ISaveableInstantiatedObject, IInstantiatedBeing
{
    public static InstantiatedSlime CreateInstantiatedObject(EnemyData slimeData, Vector2 position)
    {
        GameObject obj = Instantiate(GameResources.Instance.instantiatedSlimePrefab, position, Quaternion.identity);
        if (obj.TryGetComponent(out InstantiatedSlime instantiatedSlime))
        {
            instantiatedSlime.InitializeInstance(slimeData);
            return instantiatedSlime;
        }
        else
        {
            Debug.Log("Cant get component InstantiatedSlime in instantiatedItemPrefab");
            return null;
        }
    }

    public bool IsEnemy() => tags.CompareTag(GameResources.Instance.enemy_Tag);

    public event Action OnDie;

    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private AttributeHolder attributeHolder;
    [SerializeField] private Health health;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Tags tags;
    [SerializeField] private TagData tagData;

    private EnemyData enemyData;
    private SlimeDetailsSO slimeDetails;
    private Material createdMaterial;

    public void InitializeInstance(EnemyData slimeData)
    {
        this.enemyData = slimeData;
        slimeDetails = slimeData.enemyDetails as SlimeDetailsSO;
        if (slimeDetails == null)
            Debug.Log("cannot cast enemyDetails to slimeDetails");

        attributeHolder.SetUp(slimeDetails.attributeDataArray.baseAttributeArray);
        health.SetUp();
        stateMachine.StartStateMachine();
        SetVisuals();
    }

    private void SetVisuals()
    {
        Color color = slimeDetails.colorArray[UnityEngine.Random.Range(0, slimeDetails.colorArray.Length)];
        createdMaterial = new Material(GameResources.Instance.customColorSlimeShader);
        spriteRenderer.material = createdMaterial;
        createdMaterial.color = color;
        spriteRenderer.sprite = slimeDetails.sprite;
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
        Destroy(createdMaterial);
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
