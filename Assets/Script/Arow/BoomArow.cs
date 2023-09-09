using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomArow : ArowCotroller
{
    [SerializeField] int mulDame;
    [SerializeField] float radius;
    protected override void ArowEffectAfterTrigger()
    {
        base.ArowEffectAfterTrigger();
        Explode();
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var cl in colliders)
        {
            DamageRecceiver damageRecceiver = cl.gameObject.GetComponent<DamageRecceiver>();
            if (damageRecceiver != null)
            {
                if (!cl.CompareTag("Player"))
                {
                    damageRecceiver.Deduct(mulDame, transform);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
