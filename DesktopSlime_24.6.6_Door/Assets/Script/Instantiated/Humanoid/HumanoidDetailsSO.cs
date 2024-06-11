using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

[CreateAssetMenu(fileName = "HumanoidDetailsSO_", menuName = "Scriptable Objects/Humanoid Details")]
public class HumanoidDetailsSO : ScriptableObject
{
    public BaseAttributeArray attributeDataArray;

    public SpriteArray[] baseHeadAnimationArray = new SpriteArray[0];
    public SpriteArray[] baseBodyAnimationArray = new SpriteArray[0];

    public Sprite handSprite;
}
