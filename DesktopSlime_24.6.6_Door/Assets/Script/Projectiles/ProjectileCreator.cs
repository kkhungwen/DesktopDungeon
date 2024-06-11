using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCreator : MonoBehaviour
{
    [SerializeField] DealDamage dealDamage;
    [SerializeField] TagData tagData;
    [SerializeField] FaceDirection faceDirection;

    public void CreateProjectile(Vector2 position, ProjectileDetailsSO projectileDetails, Vector2 direction, float range, DamageData damageData)
    {
        ObjectPoolManager poolManager = SingletonReference.Instance.objectPoolManager;
        Projectile projectile = poolManager.GetComponentFromPool(projectileDetails.poolKey) as Projectile;

        if (projectile == null)
            Debug.Log("cant cast component to projectile");

        if (!faceDirection.isRight)
            direction = new Vector2(-direction.x, direction.y);

        projectile.InitializeProjectile(poolManager, dealDamage, position, projectileDetails, direction, range, damageData, tagData.hostileTagList);
    }
}
