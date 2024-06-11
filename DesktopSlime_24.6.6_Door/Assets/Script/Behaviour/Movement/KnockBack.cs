using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveByVelocity))]
public class KnockBack : MonoBehaviour
{
    private MoveByVelocity moveByVelocity;
    [SerializeField] private TakeDamage takeDamage;

    public bool isAble = true;

    private void Awake()
    {
        takeDamage.OnTakeDamage += TakeDamage_OnTakeDamage;

        moveByVelocity = GetComponent<MoveByVelocity>();
    }

    private void TakeDamage_OnTakeDamage(TakedamageEventArgs eventArgs)
    {
        if (!isAble)
            return;

        if (!eventArgs.isDirect)
            return;

        Knock(!eventArgs.isDamageFromRight, eventArgs.knockBackStrength, Settings.knockUpDirection_Right);
    }

    public void Knock(bool isRight, float strength, Vector2 right_Direction)
    {
        if (isRight)
            moveByVelocity.MoveRigidBody(right_Direction.normalized, strength, false, false);
        else
            moveByVelocity.MoveRigidBody(new Vector2(-right_Direction.x, right_Direction.y).normalized, strength, false, false);
    }
}
