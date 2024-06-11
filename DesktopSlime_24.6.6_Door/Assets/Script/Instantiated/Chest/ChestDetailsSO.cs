using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ChestDetailsSO_",menuName = "Scriptable Objects/Chest Details")]
public class ChestDetailsSO : ScriptableObject
{
    public int inventorySize;
    // index0:idle frame:1 // index1:open frame:3
    public SpriteArray[] animationArray;

    public Sprite iconSprite;
}
