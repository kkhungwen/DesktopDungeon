using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundCheck : MonoBehaviour
{
    private BoxCollider2D collisionCollider;

    [SerializeField] private LayerMask groundLayerMask;
    private ContactFilter2D groundContactFilter;

    private RaycastHit2D[] groundRaycastHitArray = new RaycastHit2D[1];

    private void Awake()
    {
        collisionCollider = GetComponent<BoxCollider2D>();

        groundContactFilter.SetLayerMask(groundLayerMask);
        groundContactFilter.useLayerMask = true;
    }

    public bool IsGrounded()
    {
        Array.Clear(groundRaycastHitArray, 0, groundRaycastHitArray.Length);

        float castDistance = 0.1f;

        collisionCollider.Cast(Vector2.down, groundContactFilter, groundRaycastHitArray, castDistance);

        foreach(RaycastHit2D raycastHit in groundRaycastHitArray)
        {
            if (raycastHit.collider != null)
                return true;
        }

        return false;
    }
}
