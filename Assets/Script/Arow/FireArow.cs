using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArow : ArowCotroller
{
    [SerializeField] float radius;
    [SerializeField] float delaySendDamage;
    [SerializeField] float timeAddDame;
    float currentTime = 1;
    float currentTimeAddDame;
    bool isSend;
    protected override void Update()
    {
        base.Update();
        if (isSend)
        {
            if(currentTimeAddDame > 0)
            {
                currentTimeAddDame -= Time.deltaTime;
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    SendDame();
                }
            }
            else
            {
                vfx.SetActive(false);
            }
        }

    }
    protected override void ResetValue()
    {
        base.ResetValue();
        isSend = false;
        currentTimeAddDame = timeAddDame;
    }
    protected override void ArowEffectAfterTrigger()
    {
        base.ArowEffectAfterTrigger();
        currentTime = delaySendDamage;
        currentTimeAddDame = timeAddDame;
        isSend = true;

    }
    void SendDame()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var cl in colliders)
        {
            if (!cl.CompareTag("Player"))
            {
                damageSender.Send(cl.transform, transform);
            }
        }
        currentTime = delaySendDamage;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
