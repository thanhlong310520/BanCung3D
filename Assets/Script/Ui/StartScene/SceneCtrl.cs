using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    private static SceneCtrl instance;
    public static SceneCtrl Instance => instance;

    public int sceneChoosed;
    public AudioClip startAudio;
    public AudioClip gameAudio;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
        DontDestroyOnLoad(instance);
    }
    public void Loadding()
    {
        SceneManager.LoadSceneAsync("Main");
        SceneManager.LoadScene("Evironment",LoadSceneMode.Additive);
        SceneManager.LoadScene($"Level{sceneChoosed}",LoadSceneMode.Additive);
        SoundManager.Instance.PlayAudio(gameAudio);
    }
    public void LoadBuyScene()
    {
        GameManager.Instance.AddCoint(sceneChoosed);
        GameManager.Instance.ClearItems();
        SceneManager.LoadScene(1);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        SoundManager.Instance.PlayAudio(startAudio);
    }
}
