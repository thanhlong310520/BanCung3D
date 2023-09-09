using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : MyMonoBehaviour
{
    private static GameCtrl instance;
    public static GameCtrl Instance => instance;
    bool isPause;
    [SerializeField] PauseUI pauseUI;
    public bool IsPause => isPause;
    [SerializeField] ItemData itemData;
    public ItemData ItemData => itemData;

    [SerializeField] PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;

    [SerializeField] GameObject itemForBagUI;
    public GameObject ItemForBagUI { get => itemForBagUI; set => itemForBagUI = value; }

    [SerializeField] BagUI bagUI;
    public BagUI BagUI { get => bagUI; set => bagUI = value; }

    [SerializeField] ShopUI shopUI;
    public ShopUI ShopUI { get => shopUI; set => shopUI = value; }

    private void Awake()
    {
        instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPauseUi();
        LoadItemData();
        LoadPlayerCtrl();
        LoadItemforBagUI();
        LoadBagUI();
        LoadShopUI();
    }
    protected override void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked;
    }
    protected override void Update()
    {
        base.Update();
        PauseGame();

    }
    void PauseGame()
    {
        if (InputManager.instance.IsPause())
        {
            Cursor.lockState = CursorLockMode.Confined;
            pauseUI.gameObject.SetActive(true);
            SetIsPause(true);
        }
    }
    public void SetIsPause(bool i)
    {
        isPause = i;
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    void LoadPauseUi()
    {
        pauseUI = GameObject.FindAnyObjectByType<PauseUI>();
    }
    void LoadItemData()
    {
        itemData = Resources.Load<ItemData>("Data/items");
    }
    void LoadPlayerCtrl()
    {
        playerCtrl = GameObject.FindAnyObjectByType<PlayerCtrl>();
    }
    void LoadItemforBagUI()
    {
        itemForBagUI = Resources.Load<GameObject>("Prefabs/ItemOfBag");
    }
    void LoadBagUI()
    {
        bagUI = GameObject.FindAnyObjectByType<BagUI>();
    }
    void LoadShopUI()
    {
        shopUI = GameObject.FindAnyObjectByType<ShopUI>();
    }

}
