using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance => instance;
    [SerializeField] AudioSource source;
    private void Awake()
    {
        if (instance != null) return;
        instance = this;
        DontDestroyOnLoad(instance);
    }
    public void PlayAudio(AudioClip audio)
    {
        source.Stop();
        source.clip = audio;
        source.Play();
    }

}
