using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArowDespawn : DespawnByTime
{
    protected override void OnEnable()
    {
        base.OnEnable();
        currenTime = delayTime;
    }
    private void OnDisable()
    {
        isDespawn = false;
    }
    public override void DespawnObject()
    {
        ArowSpawner.Instance.Despawn(transform);
    }
    public void OnDespawn()
    {
        isDespawn = true;
    }
    public void SetCurrentTime()
    {
        currenTime = delayTime;
    }
}
