using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] PlayerCtrl playerCtrl;
    [System.Serializable] // tuan tu hoa
    public class CharacterSetting
    {
        public string forward = "Forward";
        public string strafe = "Strafe";
        public string sprint = "Sprint";
    }
    [SerializeField]
    CharacterSetting setting;
    [SerializeField] Transform dayCung;
    [SerializeField] Transform arowPos;
    [SerializeField] Camera mainCam;
    [SerializeField] Transform crossfire;
    [SerializeField] float timeAim = 0.3f;
    float currentAim;

    float forward;
    float strafe;
    bool isSprint;
    Vector2 turn;
    public float speedWalk;
    public float scaleSpeedWalk;
    float _scaleSW;
    public float speedRotate = 5f;
    Vector3 startPosOfDayCung;


    bool isPull;
    bool isGetArow;
    bool isSetCross;
    public GameObject arow;

    ArowCotroller ac;
    int arowChoose = 1;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip bowAudio;
     // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        currentAim = timeAim;
        startPosOfDayCung = dayCung.localPosition;
        isGetArow = true;
        isSetCross = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameCtrl.Instance.IsPause)
        {
            forward = InputManager.instance.forward;
            strafe = InputManager.instance.strafe;
            isSprint = InputManager.instance.isSprint;
            if (isSprint)
            {
                _scaleSW = scaleSpeedWalk;
            }
            else
            {
                _scaleSW = 1f;
            }
            CharacterMove(strafe, forward);
            CharacterSprint(isSprint);
            CharacterRotate();
            CharacterAim();
            KeoDay();
            if (isSetCross)
            {
                SetPosForCrossFile();
            }
        }
    }
    private void FixedUpdate()
    {
        if (forward != 0)
        {
            transform.position += transform.forward * forward * speedWalk * Time.fixedDeltaTime * _scaleSW;
        }
        if(strafe != 0)
        {
            transform.position += transform.right * strafe * speedWalk * Time.fixedDeltaTime * _scaleSW;
        }
    }
    void SetPosForCrossFile()
    {
        crossfire.position = mainCam.transform.forward * 1000;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(mainCam.transform.position, mainCam.transform.forward, 100);
        if (Physics.RaycastAll(mainCam.transform.position, mainCam.transform.forward, 100)!= null)
        {
            foreach(var hit in hits)
            {
                if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Arow") && !hit.collider.CompareTag("Surrounded"))
                {
                    crossfire.position = hit.point;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            GameManager.Instance.IsWinCollider();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ocean")
        {
            playerCtrl.PlayerDamageRecciever.Deduct(playerCtrl.PlayerDamageRecciever.GetHp(),transform);
        }
    }

    public void CharacterMove(float strafe, float forward)
    {
        anim.SetFloat(setting.strafe, strafe);
        anim.SetFloat(setting.forward, forward);
    }
    public void CharacterSprint(bool isSprinting)
    {
        anim.SetBool(setting.sprint, isSprinting);
    }
    public void CharacterRotate()
    {
        turn.x += Input.GetAxis("Mouse X")*speedRotate;
        turn.y += Input.GetAxis("Mouse Y")*speedRotate;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }
    public void CharacterAim()
    {

        var aim = Input.GetMouseButton(0);
        if (aim)
        {
            currentAim -= Time.deltaTime;
            anim.SetBool("Aim", aim);
            if(currentAim < 0 && isGetArow)
            {
                GetArow();
            }
            if (currentAim < -0.1)
            {
                KeoDayCung();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSetCross = false;
            if( currentAim> 0)
            {
                isSetCross = true;
            }
            else
            {
                anim.SetTrigger("Fire");
                Ban();
                EndKeoDayCung();
            }
            currentAim = timeAim;
            anim.SetBool("Aim", aim);
        }
        
        
    }
    public void KeoDayCung()
    {
        isPull = true;
    }
    public void EndKeoDayCung()
    {
        isPull = false;
        dayCung.localPosition = startPosOfDayCung;
    }
    public void KeoDay()
    {
        if (isPull)
        {
            Vector3 newPos = arowPos.position;
            dayCung.position = newPos;
        }
    }

    public void Ban()
    {
        Vector3 dir = crossfire.position-arowPos.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        if (ac != null)
        {
            ac.rot = rot;
            ac.transform.rotation = rot;
            ac.transform.position = arowPos.position;
            ac.isFire = true;
            ac = null;
            foreach (var i in playerCtrl.PlayerItems.GetItemOwners())
            {
                if (i.itemDt.id == arowChoose)
                {
                    i.soluong--;
                }
            }
        }
        source.PlayOneShot(bowAudio);
        isGetArow = true;
        isSetCross = true;
        playerCtrl.BowCtrl.Deduct(1, transform);
    }
    public void GetArow()
    {
        var arSpawn = SetArow();
        if(arSpawn != null)
        {
            var arow = ArowSpawner.Instance.Sqawn(arSpawn.transform, arowPos.position, Quaternion.identity);
            ac = arow.GetComponent<ArowCotroller>();
            ac.target = arowPos;
            isGetArow = false;
        }
    }

    GameObject SetArow()
    {
        arowChoose = InputManager.instance.arowChoose;
        List<ItemOwner> list = playerCtrl.PlayerItems.GetItemOwners();
        foreach (var it in list)
        {
            if (it.itemDt.id == arowChoose)
            {
                if (it.soluong > 0)
                {
                    return it.itemDt.gameObjectPrefab;
                }
                
            }
        }
        arowChoose = FindArow(list);
        InputManager.instance.arowChoose = arowChoose;
        foreach (var i in list)
        {
            if (i.itemDt.id == arowChoose)
            {
                return i.itemDt.gameObjectPrefab;
            }
        }
        return null;
    }
    int FindArow(List<ItemOwner> list)
    {
        foreach(var it in list)
        {
            if(it.itemDt.type == Type.arow)
            {
                if(it.soluong > 0)
                {
                    return it.itemDt.id;
                }
            }
        }
        return 0;
    }
    public int GetArowChoose()
    {
        return arowChoose;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(mainCam.transform.position, mainCam.transform.forward * 100);
        Gizmos.DrawLine(mainCam.transform.position, crossfire.position);
    }
}
