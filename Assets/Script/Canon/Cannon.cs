using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    public Transform target;
    public float range = 15f;
    public LayerMask isLayerMask;

    public Transform partToRotate;
    public Transform shooting;
    public float fireRate;
    public float fireCountDown;

    public Transform ui;
    public DamageRecceiver damageRecceiver;
    public Image healUI;
    public GameObject vfx;
    public AudioClip audioClip;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        healUI.fillAmount = 1;
        vfx.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!damageRecceiver.GetIsDead())
        {
            ui.LookAt(Camera.main.transform);
            FindPlayer();
            RotateWithTarget();
            if (target != null)
            {
                Shooting();
            }
            healUI.fillAmount = (float)damageRecceiver.GetHp() / damageRecceiver.GetHpMax();
        }
        else
        {
            Dead();
        }
    }
    void FindPlayer()
    {
        
        Collider[] colliders = null;
        colliders =  Physics.OverlapSphere(transform.position, range, isLayerMask);
        if (colliders.Length > 0)
        {
            foreach (var cl in colliders)
            {
                target = cl.transform;
            }
        }
        else
            target = null;
            
    }
    void RotateWithTarget()
    {
        if (target == null) return;
        Vector3 dir = transform.position - target.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        Vector3 rot = lookRot.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rot.y, 0f);
    }
    void Shooting()
    {
        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }
    void Shoot()
    {
        Quaternion rot = shooting.rotation;
        rot.z *= -1;
        Transform ballClone = BallSpawner.Instance.Sqawn(BallSpawner.Instance.ball.transform, shooting.position, rot);
        BallCtrl bc = ballClone.GetComponent<BallCtrl>();
        if(bc!= null)
        {
            bc.target = target;
            bc.isFire = true;
        }
    }
    void Dead()
    {
        vfx.SetActive(true);
        source.PlayOneShot(audioClip);
        ui.gameObject.SetActive(false);
        StartCoroutine(SetActiveGO());
    }

    IEnumerator SetActiveGO()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
