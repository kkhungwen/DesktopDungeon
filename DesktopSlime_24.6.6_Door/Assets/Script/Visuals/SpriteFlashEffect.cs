using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlashEffect : MonoBehaviour
{
    [SerializeField] private TakeDamage takeDamage;
    [SerializeField] private SpriteRenderer[] spriteRendererArray;
    [SerializeField] private float flashTime;
    [SerializeField] private Material flashMaterial;

    private Material[] originalMaterialArray;
    bool isFlash = false;
    private float startFlashTime;

    private void Awake()
    {
        originalMaterialArray = new Material[spriteRendererArray.Length];
    }

    private void OnEnable()
    {
        takeDamage.OnTakeDamage += TakeDamage_OnTakeDamage;
    }

    private void OnDisable()
    {
        takeDamage.OnTakeDamage -= TakeDamage_OnTakeDamage;
    }

    private void TakeDamage_OnTakeDamage(TakedamageEventArgs eventArgs)
    {
        if (eventArgs.isDirect)
            Flash();
    }

    private void Update()
    {
        if (!isFlash)
            return;

        if (Time.time < startFlashTime + flashTime)
            return;

         EndFlash();
    }

    public void Flash()
    {
        if (isFlash)
            return;

        for(int i = 0; i< spriteRendererArray.Length; i++)
        {
            originalMaterialArray[i] = spriteRendererArray[i].material;
            spriteRendererArray[i].material = flashMaterial;
        }

        startFlashTime = Time.time;

        isFlash = true;
    }

    private void EndFlash()
    {
        for (int i = 0; i < spriteRendererArray.Length; i++)
        {
            spriteRendererArray[i].material = originalMaterialArray[i];
        }

        isFlash = false;
    }
}
