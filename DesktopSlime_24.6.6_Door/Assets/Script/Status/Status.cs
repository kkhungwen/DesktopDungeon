using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Status
{
    public Status(StatusTypeSO statusType)
    {
        this.statusType = statusType;
    }

    public event Action<StatusTypeSO, int> OnStatusChange;

    [field: SerializeField] public StatusTypeSO statusType { get; private set; }

    [SerializeField] private List<StatusTimer> statusTimerList = new List<StatusTimer>();

    private int totalStack = 0;

    public void AddStack(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            StatusTimer statusTimer = new StatusTimer(statusType.duration);

            statusTimerList.Add(statusTimer);

            totalStack++;
        }

        OnStatusChange?.Invoke(statusType, GetStack());
    }

    public bool UpdateTimer(float deltaTime)
    {
        for (int i = statusTimerList.Count - 1; i >= 0; i--)
        {
            statusTimerList[i].duration -= deltaTime;

            if (statusTimerList[i].duration <= 0)
            {
                statusTimerList.RemoveAt(i);

                OnStatusChange?.Invoke(statusType, GetStack());
            }
        }

        if (statusTimerList.Count > 0)
            return true;
        else
            return false;
    }

    private int GetStack()
    {
        if (statusTimerList.Count <= 0)
            return 0;

        int stack = 1;

        if (statusType.resetDurationOnStack)
            stack = totalStack;
        else
            stack = statusTimerList.Count;

        if (stack > statusType.maxStack)
            stack = statusType.maxStack;

        return stack;
    }
}
