using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileDetailsSO_", menuName = "Scriptable Objects/Projectile Details")]
public class ProjectileDetailsSO : ScriptableObject
{
    public GameObject poolKey;
    public float speed;
    public Vector2 hitBoxSize;
    public Sprite[] animationSpriteArray;
    public int hitCount;
}
