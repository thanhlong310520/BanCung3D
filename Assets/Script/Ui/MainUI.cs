using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    [SerializeField] Image fillHealth;
    [SerializeField] Image fillBow;
    [SerializeField] GameObject gameOverUi;
    [SerializeField] GameObject winUI;
    [SerializeField] TMP_Text normalArow;
    [SerializeField] TMP_Text boomArow;
    [SerializeField] TMP_Text fireArow;
    [SerializeField] TMP_Text health;
    [SerializeField] TMP_Text fixbow;
    [SerializeField] Image normalFiller;
    [SerializeField] Image boomFiller;
    [SerializeField] Image fireFiller;
    PlayerCtrl playerCtrl;
    int hpH, maxHpH;
    int hpB, maxHpB;
    public List<ItemOwner> listIt;
    private void OnEnable()
    {

        EventManager.StartListening("GameOver",GameOver);
        EventManager.StartListening("Win",Win);
    }
    private void OnDisable()
    {
        EventManager.StopListening("GameOver", GameOver);
        EventManager.StopListening("Win", Win);
    }


    private void Start()
    {
        playerCtrl = GameCtrl.Instance.PlayerCtrl;
    }
    private void Update()
    {
        listIt = playerCtrl.PlayerItems.GetItemOwners();
        hpH = playerCtrl.PlayerDamageRecciever.GetHp();
        maxHpH = playerCtrl.PlayerDamageRecciever.GetHpMax();
        hpB = playerCtrl.BowCtrl.GetHp();
        maxHpB = playerCtrl.BowCtrl.GetHpMax();
        SetFillBow(hpB, maxHpB);
        SetFillHealth(hpH, maxHpH);
        SetTextNumber();
        SetFillerArow();
    }

    public void SetFillHealth(int hp, int maxHp)
    {
        fillHealth.fillAmount = (float)hp / maxHp;
    }
    public void SetFillBow(int hp, int maxHp)
    {
        fillBow.fillAmount = (float)hp / maxHp;
    }
    private void GameOver()
    {
        gameOverUi.SetActive(true);
    }
    void Win()
    {
        winUI.SetActive(true);
    }

    void SetTextNumber()
    {
        foreach(var i in listIt)
        {
            switch (i.itemDt.id)
            {
                case 1:
                    {
                        normalArow.text = i.soluong + "";
                        break;
                    }
                case 2:
                    {
                        boomArow.text = i.soluong + "";
                        break;
                    }
                case 3:
                    {
                        fireArow.text = i.soluong + "";
                        break;
                    }
                case 4:
                    {
                        health.text = i.soluong + "";
                        break;
                    }
                case 5:
                    {
                        fixbow.text = i.soluong + "";
                        break;
                    }

            }
        }
    }
    void SetFillerArow()
    {
        int arowChoose = InputManager.instance.arowChoose;
        switch (arowChoose)
        {
            case 1:
                {
                    normalFiller.gameObject.SetActive(false);
                    boomFiller.gameObject.SetActive(true);
                    fireFiller.gameObject.SetActive(true);
                    break;
                }
            case 2:
                {
                    normalFiller.gameObject.SetActive(true);
                    boomFiller.gameObject.SetActive(false);
                    fireFiller.gameObject.SetActive(true);
                    break;
                }
            case 3:
                {
                    normalFiller.gameObject.SetActive(true);
                    boomFiller.gameObject.SetActive(true);
                    fireFiller.gameObject.SetActive(false);
                    break;
                }
            default:
                {
                    normalFiller.gameObject.SetActive(true);
                    boomFiller.gameObject.SetActive(true);
                    fireFiller.gameObject.SetActive(true);
                    break;
                }
        }
    }
    public void Replay()
    {
        GameManager.Instance.Replay();
        SceneCtrl.Instance.Loadding();

    }
    public void ExitGame()
    {
        Debug.Log("Exit");
    }
    public void Home()
    {
        GameManager.Instance.SaveInformation();
        GameManager.Instance.ResetGame();
        SceneCtrl.Instance.LoadStartScene();

    }
    public void Continue()
    {
        GameManager.Instance.SaveInformation();
        GameManager.Instance.ResetGame();
        SceneCtrl.Instance.sceneChoosed++;
        GameManager.Instance.SaveLevelPass();
        SceneCtrl.Instance.LoadBuyScene();
    }
}
