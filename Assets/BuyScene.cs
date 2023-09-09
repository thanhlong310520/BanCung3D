using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyScene : MyMonoBehaviour
{
    [SerializeField] Button playbt;
    [SerializeField] GameObject hienthi;
    [SerializeField] Transform content;
    [SerializeField] TMP_Text cointNumber;
    [SerializeField] TMP_Text levelNb;
    List<ItemOwner> items;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayButton();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        ShowCointNumber();
        EventManager.StartListening("Buy", ShowItemOwner);
    }
    private void OnDisable()
    {
        EventManager.StartListening("Buy", ShowItemOwner);
    }
    private void Awake()
    {
        playbt.onClick.AddListener(JoinGame);
    }

    public void JoinGame()
    {
        GameManager.Instance.SetLevelContinue();
        SceneCtrl.Instance.Loadding();
    }

    private void LoadPlayButton()
    {
        playbt = GameObject.Find("PlayBT").GetComponent<Button>();
    }

    void ShowItemOwner()
    {
        ShowCointNumber();
        items = GameManager.Instance.testList;
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

    void ShowCointNumber()
    {
        cointNumber.text = GameManager.Instance.GetCoint() + "";
        levelNb.text = SceneCtrl.Instance.sceneChoosed + "";
    }
}
