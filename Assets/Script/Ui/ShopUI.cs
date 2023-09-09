using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    [SerializeField] Transform content;
    [SerializeField] Button exitBt;
    int id;
    [SerializeField] Image icon;
    [SerializeField] TMP_Text nametxt;
    [SerializeField] TMP_Text cointTxt;
    [SerializeField] TMP_InputField numberItem;
    [SerializeField] TMP_Text bill;
    [SerializeField] Button buyBt;
    int price = 0;
    int sum;
    int nb;
    private void OnEnable()
    {
        EventManager.StartListening("ShowItemShop", Show);
        exitBt.onClick.AddListener(Exit);
        //buyBt.onClick.AddListener(Buy);
        //itemData = GameCtrl.Instance.ItemData;
    }

    private void OnDisable()
    {
        EventManager.StopListening("ShowItemShop", Show);
    }


    void Start()
    {
        price = 0;
        if (itemData != null)
        {
            foreach (var dt in itemData.itemInfors)
            {
                
                GameObject temp = Instantiate(itemData.itemUIShop);
                temp.transform.SetParent(content);
                temp.transform.localScale = Vector3.one;
                var ic = temp.GetComponent<ItemOfShop>();
                ic.SetID(dt.id);
                ic.SetCoint(dt.coint);
                ic.SetIcon(dt.icon);
                
            }
        }
    }
    private void Update()
    {
        
        bool isNB = Int32.TryParse(numberItem.text, out nb);
        if (nb > 0 && isNB && GameManager.Instance.GetCoint() >= sum)
        {
            buyBt.interactable = true;
        }
        else
        {
            buyBt.interactable = false;
        }
        sum = CalculateCoint(price, nb);
        bill.text = sum + "";
    }
    private void Show()
    {
        id = EventManager.GetInt("ShowItemShop");
        ItemInfor ii = GetItemData(id);
        icon.sprite = ii.icon;
        nametxt.text = ii.name;
        cointTxt.text = ii.coint + "";
        price = ii.coint;

    }
    int CalculateCoint(int price,int nb)
    {
        return price * nb;
    }
    private void Exit()
    {
        gameObject.SetActive(false);
    }


    public void Buy()
    {
        if (GetItemData(id) != null)
        {
            PlayerData.AddItem(GetItemData(id), nb);
            GameManager.Instance.UpdateTempItem();
            GameManager.Instance.RemoveCoint(sum);
            // gui event den BuyScene (Scene BuyScene)
            EventManager.EmitEvent("Buy");
        }
    }
    ItemInfor GetItemData(int id)
    {
        ItemInfor ii = null;
        foreach(var i in itemData.itemInfors)
        {
            if (i.id == id)
                ii = i;
        }
        return ii;
    }
}
