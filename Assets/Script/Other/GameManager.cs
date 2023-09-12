using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int levelMaxPassed;
    int levelContinue;
    public bool isContinue;
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] LevelData levelData;
    int coint;
    bool isClearEnemy;
    bool isWin;
    public List<ItemOwner> beginItems;
    public List<ItemOwner> testList;
    public int levelMax = 5; 
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        DontDestroyOnLoad(instance);
        
    }
    private void OnEnable()
    {
        levelMaxPassed = PlayerPrefs.GetInt("LevelPassed");
        if(levelMaxPassed < 1)
        {
            levelMaxPassed = 1;
        }
        levelContinue = PlayerPrefs.GetInt("LevelContinue");
        CheckOnBtContinue();
        
    }
    private void Start()
    {
        coint = 0;
        beginItems = PlayerData.GetData();
        UpdateTempItem();
    }
    public void Update()
    {
        if (isClearEnemy && isWin)
        {
            Win();
        }
    }
    public void PlayerDead()
    {
        Time.timeScale = 0f;
        GameCtrl.Instance.SetIsPause(true);
        Cursor.lockState = CursorLockMode.Confined;
        // gui event den MainUI(Scene Main)
        EventManager.EmitEvent("GameOver");
    }

    public void Win()
    {
        Time.timeScale = 0f;
        GameCtrl.Instance.SetIsPause(true);
        Cursor.lockState = CursorLockMode.Confined;
        // gui event den MainUI(Scene Main)
        EventManager.EmitEvent("Win");
    }
    public void Replay()
    {
        UpdateTempItem();
        Time.timeScale = 1f;
        GameCtrl.Instance.SetIsPause(false);
    }
    public void ResetGame()
    {
        isWin = false;
        isClearEnemy = false;
        Replay();
    }
    public void IsClearEnemy()
    {
        isClearEnemy = true;
    }
    public bool GetIsClearEnemy()
    {
        return isClearEnemy;
    }
    public void IsWinCollider()
    {
        isWin = true;
    }
    public void UpdateTempItem()
    {
        testList.Clear();
        foreach (var i in beginItems)
        {
            ItemOwner temp = new ItemOwner(i.itemDt, i.soluong);
            testList.Add(temp);
        }
    }
    public void UpdateTempItem(ItemInfor item, int sl)
    {
        bool isHas = false;
        
        foreach(var i in testList)
        {
            if(i.itemDt.id == item.id)
            {
                isHas = true;
                i.soluong += sl;
            }
        }

        if (!isHas)
        {
            testList.Add(new ItemOwner(item, sl));
        }
    }
    public void AddCoint(int number)
    {
        coint = levelData.listMoneyLevel[number];
        Debug.Log(coint);
    }
    public void RemoveCoint(int detruc)
    {
        coint -= detruc;
    }
    public int GetCoint()
    {
        return coint;
    }
    public void SaveInformation()
    {
        List<ItemOwner> list = GameCtrl.Instance.PlayerCtrl.PlayerItems.GetItemOwners();
        PlayerData.SaveOwner(list);
    }
    public void ClearItems()
    {
        PlayerData.ClearData();
    }

    public void SaveLevelPass()
    {
        SetLevelContinue();
        if(levelMaxPassed < SceneCtrl.Instance.sceneChoosed)
        {
            PlayerPrefs.SetInt("LevelPassed", SceneCtrl.Instance.sceneChoosed);
            levelContinue = SceneCtrl.Instance.sceneChoosed;
        }
        Debug.Log(SceneCtrl.Instance.sceneChoosed);
    }
    public int GetLevelPass()
    {
        return levelMaxPassed;
    }
    public int GetLevelContinue()
    {
        return levelContinue;
    }
    public void SetLevelContinue()
    {
        levelContinue = SceneCtrl.Instance.sceneChoosed;
        Debug.Log(levelContinue);
        PlayerPrefs.SetInt("LevelContinue", levelContinue);
        CheckOnBtContinue();
    }
    void CheckOnBtContinue()
    {
        if (levelContinue < 1)
        {
            isContinue = false;
        }
        else
        {
            isContinue = true;
        }
    }
}
