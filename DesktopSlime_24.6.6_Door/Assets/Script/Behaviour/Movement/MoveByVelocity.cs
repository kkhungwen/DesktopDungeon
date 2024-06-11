using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class MoveByVelocity : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveRigidBody(Vector2 moveDirection, float moveSpeed, bool isXaxis, bool isYaxis)
    {
        if (isXaxis)
        {
            Vector2 velocity = rb.velocity;
            velocity.x = moveDirection.x * moveSpeed;
            rb.velocity = velocity;
            return;
        }

        if (isYaxis)
        {
            Vector2 velocity = rb.velocity;
            velocity.y = moveDirection.y * moveSpeed;
            rb.velocity = velocity;
            return;
        }

        rb.velocity = moveDirection * moveSpeed;
    }
}
