using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCooldownManager : MonoBehaviour
{
    private List<AbilityCooldown> abilityCooldownList = new();

    private void Update()
    {
        foreach (AbilityCooldown abilityCooldown in abilityCooldownList)
        {
            abilityCooldown.UpdateTime(Time.time);
        }
    }

    public AbilityCooldown CreateAbilityCooldown(float abilityCooldownTime)
    {
        AbilityCooldown abilityCooldown = new AbilityCooldown(abilityCooldownTime);
        abilityCooldownList.Add(abilityCooldown);

        return abilityCooldown;
    }
}
