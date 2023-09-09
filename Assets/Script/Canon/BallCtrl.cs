using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl : MonoBehaviour
{
    [SerializeField] Rigidbody rig;
    [SerializeField] SphereCollider sphereCollier;
    [SerializeField] GameObject vfx;
    public Transform target;
    public DamageSender damageSender;
    public bool isFire;
    public float radius;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip audioClip;
 
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        ResetVal();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            AddF();
        }
    }
    public void AddF()
    {
        Vector3 force = target.position - transform.position + Vector3.up * 5;
        rig.AddForce(force, ForceMode.Impulse);
        isFire = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Surrounded"))
            return;
        Explode();
        sphereCollier.enabled = false;
        rig.isKinematic = true;
        vfx.SetActive(true);
        source.PlayOneShot(audioClip);
    }
    private void ResetVal()
    {
        rig.velocity = Vector3.zero;
        rig.isKinematic = false;
        sphereCollier.enabled = true;
        vfx.SetActive(false);
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var cl in colliders)
        {
            damageSender.Send(cl.transform, transform);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
