using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : Sqawner
{
    private static BallSpawner instance;
    public static BallSpawner Instance => instance;

    public GameObject ball;
    private void Awake()
    {
        instance = this;
    }

}
