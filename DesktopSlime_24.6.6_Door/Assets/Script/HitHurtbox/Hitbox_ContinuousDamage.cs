using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Hitbox_ContinuousDamage : MonoBehaviour, IHitBox
{
    public event Action<float> OnDealDamage;

    private BoxCollider2D boxCollider;
    private DamageData damageData;
    private List<HitColliderTimer> hitColliderTimerList = new();
    private List<TagSO> enemyTagList;
    private float damageTickTime = 1f;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void SetUpHitBox(DamageData damageData, List<TagSO> enemyTagList)
    {
        this.damageData = damageData;
        this.enemyTagList = enemyTagList;
    }

    public void SetUpHitBox(DamageData damageData, List<TagSO> enemyTagList, Vector2 size)
    {
        this.damageData = damageData;
        this.enemyTagList = enemyTagList;
        boxCollider.size = size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageData == null)
            return;
        HandleHit(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (damageData == null)
            return;
        HandleHit(collision);
    }

    private void HandleHit(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Tags collisionTags))
        {
            Debug.Log("HurtBox should always contain Tags");
            return;
        }

        if (!collisionTags.CompareTag(enemyTagList))
            return;

        if (!collision.TryGetComponent(out HurtBox hurtBox))
        {
            Debug.Log("HitBox should only collide with hurtBox");
            return;
        }

        for (int i = hitColliderTimerList.Count - 1; i >= 0; i--)
        {
            if (Time.time >= hitColliderTimerList[i].hitTime + damageTickTime)
                hitColliderTimerList.RemoveAt(i);
        }

        foreach (HitColliderTimer hitColliderTimer in hitColliderTimerList)
        {
            if (hitColliderTimer.hitCollider == collision)
                return;
        }

        // Set damage position to collider position
        damageData.damagePosition = transform.position;

        hurtBox.GetAttack(damageData, out float damage);

        OnDealDamage.Invoke(damage);

        hitColliderTimerList.Add(new HitColliderTimer(collision, Time.time));
    }
}

public class HitColliderTimer
{
    public HitColliderTimer(Collider2D hitCollider, float hitTime)
    {
        this.hitCollider = hitCollider;
        this.hitTime = hitTime;
    }

    public Collider2D hitCollider { get; private set; }
    public float hitTime { get; private set; }
}
