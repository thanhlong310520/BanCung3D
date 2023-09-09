using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCam : MonoBehaviour
{
    Vector2 turn;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * speed;
        turn.y += Input.GetAxis("Mouse Y") * speed;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

    }
}
