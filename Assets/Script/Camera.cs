using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameObject PlayerObj;
    Vector3 camPos;
    UnityEngine.Camera cam;
    //public Transform  gunhoie;
    public GameObject GunHole;       
    public float rotSpeed = 5.0f;
    public float Distans = 0.0f;
    public float Hight = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerObj = Shared.BattelMgr.PLAYER.gameObject;
        cam = UnityEngine.Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ShakeOn();
        movePOs();
        camRot();
    }
    void camRot() 
    {
        Vector3 rot1 = cam.ScreenToWorldPoint(Input.mousePosition);
        //Quaternion rot2 = Quaternion.LookRotation(gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Euler(rot1);
        //Vector3 dis = (rot - transform.position);
    }
    void movePOs() 
    {
        if (PlayerObj == null) return;
        transform.position = PlayerObj.transform.position + new Vector3(0, Hight, Distans);
    }

    void ShakeOn() 
    {
        if (Input.GetMouseButton(0)) 
        {
            Shared.MainCamera.Shake(0);
        }
    }
}
