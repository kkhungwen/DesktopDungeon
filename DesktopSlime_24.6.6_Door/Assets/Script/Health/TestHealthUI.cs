using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TestHealthUI : MonoBehaviour
{
    [SerializeField] private Health health;

    private TextMeshPro textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        health.OnHealthChange += Health_OnHealthChange;
    }

    private void OnDisable()
    {
        health.OnHealthChange -= Health_OnHealthChange;
    }

    private void Health_OnHealthChange(OnHealthChangeEventArgs eventArgs)
    {
        textMeshPro.text = Mathf.RoundToInt(eventArgs.currentHealth) + " / " + Mathf.RoundToInt(eventArgs.maxHealth);
    }
}
