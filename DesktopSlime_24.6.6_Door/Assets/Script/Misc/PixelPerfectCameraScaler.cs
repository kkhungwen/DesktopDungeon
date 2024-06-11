using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(PixelPerfectCamera))]
[DisallowMultipleComponent]
public class PixelPerfectCameraScaler : MonoBehaviour
{
    private PixelPerfectCamera pixelPerfectCamera;

    [SerializeField] private int refResolutionX;

    private void Awake()
    {
        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();

        float aspectRatio = (float)Screen.height / (float)Screen.width;

        pixelPerfectCamera.refResolutionX = refResolutionX;
        pixelPerfectCamera.refResolutionY = (int)(refResolutionX * aspectRatio);
    }
}
