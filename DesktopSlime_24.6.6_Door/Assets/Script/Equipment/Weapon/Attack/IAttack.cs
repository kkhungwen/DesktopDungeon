using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    public void SetUpAttack(WeaponDetailsSO weaponDetails);

    public void SetDownAttack();

    public void Attack();

    public void EndAttack();

    public void WeaponUpdate();
}
