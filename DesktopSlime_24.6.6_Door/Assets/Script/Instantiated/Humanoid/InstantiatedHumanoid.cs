using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InstantiatedHumanoid : MonoBehaviour, ISaveableInstantiatedObject, IInstantiatedBeing
{
    public static InstantiatedHumanoid CreateInstantiatedObject(HumanoidData humanoidData, Vector2 position)
    {
        GameObject obj = Instantiate(GameResources.Instance.instantiatedHumanoidPrefab, position, Quaternion.identity);
        if (obj.TryGetComponent(out InstantiatedHumanoid instantiatedHumanoid))
        {
            instantiatedHumanoid.InitializeInstance(humanoidData);
            return instantiatedHumanoid;
        }
        else
        {
            Debug.Log("Cant get component InstantiatedHumanoid in instantiatedItemPrefab");
            return null;
        }
    }

    public bool IsEnemy() => tags.CompareTag(GameResources.Instance.enemy_Tag);

    public event Action OnDie;

    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private AttributeHolder attributeManager;
    [SerializeField] private Weapon_Humanoid weapon;
    [SerializeField] private Equipments_Humanoid equipments;
    [SerializeField] private Health health;
    [SerializeField] private InstantiatedObjectInspecControl instantiatedObjectInspecControl;
    [SerializeField] private SpriteAnimationControl_Humanoid spriteAnimationControl;
    [SerializeField] private HandSpriteReplacement handSpriteReplacement;
    [SerializeField] private Tags tags;
    [SerializeField] private TagData tagData;

    private HumanoidData humanoidData;

    public void InitializeInstance(HumanoidData humanoidData)
    {
        this.humanoidData = humanoidData;

        attributeManager.SetUp(humanoidData.humanoidDetails.attributeDataArray.baseAttributeArray);
        weapon.SetUp();
        equipments.SetUp(humanoidData);
        health.SetUp();
        instantiatedObjectInspecControl.SetUp(humanoidData);
        spriteAnimationControl.SetUp(humanoidData.humanoidDetails.baseHeadAnimationArray, humanoidData.humanoidDetails.baseBodyAnimationArray);
        handSpriteReplacement.SetUp(humanoidData.humanoidDetails.handSprite);
        stateMachine.StartStateMachine();
    }

    public HumanoidData GetHumanoidData()
    {
        if (humanoidData != null)
            return humanoidData;

        Debug.Log("no humanoid data");
        return null;
    }

    public ISaveableData GetSaveableData()
    {
        return GetHumanoidData();
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
    }

    public void AddEnemyTag()
    {
        tagData.AddSelfTag(GameResources.Instance.enemy_Tag);
        tagData.AddFriendlyTag(GameResources.Instance.enemy_Tag);
        tagData.AddHostileTag(GameResources.Instance.player_Tag);
    }
}
