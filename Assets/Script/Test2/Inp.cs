using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class Inp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.EmitEventData("Test2", 1);
        }
    }
}
