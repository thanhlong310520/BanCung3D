using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MyMonoBehaviour
{
    bool isClear;
    public GameObject cannons;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCannons();
    }

    protected override void Start()
    {
        base.Start();
        isClear = false;
    }

    protected override void Update()
    {
        base.Update();
        if (!isClear)
        {
            CheckActive();
        }
    }

    private void CheckActive()
    {
        int count = 0;
        foreach (Transform cn in cannons.transform)
        {
            if (!cn.gameObject.activeInHierarchy)
            {
                count++;
            }
        }
        if(count == cannons.transform.childCount)
        {
            isClear = true;
            GameManager.Instance.IsClearEnemy();
        }
    }

    private void LoadCannons()
    {
        cannons = GameObject.Find("Cannons");
    }
}
