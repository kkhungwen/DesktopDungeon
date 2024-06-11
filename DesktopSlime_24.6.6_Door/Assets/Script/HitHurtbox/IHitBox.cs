using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHitBox 
{
    public event Action<float> OnDealDamage;
}
