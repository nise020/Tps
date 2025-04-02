using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MoveCamera : MonoBehaviour
{
    Player PlayerObj;
    Vector3 camPos;
    Quaternion aimRot;
    UnityEngine.Camera cam;
    public Animation Shake;
    public Animator camAnim;
    PlayerEnum playerType = PlayerEnum.None;

    public float Distans = 0.0f;
    public float Hight = 0.0f;
    public float aim = 0.0f;

    public float QxValu;
    public float QyValu;

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

        //playerType == PlayerType.Gunner

        float xRot = Input.GetAxis("Mouse X") * rotSensitive;
        float yRot = Input.GetAxis("Mouse Y") * rotSensitive;

        xValue += xRot;
        yValue -= yRot;

        yValue = Mathf.Clamp(yValue, -limitRot, limitRot);
        Vector3 distans = new Vector3(0, 0, Distans);
        Quaternion rotation = Quaternion.Euler(yValue, xValue, 0);
        transform.position = PlayerObj.transform.position + rotation * distans;

        gameObject.transform.rotation = rotation;
    }
    private void shootCamera(bool _value)
    {
        if (_value == true)
        {
            //playerType == PlayerType.Gunner

            camPos = new Vector3(aim, Hight, Distans);
            float xRot = Input.GetAxis("Mouse X") * rotSensitive;
            float yRot = Input.GetAxis("Mouse Y") * rotSensitive;

            xValue += xRot;
            yValue -= yRot;
            yValue = Mathf.Clamp(yValue, -attacklimitRot, attacklimitRot);

            Quaternion rotation = Quaternion.Euler(yValue, xValue, 0);
            PlayerObj.transform.rotation = rotation;
        }
        else { return; }
    }
    private void shootMode()
    {
        if (PlayerObj == null) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attackModeOn = !attackModeOn;
        }

        //여기 중간에 부드럽게 카메라 위치를 이동시킬 블ㄹ렌드 효과가 필ㄴ요함

        shootCamera(attackModeOn);
    }
    void Start()
    {
        PlayerObj = GetComponentInParent<Player>();
        camAnim = GetComponentInParent<Animator>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //transform.LookAt(PlayerObj.transform);
    }

    
    private void LateUpdate()
    {
        //showCursue();

        shootMode();
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
