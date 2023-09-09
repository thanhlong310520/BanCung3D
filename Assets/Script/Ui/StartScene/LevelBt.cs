using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelBt : MyMonoBehaviour
{
    [SerializeField] Button bt;
    [SerializeField] TMP_Text txt;
    [SerializeField] int level;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
    }
    private void Awake()
    {
        bt.onClick.AddListener(LoadScene);
    }
    public void SetLevelNumber(int nb)
    {
        level = nb;
        txt.text = nb + "";
    }
    public int GetLevelNumber()
    {
        return Int32.Parse(txt.text);
    }
    public void SetInteractableBt(bool set)
    {
        bt.interactable = set;
    }
    void LoadScene()
    {
        SceneCtrl.Instance.sceneChoosed = level;
        SceneCtrl.Instance.LoadBuyScene();
    }


    private void LoadButton()
    {
        bt = transform.Find("Button").GetComponent<Button>();
    }

    
}
