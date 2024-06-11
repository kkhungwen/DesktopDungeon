using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    public event Action OnAnimationFinished;

    private SpriteRenderer spriteRenderer;
    private float defaultSecPerClip = 0.125f;

    private Sprite[] spriteArray;
    private float secPerClip;
    private bool isLoop;
    private bool isPlaying = false;
    private bool isAnimationFinished;
    private int clipCount = 0;
    private float timeCount = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // if playing animation
        if (!isPlaying)
            return;

        timeCount += Time.deltaTime;

        clipCount = Mathf.FloorToInt(timeCount / secPerClip) % spriteArray.Length;

        // when finish one animation loop check if continue looping
        if (clipCount == spriteArray.Length - 1 && !isLoop)
            isAnimationFinished = true;

        if (clipCount == 0 && isAnimationFinished)
        {
            FinishPlaying();
            return;
        }

        spriteRenderer.sprite = spriteArray[clipCount];
    }

    public void PlayAnimation(SpriteAnimationSO spriteAnimationSO)
    {
        spriteArray = spriteAnimationSO.spriteArray;
        secPerClip = spriteAnimationSO.secPerClip;
        isLoop = spriteAnimationSO.isLoop;
        clipCount = 0;
        timeCount = 0;

        isPlaying = true;
    }

    public void PlayAnimation(Sprite[] spriteArray)
    {
        this.spriteArray = spriteArray;
        this.secPerClip = defaultSecPerClip;
        isLoop = true;
        isAnimationFinished = false;
        clipCount = 0;
        timeCount = 0;

        isPlaying = true;
    }

    public void FinishPlaying()
    {
        isPlaying = false;

        OnAnimationFinished?.Invoke();
    }
}
