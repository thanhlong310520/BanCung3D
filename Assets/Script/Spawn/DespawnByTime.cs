using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : Despawn
{
    protected bool isDespawn;
    [SerializeField]protected float delayTime;
    [SerializeField]protected float currenTime;
    protected override bool CanDespawn()
    {
        if (currenTime <= 0) return true;
        if (isDespawn)
        {
            currenTime -= Time.fixedDeltaTime;
        }
        return false;
    }
}
