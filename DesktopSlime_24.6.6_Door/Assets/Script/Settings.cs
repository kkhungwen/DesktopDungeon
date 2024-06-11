using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    // ATTRIBUTE LEVEL
    public const int max_BaseAttributeLevel = 100;

    public const int max_ItemLevel = 48;
    public const int itemLevelPerForge = 12;
    public const int itemLevelPerBonusStat = 3;
    public const int max_ForgeLevel = 3;

    // POISON DAMAGE
    public const float poisonDamage = 0.003f;
    public const float poisonTickTime = 1f;

    // KNOCK UP
    public static Vector2 knockUpDirection_Right = new Vector2(1,2);

    // FORMULA
    public const float absorbConstant = 100f;

    // VISION
    public const float checkVisionTime = 0.25f;

    // AREA EFFECT
    public const float checkAreaTime = 0.25f;

    #region Animator Parameters

    public static int idle = Animator.StringToHash("Idle");
    public static int idleBattle = Animator.StringToHash("IdleBattle");
    public static int move = Animator.StringToHash("Move");
    public static int moveBattle= Animator.StringToHash("MoveBattle");
    public static int alert = Animator.StringToHash("Alert");
    public static int drag = Animator.StringToHash("Drag");
    public static int fall = Animator.StringToHash("Fall");
    public static int die = Animator.StringToHash("Die");
    public static int open = Animator.StringToHash("Open");

    #endregion Animator Parameters
}
