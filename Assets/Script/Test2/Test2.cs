using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.StartListening("Test2", Test);
    }
    void Test()
    {
        Debug.Log("Test2 ");
        var data = EventManager.GetData("Test2");
        Debug.Log("data " + data);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
