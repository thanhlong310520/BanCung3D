using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class PlayerDamageRecciever : DamageRecceiver
{
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("Heal", Heal);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Heal", Heal);
    }
    protected override void OnDead()
    {
        base.OnDead();
        GameManager.Instance.PlayerDead();
        gameObject.SetActive(false);
    }
    public void Heal()
    {
        int current = EventManager.GetInt("Heal");
        Add(current);
    }
}
