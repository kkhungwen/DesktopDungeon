using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Hitbox_OnTriggerEnterOnce : MonoBehaviour,IHitBox
{
    public event Action<float> OnDealDamage;

    private BoxCollider2D boxCollider;
    private DamageData damageData;
    private List<Collider2D> hitColliderList = new();
    private List<TagSO> enemyTagList;
    private bool isAble;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void SetUpHitBox(DamageData damageData, List<TagSO> enemyTagList)
    {
        hitColliderList.Clear();
        this.damageData = damageData;
        this.enemyTagList = enemyTagList;
        isAble = true;
    }

    public void SetUpHitBox(DamageData damageData, List<TagSO> enemyTagList, Vector2 size)
    {
        this.damageData = damageData;
        this.enemyTagList = enemyTagList;
        boxCollider.size = size;
        hitColliderList.Clear();
        isAble = true;
    }

    public void SetDownHitBox()
    {
        isAble = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAble)
            return;

        if (hitColliderList.Contains(collision))
            return;

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

        // Set damage position to collider position
        damageData.damagePosition = transform.position;

        hurtBox.GetAttack(damageData, out float damage);

        OnDealDamage.Invoke(damage);

        hitColliderList.Add(collision);
    }
}
