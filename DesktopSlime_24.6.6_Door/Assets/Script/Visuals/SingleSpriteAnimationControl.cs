using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpriteAnimationControl : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    private SpriteArray[] animationArray;

    private int animationIndex;
    private int frameIndex;

    public void SetUp(SpriteArray[] animationArray)
    {
        this.animationArray = animationArray;
        UpdateFrame(frameIndex);
    }

    // Called by animation clip
    public void UpdateAnimation(int animationIndex)
    {
        this.animationIndex = animationIndex;
    }

    // Called by animation clip
    public void UpdateFrame(int frameIndex)
    {
        if (animationArray != null)
        {
            if (!IsAnimationPlayable(animationArray, animationIndex, frameIndex))
                return;

            spriteRenderer.sprite = animationArray[animationIndex].spriteArray[frameIndex];
        }

        this.frameIndex = frameIndex;
    }

    private bool IsAnimationPlayable(SpriteArray[] animationArray, int animationIndex, int frameIndex)
    {
        if (animationIndex > animationArray.Length - 1)
        {
            Debug.Log("animation out of index");
            return false;
        }

        if (frameIndex > animationArray[animationIndex].spriteArray.Length - 1)
        {
            Debug.Log("frame " + frameIndex + " out of index "+ (animationArray[animationIndex].spriteArray.Length - 1));
            return false;
        }

        return true;
    }
}
