using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteAnimationSO_", menuName = "Scriptable Objects/Sprite Animation")]
public class SpriteAnimationSO : ScriptableObject
{
    public Sprite[] spriteArray;
    public float secPerClip;
    public bool isLoop;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtils.ValidateCheckEnumerableValues(this, nameof(spriteArray), spriteArray);
        HelperUtils.ValidateCheckPositiveValue(this, nameof(secPerClip), secPerClip, false);
    }
#endif
    #endregion
}
