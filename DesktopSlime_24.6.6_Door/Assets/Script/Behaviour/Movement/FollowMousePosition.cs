using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveByVelocity))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class FollowMousePosition : MonoBehaviour
{
    private Rigidbody2D rb;
    private MoveByVelocity moveByVelocity;
    private float originalGravity;
    private Vector2 offset;
    private bool isFollowing = false;

    private const float followSoftRange = 0.1f;
    private const float followStrength = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveByVelocity = GetComponent<MoveByVelocity>();

        originalGravity = rb.gravityScale;
    }

    private void FixedUpdate()
    {
        if (!isFollowing)
            return;

        Vector2 adjustTarget = HelperUtils.GetMouseWorldPosition() - offset;

        float distance = Vector2.Distance(adjustTarget, transform.position);
        if (distance < followSoftRange)
        {
            moveByVelocity.MoveRigidBody(Vector2.zero, 0, false, false);
            return;
        }

        // Move to mouse position
        Vector2 direction = adjustTarget - (Vector2)transform.position;
        moveByVelocity.MoveRigidBody(direction, followStrength, false, false);
    }

    public void StartFollow(Vector2 offset)
    {
        rb.gravityScale = 0;
        this.offset = offset;
        isFollowing = true;
    }

    public void EndFollow()
    {
        rb.gravityScale = originalGravity;
        isFollowing = false;
    }
}
