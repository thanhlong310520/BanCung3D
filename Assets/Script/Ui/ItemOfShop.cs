using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using TigerForge;

public class ItemOfShop : MonoBehaviour
{
    int id;
    [SerializeField] Button bt;
    [SerializeField] Image icon;
    [SerializeField] TMP_Text txt;
    private void OnEnable()
    {
        bt.onClick.AddListener(ShowItem);
    }

    private void ShowItem()
    {
        EventManager.EmitEventData("ShowItemShop", id);
    }
    public void SetID(int id)
    {
        this.id = id;
    }
    public int GetID()
    {
        return id;
    }
    public void SetCoint(int coint)
    {
        txt.text = coint + "";
    }
    public void SetIcon(Sprite icon)
    {
        this.icon.sprite = icon;
    }
}
