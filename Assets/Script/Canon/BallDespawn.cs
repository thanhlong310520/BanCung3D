using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDespawn : DespawnByTime
{
    protected override void OnEnable()
    {
        base.OnEnable();
        isDespawn = true;
        currenTime = delayTime;
    }
    public override void DespawnObject()
    {
        BallSpawner.Instance.Despawn(transform);
    }
}
