using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event Action<OnHealthChangeEventArgs> OnHealthChange;
    public event Action OnEmptyHealth;

    [SerializeField] private TakeDamage takeDmage;
    [SerializeField] private TakeHeal takeHeal;
    [SerializeField] private Formula formula;

    private float maxHealth = 0;
    private float currentHealth = 0;

    private void Awake()
    {
        formula.OnAttributeValueChange += Formula_OnAttributeChange;
        takeDmage.OnTakeDamage += TakeDmage_OnTakeDamage;
        takeHeal.OnHeal += TakeHeal_OnHeal;
    }

    private void Formula_OnAttributeChange(AttributeValueChangeEventArgs attributeValueChangeEventArgs)
    {
        if (attributeValueChangeEventArgs.attributeType != GameResources.Instance.health_Attibute)
            return;

        UpdateMaxHealth(attributeValueChangeEventArgs.value);
    }

    private void TakeHeal_OnHeal(float amount)
    {
        AddHealth(amount);
    }

    private void TakeDmage_OnTakeDamage(TakedamageEventArgs eventArgs)
    {
        DecreaseHealth(eventArgs.damage);
    }

    public void SetUp()
    {
        UpdateMaxHealth(formula.GetAttributeValueModified(GameResources.Instance.health_Attibute));

        FullHealth();
    }

    public void FullHealth()
    {
        currentHealth = maxHealth;

        OnHealthChange?.Invoke(new OnHealthChangeEventArgs() { currentHealth = currentHealth, maxHealth = maxHealth });
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        OnHealthChange?.Invoke(new OnHealthChangeEventArgs() { currentHealth = currentHealth, maxHealth = maxHealth });

        if (currentHealth <= 0)
            OnEmptyHealth?.Invoke();
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;

        if (currentHealth < 0)
            currentHealth = 0;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        OnHealthChange?.Invoke(new OnHealthChangeEventArgs() { currentHealth = currentHealth, maxHealth = maxHealth });

        if (currentHealth <= 0)
            OnEmptyHealth?.Invoke();
    }

    public void UpdateMaxHealth(float healthValueModified)
    {
        maxHealth = healthValueModified;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        OnHealthChange?.Invoke(new OnHealthChangeEventArgs() { currentHealth = currentHealth, maxHealth = maxHealth });
    }

    public bool isEmptyHealth()
    {
        return currentHealth <= 0f;
    }
}

public class OnHealthChangeEventArgs : EventArgs
{
    public float maxHealth;
    public float currentHealth;
}
