using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class FixBow : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.EmitEventData("FixBow", 1);
    }
}
