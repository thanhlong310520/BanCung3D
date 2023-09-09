using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TigerForge;

public class ItemOfBag : MonoBehaviour
{
    public int id;
    [SerializeField] Image icon;
    [SerializeField] TMP_Text number;
    [SerializeField] Button bt;

    private void Awake()
    {
        bt.onClick.AddListener(ShowInfor);
    }

    private void ShowInfor()
    {
        EventManager.EmitEventData("ShowInforItem", id);
    }

    public void SetIcon(Sprite icon)
    {
        this.icon.sprite = icon;
    }
    public void SetNumber(string nb)
    {
        number.text = nb;
    }
}
