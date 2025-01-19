using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MoveCamera : MonoBehaviour
{
    GameObject PlayerObj;
    Vector3 camPos;
    UnityEngine.Camera cam;
    public Animation Shake;
    public Animator camAnim;


    public float Distans = 0.0f;
    public float Hight = 0.0f;
    public float aim = 0.0f;

    public float rotSensitive = 10.0f;
    public float limitRot = 55.0f;
    public float rotTime = 1.5f;

    public bool camRotOn = false;

    float xValue = 0;
    float yValue = 0;

    public bool attackModeOn = false;
    public void camRot()
    {
        if (PlayerObj == null || 
            camRotOn == false || 
            attackModeOn == true) return;

        float xRot = Input.GetAxis("Mouse X") * rotSensitive;
        float yRot = Input.GetAxis("Mouse Y") * rotSensitive;

        //xValue = xValue + xRot;
        //yValue = yValue + yRot * (-1.0f);
        xValue += xRot;
        yValue -= yRot;

        yValue = Mathf.Clamp(yValue, -limitRot, limitRot);
        Vector3 distans = new Vector3(0, 0, Distans);
        //transform.rotation = Quaternion.Euler(yValue, xValue, 0);
        Quaternion rotation = Quaternion.Euler(yValue, xValue, 0);
        transform.localPosition = PlayerObj.transform.position + rotation * distans;

        transform.LookAt(PlayerObj.transform.position);
        //Cursor.lockState = CursorLockMode.None;
        //return transform.rotation;
    }
    private void shootMode()
    {
        if (PlayerObj == null) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attackModeOn = !attackModeOn;
        }
    }
    private void shootCamera()
    {
        if (attackModeOn == true)
        {
            gameObject.transform.localRotation = new Quaternion();
            gameObject.transform.localPosition = PlayerObj.transform.position + camPos;
        }
        else { return; }
    }
    void Start()
    {
        camPos = new Vector3(aim, Hight, Distans);
        camAnim = GetComponentInParent<Animator>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        PlayerObj = Shared.BattelMgr.PLAYER.gameObject;
    }

    
    private void LateUpdate()
    {
        showCursue();
        shootMode();
        shootCamera();
        camRot();
    }
    public void cameraShakeAnim(bool _anim) 
    {
        string text = $"{CameraAnim.Shake}";
        if (_anim) 
        {
            camAnim.SetInteger(text, 1);
        }
        else 
        {
            camAnim.SetInteger(text, 0);
        }
    }
    void showCursue()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.visible = true;
        }
    }
    //poton transform

}
