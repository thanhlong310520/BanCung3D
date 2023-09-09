using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Camera mainCam;
    [SerializeField] float speed;
    [SerializeField] float speedZoom;

    Vector3 diff;
    Vector2 turn;
    // Start is called before the first frame update
    void Start()
    {
        diff = target.transform.position -transform.position;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!GameCtrl.Instance.IsPause)
        {
            FollowTarget();
            RotateCamForX();
            CamZoom();
        }
    }
    void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, 3f);
        transform.rotation = target.rotation;
    }
    void RotateCamForX()
    {
        turn.y += Input.GetAxis("Mouse Y");
        mainCam.transform.localRotation = Quaternion.Euler(-turn.y, mainCam.transform.localRotation.y, 0);
    }
    void CamZoom()
    {
        if (Input.GetMouseButton(0))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, 20, speedZoom);
        }
        else
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, 60, speedZoom);
        }
    }
    
    
    
}
