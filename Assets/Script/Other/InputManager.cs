using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public float forward;
    public float strafe;
    public bool isSprint;
    bool isGamePause;
    public int arowChoose = 1;
    private void Awake()
    {
        instance = this;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        isGamePause = GameCtrl.Instance.IsPause;
        if (!isGamePause)
        {
            strafe = Input.GetAxisRaw("Horizontal");
            forward = Input.GetAxisRaw("Vertical");
            isSprint = Input.GetKey(KeyCode.LeftShift);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arowChoose = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            arowChoose = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            arowChoose = 3;
        }

    }
    public bool IsUseHeal()
    {
        if (!isGamePause && Input.GetKeyDown(KeyCode.Alpha5))
            return true;
        return false;
    }
    public bool IsUseFixBow()
    {
        if (!isGamePause && Input.GetKeyDown(KeyCode.Alpha6))
            return true;
        return false;
    }
    public bool IsPause()
    {
        if (!isGamePause && Input.GetKeyDown(KeyCode.Escape))
        {
            return true;
        }
        return false;
    }
}
