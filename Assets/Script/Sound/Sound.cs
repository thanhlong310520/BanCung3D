using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MyMonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip footstep;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAudioSource();
    }

    private void LoadAudioSource()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(InputManager.instance.strafe != 0 || InputManager.instance.forward != 0)
        {
            source.PlayOneShot(footstep);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!InputManager.instance.isSprint)
        {
            source.Stop();
        }
    }
}
