using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class BowCtrl : DamageRecceiver
{
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("FixBow", FixBow);
    }
    private void OnDisable()
    {
        EventManager.StopListening("FixBow", FixBow);
    }
    public void FixBow()
    {
        int current = EventManager.GetInt("FixBow");
        Add(current);
    }
}
