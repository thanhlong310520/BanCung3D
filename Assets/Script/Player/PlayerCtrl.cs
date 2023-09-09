using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MyMonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;
    [SerializeField] PlayerItems playerItems;
    public PlayerItems PlayerItems => playerItems;
    [SerializeField] PlayerDamageRecciever playerDamageRecciever;
    public PlayerDamageRecciever PlayerDamageRecciever => playerDamageRecciever;
    [SerializeField] BowCtrl bowCtrl;
    public BowCtrl BowCtrl => bowCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerMovement();
        LoadPlayerItems();
        LoadPlayerDamageRecciever();
        LoadBowCtrl();
    }

    private void LoadBowCtrl()
    {
        bowCtrl = GetComponent<BowCtrl>();
    }

    private void LoadPlayerDamageRecciever()
    {
        playerDamageRecciever = GetComponent<PlayerDamageRecciever>();
    }

    private void LoadPlayerMovement()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }
    private void LoadPlayerItems()
    {
        playerItems = gameObject.GetComponent<PlayerItems>();
    }
}
