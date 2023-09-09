using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public int countHealer;
    public float lifeTime;
    public GameObject vfx;
    public AudioClip audioClip;
    public AudioSource source;

    private void OnEnable()
    {
        EventManager.EmitEventData("Heal", countHealer);
        vfx.SetActive(true);
        source.PlayOneShot(audioClip);
        StartCoroutine(Despawn());
    }
    private void Update()
    {
        transform.position = GameCtrl.Instance.PlayerCtrl.transform.position;
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(lifeTime);
        vfx.SetActive(false);
        OtherItemsSqawner.Instance.Despawn(transform);
    }
}
