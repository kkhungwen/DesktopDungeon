using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Health health;
    [SerializeField] private RectTransform healthBarTransForm;
    [SerializeField] private RectTransform backgorundTransform;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Gradient healthBarGradient;

    float maxHealthBarLength = 0.875f;
    float healthBarWidthEdge = 0.125f;
    float healthBarHeight = 0.375f;
    float backgroundHeight = 0.25f;

    private void Awake()
    {
        health = GetComponentInParent<Health>();

        health.OnHealthChange += Health_OnHealthChange;
    }

    private void Health_OnHealthChange(OnHealthChangeEventArgs args)
    {

        float healthBarWidth = maxHealthBarLength * (args.currentHealth / args.maxHealth) + healthBarWidthEdge;
        healthBarTransForm.sizeDelta = new Vector2(healthBarWidth, healthBarHeight);

        float backGroundWidth = maxHealthBarLength * (1 - args.currentHealth / args.maxHealth);
        backgorundTransform.sizeDelta = new Vector2(backGroundWidth, backgroundHeight);

        healthBarImage.color = healthBarGradient.Evaluate(args.currentHealth / args.maxHealth);
        backgroundImage.color = healthBarGradient.Evaluate(args.currentHealth / args.maxHealth);
    }
}
