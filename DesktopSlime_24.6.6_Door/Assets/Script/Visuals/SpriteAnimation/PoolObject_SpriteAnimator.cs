using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteAnimator))]
public class PoolObject_SpriteAnimator : MonoBehaviour
{
    private ObjectPoolManager objectPoolManager;
    private GameObject poolKey;
    private SpriteAnimator spriteAnimator;

    private void Awake()
    {
        spriteAnimator = GetComponent<SpriteAnimator>();
    }

    private void OnEnable()
    {
        spriteAnimator.OnAnimationFinished += SpriteAnimator_OnAnimationFinished;
    }

    private void OnDisable()
    {
        spriteAnimator.OnAnimationFinished -= SpriteAnimator_OnAnimationFinished;
    }

    private void SpriteAnimator_OnAnimationFinished()
    {
        gameObject.SetActive(false);

        //Release object to pool
        objectPoolManager.ReleaseComponentToPool(poolKey, this);
    }

    public void PlayAnimation(SpriteAnimationSO spriteAnimationSO, GameObject poolKey, ObjectPoolManager objectPoolManager, Vector3 position, float angle)
    {
        this.objectPoolManager = objectPoolManager;

        this.poolKey = poolKey;

        transform.position = position;

        Vector3 eulerAngle = new Vector3(0, 0, angle);

        transform.eulerAngles = eulerAngle;

        gameObject.SetActive(true);

        spriteAnimator.PlayAnimation(spriteAnimationSO);
    }
}

