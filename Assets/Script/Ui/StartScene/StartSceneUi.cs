using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUi : MyMonoBehaviour
{
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject screenUI;
    [SerializeField] Button playBt;
    [SerializeField] Button exitBt;
    [SerializeField] Button continueBt;
    [SerializeField] GameObject content;
    [SerializeField] GameObject levelBt;
    [SerializeField] GameObject tutarial;
    [SerializeField] int numberLevel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMainUI();
        LoadScreenUI();
        LoadPlayButton();
        LoadExitButton();
        LoadContent();
    }
    private void Awake()
    {
        playBt.onClick.AddListener(OnScreenUI);
        exitBt.onClick.AddListener(ExitGame);
        continueBt.onClick.AddListener(Continue);
    }
    protected override void OnEnable()
    {
        if(GameManager.Instance.isContinue)
        {
            continueBt.interactable = true;
        }
        else
        {
            continueBt.interactable = false;
        }
        base.OnEnable();
        SetInteractableBT();
    }
    protected override void Start()
    {
        numberLevel = GameManager.Instance.levelMax;
        InstantiateButtonLevels();
        base.Start();
        mainUI.SetActive(true);
        screenUI.SetActive(false);
    }

    private void InstantiateButtonLevels()
    {
        for(int i = 0; i < numberLevel; i++)
        {
            var lv = Instantiate(levelBt);
            lv.GetComponent<LevelBt>().SetLevelNumber(i + 1);
            lv.transform.SetParent(content.transform);
        }
        SetInteractableBT();
        
    }
    void SetInteractableBT()
    {
        foreach(Transform lv in content.transform)
        {
            var lb = lv.gameObject.GetComponent<LevelBt>();
            if (lb.GetLevelNumber() <= GameManager.Instance.GetLevelPass())
            {
                lb.SetInteractableBt(true);
            }
            else
            {
                lb.SetInteractableBt(false);
            }
        }
    }

    void OnScreenUI()
    {
        mainUI.SetActive(false);
        screenUI.SetActive(true);
        GameManager.Instance.ClearItems();
    }
    void ExitGame()
    {
        Debug.Log("Exit");
    }

    void Continue()
    {
        SceneCtrl.Instance.sceneChoosed = GameManager.Instance.GetLevelContinue();
        SceneCtrl.Instance.LoadBuyScene();
    }
    void LoadMainUI()
    {
        mainUI = transform.Find("MainUI").gameObject;
    }
    void LoadScreenUI()
    {
        screenUI = transform.Find("ScreenUI").gameObject;
    }
    void LoadPlayButton()
    {
        playBt = mainUI.transform.Find("PlayBt").GetComponent<Button>();
    }
    void LoadExitButton()
    {
        exitBt = mainUI.transform.Find("ExitBt").GetComponent<Button>();
    }
    void LoadContent()
    {
        content = GameObject.Find("Content");
    }
    public void ExitTutorial()
    {
        tutarial.SetActive(false);
    }
}
