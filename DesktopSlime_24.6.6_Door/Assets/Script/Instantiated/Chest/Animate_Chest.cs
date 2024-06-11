using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Animate_Chest : MonoBehaviour
{
    [SerializeField] private InstantiatedObjectInspecControl objectInspecControl;

    private Animator anima;

    private void Awake()
    {
        anima = GetComponent<Animator>();

        objectInspecControl.OnToggleOpen += ObjectInspecControl_OnToggleOpen;
    }

    private void ObjectInspecControl_OnToggleOpen(bool isOpen)
    {
        if (isOpen)
            anima.Play(Settings.open);
        else
            anima.Play(Settings.idle);
    }
}
