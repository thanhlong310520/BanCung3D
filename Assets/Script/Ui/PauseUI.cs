using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button exit;
    [SerializeField] Button home;
    [SerializeField] Button replay;
    [SerializeField] Button bag;
    private void OnEnable()
    {
        exit.onClick.AddListener(ExitUi);
        home.onClick.AddListener(Home);
        replay.onClick.AddListener(Replay);
        bag.onClick.AddListener(ShowBag);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ExitUi()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
        GameCtrl.Instance.SetIsPause(false);
    }
    private void ShowBag()
    {
        GameCtrl.Instance.BagUI.gameObject.SetActive(true);
    }

    private void Home()
    {
        SceneCtrl.Instance.LoadStartScene();
        GameManager.Instance.Replay();
    }
    void Replay()
    {
        SceneCtrl.Instance.Loadding();
        GameManager.Instance.Replay();
    }

}
