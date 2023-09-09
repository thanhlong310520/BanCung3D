using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherItemsSqawner : Sqawner
{
    private static OtherItemsSqawner instance;
    public static OtherItemsSqawner Instance => instance;
    private void Awake()
    {
        instance = this;
    }
}
