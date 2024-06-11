using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    public event Action<float> OnDealDamage;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private SpriteAnimator spriteAnimator;
    [SerializeField] private Hitbox_OnTriggerEnterOnce hitbox;

    private ObjectPoolManager poolManager;
    private DealDamage dealDamage;
    private ProjectileDetailsSO projectileDetails;

    private bool isAble = false;
    private Vector2 launchDirection;
    private float speed;
    private float range;
    private int hitCount;

    private void Awake()
    {
        hitbox.OnDealDamage += Hitbox_OnDealDamage;
    }

    private void Hitbox_OnDealDamage(float damage)
    {
        dealDamage.CallDealDirectDamage(damage);

        hitCount--;
        if (hitCount <= 0)
            DisableProjectile();
    }

    private void Update()
    {
        Vector3 distanceVector = launchDirection * speed * Time.deltaTime;

        transform.position += distanceVector;

        range -= distanceVector.magnitude;

        if (range < 0f)
        {
            DisableProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((layerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            DisableProjectile();
        }
    }

    public void InitializeProjectile(ObjectPoolManager poolManager, DealDamage dealDamage, Vector2 position, ProjectileDetailsSO projectileDetails, Vector2 launchDirection, float range,DamageData damageData, List<TagSO> enemyTagList)
    {
        this.poolManager = poolManager;
        this.dealDamage = dealDamage;
        transform.position = position;
        this.projectileDetails = projectileDetails;
        this.launchDirection = launchDirection;
        this.range = range;
        speed = projectileDetails.speed;
        hitCount = projectileDetails.hitCount;

        float projectileAngle = HelperUtils.GetAngleFromVector(launchDirection);
        transform.eulerAngles = new Vector3(0, 0, projectileAngle);

        hitbox.SetUpHitBox(damageData, enemyTagList, projectileDetails.hitBoxSize);

        spriteAnimator.PlayAnimation(projectileDetails.animationSpriteArray);

        isAble = true;

        gameObject.SetActive(true);
    }

    private void DisableProjectile()
    {
        if (!isAble)
            return;

        hitbox.SetDownHitBox();
        isAble = false;
        gameObject.SetActive(false);
        poolManager.ReleaseComponentToPool(projectileDetails.poolKey, this);
    }
}
