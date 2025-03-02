using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MoveCamera : MonoBehaviour
{
    public GameObject PlayerObj;
    GameObject attackAim;
    Vector3 camPos;
    UnityEngine.Camera cam;
    public Animation Shake;
    public Animator camAnim;


    public float Distans = 0.0f;
    public float Hight = 0.0f;
    public float aim = 0.0f;

    public float rotSensitive = 10.0f;
    public float limitRot = 55.0f;
    public float attacklimitRot = 55.0f;
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

        xValue += xRot;
        yValue -= yRot;

        yValue = Mathf.Clamp(yValue, -limitRot, limitRot);
        Vector3 distans = new Vector3(0, 0, Distans);
        //transform.rotation = Quaternion.Euler(yValue, xValue, 0);
        Quaternion rotation = Quaternion.Euler(yValue, xValue, 0);
        transform.localPosition = PlayerObj.transform.position + rotation * distans;

        transform.LookAt(PlayerObj.transform.position);
        PlayerObj.transform.rotation = rotation;
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
            camPos = new Vector3(aim, Hight, Distans);

            float xRot = Input.GetAxis("Mouse X") * rotSensitive;
            float yRot = Input.GetAxis("Mouse Y") * rotSensitive;

            xValue += xRot;
            yValue -= yRot;

            Quaternion rotation = Quaternion.Euler(yValue, xValue, 0);
            yValue = Mathf.Clamp(yValue, -attacklimitRot, attacklimitRot);

            gameObject.transform.localPosition = PlayerObj.transform.position + rotation * camPos;
            gameObject.transform.rotation = rotation;
            PlayerObj.transform.rotation = rotation;

        }
        else { return; }
    }
    void Start()
    {
        camAnim = GetComponentInParent<Animator>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        attackAim = Shared.BattelMgr.CamAim;
        //transform.LookAt(PlayerObj.transform);
    }

    
    private void LateUpdate()
    {
        //showCursue();
        shootMode();
        shootCamera();
        camRot();
    }
    public void cameraShakeAnim(bool _anim) 
    {
        if (_anim) 
        {
            camAnim.SetInteger("Shake", 1);
        }
        else 
        {
            camAnim.SetInteger("Shake", 0);
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
