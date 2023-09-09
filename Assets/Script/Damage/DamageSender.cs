using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MyMonoBehaviour
{
    [SerializeField] protected int damage = 1;
    public virtual void Send(Transform obj,Transform val)
    {
        DamageRecceiver damageReceiver;
        damageReceiver = obj.GetComponentInChildren<DamageRecceiver>();
        if (damageReceiver == null) return;
        Send(damageReceiver,val);

    }

    protected virtual void Send(DamageRecceiver damageReceiver, Transform val)
    {
        damageReceiver.Deduct(damage,val);
    }
}
