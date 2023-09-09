using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagUI : MonoBehaviour
{
    [SerializeField] List<ItemOwner> items;
    [SerializeField] GameObject hienthi;
    [SerializeField] Transform content;
    [SerializeField] Image iconInfor;
    [SerializeField] TMP_Text txtInfor;

    [SerializeField] Button exit;
    private void OnEnable()
    {
        items = GameCtrl.Instance.PlayerCtrl.PlayerItems.GetItemOwners();
        hienthi = GameCtrl.Instance.ItemForBagUI;
        exit.onClick.AddListener(ExitBagUI);
        SetDtForHT();
        EventManager.StartListening("ShowInforItem", ShowInfor);
    }
    private void OnDisable()
    {
        EventManager.StopListening("ShowInforItem", ShowInfor);
    }
    private void Start()
    {
        
    }
    private void ShowInfor()
    {
        int inforID = EventManager.GetInt("ShowInforItem");
        foreach (var p in items)
        {
            if (p.itemDt.id == inforID)
            {
                iconInfor.sprite = p.itemDt.icon;
                txtInfor.text = "" + p.soluong;
            }
        }
    }
    void SetDtForHT()
    {
        if (items != null)
        {
            foreach (var dt in items)
            {
                bool ishas = false;
                foreach (Transform chillContent in content)
                {
                    var ip = chillContent.gameObject.GetComponent<ItemOfBag>();
                    if (dt.itemDt.id == ip.id)
                    {
                        ishas = true;
                        ip.SetNumber(dt.soluong + "");
                    }
                }
                if (!ishas)
                {
                    GameObject temp = Instantiate(hienthi);
                    temp.transform.SetParent(content);
                    temp.transform.localScale = new Vector3(1, 1, 1);
                    var ic = temp.GetComponent<ItemOfBag>();
                    ic.SetNumber("" + dt.soluong);
                    ic.id = dt.itemDt.id;
                    ic.SetIcon(dt.itemDt.icon);
                }

            }
        }

    }
    private void ExitBagUI()
    {
        gameObject.SetActive(false);
    }
}
