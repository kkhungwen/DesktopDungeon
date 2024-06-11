using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCooldown 
{
    public AbilityCooldown(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;
        lastResetCooldownTime = Time.time;
        isCooldown = true;
    }

    public bool isCooldown { get; private set; }
    private float cooldownTime;
    private float lastResetCooldownTime;

    public void UpdateTime(float time)
    {
        if (!isCooldown)
            return;

        if(time > lastResetCooldownTime + cooldownTime)
        {
            isCooldown = false;
        }
    }

    public void ResetCooldownTime()
    {
        lastResetCooldownTime = Time.time;

        isCooldown = true;
    }
}
