using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArowCotroller : MonoBehaviour
{
    [SerializeField] Rigidbody rig;
    [SerializeField] BoxCollider box;
    [SerializeField] float speed;
    [SerializeField] protected DamageSender damageSender;
    [SerializeField] protected ArowDespawn despawn;
    [SerializeField] protected GameObject vfx;
    [SerializeField] protected GameObject trail;

    [SerializeField] protected AudioClip soundClip;
    [SerializeField] protected AudioSource source;
    bool isRot;
    public bool isFire;
    bool istarget;

    public Transform target;
    public Quaternion rot;
    protected Transform muctieu;
    private void OnEnable()
    {
        istarget = true;
    }
    private void OnDisable()
    {
        ResetValue();
    }
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
        
        //if (isRot)
        //{
        //    transform.rotation = Quaternion.LookRotation(rig.velocity);
        //}
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isFire)
        {
            Ban();
        }
        else if(istarget)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        
    }

    public void Ban()
    {
        box.enabled = true;
        trail.SetActive(true);
        istarget = false;
        rig.AddForce(transform.forward * speed, ForceMode.Impulse);
        isRot = true;
        isFire = false;
        despawn.OnDespawn();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Arow" || other.gameObject.tag == "Surrounded")
            return;

        damageSender.Send(other.transform, transform);
        muctieu = other.transform;
        ArowEffectAfterTrigger();
        despawn.SetCurrentTime();
        box.enabled = false;
        rig.isKinematic = true;
        isRot = false;
    }
    protected virtual void ResetValue()
    {
        rig.isKinematic = false;
        if (vfx != null)
        {
            vfx.SetActive(false);
        }
        muctieu = null;
        trail.SetActive(false);
        box.enabled = false;
    }
    protected virtual void ArowEffectAfterTrigger()
    {
        if (vfx != null)
        {
            vfx.SetActive(true);
        }
        source.PlayOneShot(soundClip);
    }
}
