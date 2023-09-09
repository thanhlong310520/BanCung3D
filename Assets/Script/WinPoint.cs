using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    [SerializeField] BoxCollider box;
    [SerializeField] GameObject vfx;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip audio;
    bool isShow;

    // Start is called before the first frame update
    void Start()
    {
        SetUp(false);
        isShow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetIsClearEnemy())
        {
            if (!isShow)
            {
                SetUp(true);
                source.PlayOneShot(audio);
                isShow = true;
            }
        }
    }
    void SetUp(bool set)
    {
        box.enabled = set;
        vfx.SetActive(set);
    }
}
