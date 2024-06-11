using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusHolder : MonoBehaviour
{
    [SerializeField] private TakeDamage takeDamage;
    [SerializeField] private HurtBox hurtBox;
    [SerializeField] private AffixListener_AddStatusToSelf affixListener_AddStatusToSelf;

    [SerializeField] private List<Status> statusList = new List<Status>();
    public event Action<StatusTypeSO, int> OnStatusChange;

    private void Awake()
    {
        takeDamage.OnTakeDamage += TakeDamage_OnTakeDamage;
        hurtBox.OnAddStatus += HurtBox_OnAddStatus;
        affixListener_AddStatusToSelf.OnAddStatusToSelf += AffixListener_AddStatusToSelf_OnAddStatusToSelf;
    }

    private void HurtBox_OnAddStatus(StatusTypeSO statusType, int stack)
    {
        AddStatus(statusType, stack);
    }

    private void AffixListener_AddStatusToSelf_OnAddStatusToSelf(StatusTypeSO statusType, int stack)
    {
        AddStatus(statusType, stack);
    }

    private void TakeDamage_OnTakeDamage(TakedamageEventArgs eventArgs)
    {
        if (eventArgs.passingStatusCountDic == null)
            return;

        foreach (KeyValuePair<StatusTypeSO,int> pair in eventArgs.passingStatusCountDic)
        {
            AddStatus(pair.Key, pair.Value);
        }
    }

    private void Update()
    {
        for (int i = statusList.Count - 1; i >= 0; i--)
        {
            if (!statusList[i].UpdateTimer(Time.deltaTime))
            {
                statusList[i].OnStatusChange -= Status_OnStatusChange;

                statusList.RemoveAt(i);
            }
        }
    }

    private void AddStatus(StatusTypeSO statusType, int amount)
    {
        // If already contains status type
        foreach (Status status in statusList)
        {
            if (status.statusType == statusType)
            {
                status.AddStack(amount);
                return;
            }
        }

        // Create new status
        Status newStatus = new Status(statusType);

        newStatus.OnStatusChange += Status_OnStatusChange;

        newStatus.AddStack(amount);

        statusList.Add(newStatus);
    }

    private void Status_OnStatusChange(StatusTypeSO statusType, int stack)
    {
        OnStatusChange?.Invoke(statusType, stack);
    }
}
