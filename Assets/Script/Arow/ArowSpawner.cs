using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArowSpawner : Sqawner
{
    private static ArowSpawner instance;
    public static ArowSpawner Instance { get => instance; private set { instance = value; } }
    public Transform arow1;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("da co singleton");
        }
        instance = this;
    }
}
