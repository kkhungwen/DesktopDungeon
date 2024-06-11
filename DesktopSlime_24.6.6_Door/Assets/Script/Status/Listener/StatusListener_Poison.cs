using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusListener_Poison : MonoBehaviour
{
    public event Action<float> OnDealPoisonDamage;

    [SerializeField] private TakeDamage takeDamage;
    [SerializeField] private Formula formula;
    [SerializeField] private StatusTypeSO poison_StatusType;

    private StatusHolder statusManager;

    private int poisonStack = 0;
    private float resetTime;

    private void Awake()
    {
        statusManager = GetComponentInParent<StatusHolder>();

        statusManager.OnStatusChange += StatusManager_OnStatusChange;
    }

    private void Update()
    {
        if (poisonStack <= 0)
            return;

        if (Time.time >= resetTime + Settings.poisonTickTime)
        {
            DealPoisonDamage();

            resetTime = Time.time;
        }
    }

    private void StatusManager_OnStatusChange(StatusTypeSO statusType, int stack)
    {
        if (statusType != poison_StatusType)
            return;

        poisonStack = stack;
    }

    private void DealPoisonDamage()
    {
        OnDealPoisonDamage?.Invoke(formula.GetPoisonDamage(poisonStack));
    }
}
