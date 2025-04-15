using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public partial class PlayerCamera : CameraBase
{
    //Player PlayerObj;
    GameObject viewObj;
    Vector3 camPos;
    Quaternion aimRot;
    public Animation Shake;
    public Animator camAnim;
    CharactorJobEnum playerType = CharactorJobEnum.None;

    public float Distans = 0.0f;
    public float Hight = 0.0f;
    public float aim = 0.0f;

    public float QxValu;
    public float QyValu;

    public float rotSensitive = 10.0f;
    public float limitRot = 55.0f;
    public float attacklimitRot = 55.0f;
    public float rotTime = 1.5f;

    [SerializeField] bool camRotOn = false;
    [SerializeField] bool GunModeOn = false;

    float xValue = 0;
    float yValue = 0;
    [SerializeField] PlayerCameraMode playerCameraMode = PlayerCameraMode.CameraRotationMode;
    public Queue<float> MouseScrollQueBase => Shared.InputManager.MouseScrollQueBase;
    public Queue<Vector3> MouseMoveQueBase => Shared.InputManager.MouseMoveQueBase;
    Camera thisCamera;
    public float minFOV = 40f;
    public float maxFOV = 80f;
    float zoomSpeed = 2.5f;
    public bool GunModeCheck() 
    {
        if (GunModeOn) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public void viewObjInit(GameObject _obj) 
    {
        viewObj = _obj;
    }
    public void CameraModeInit(PlayerCameraMode _mode)
    {
        playerCameraMode = _mode;
    }
    public void camRot(Vector3 _pos)//수정 필요
    {
        if (viewObj == null) return;

        //playerType == PlayerType.Gunner

        float xRot = Input.GetAxis("Mouse X") * rotSensitive;
        float yRot = Input.GetAxis("Mouse Y") * rotSensitive;
        //float xRot = _pos.x * rotSensitive;
        //float yRot = _pos.y * rotSensitive;

        xValue += xRot;
        yValue -= yRot;

        yValue = Mathf.Clamp(yValue, -limitRot, limitRot);
        Vector3 distans = new Vector3(0, 0, Distans);
        Quaternion rotation = Quaternion.Euler(yValue, xValue, 0);
        transform.position = viewObj.transform.position + rotation * distans;

        gameObject.transform.localRotation = rotation;
        //Vector3 offset = rotation * new Vector3(0, 0, -Distans);
        //transform.position = viewObj.transform.position + offset;

        // 카메라가 viewObj를 바라보게
        //transform.LookAt(viewObj.transform);
    }
    private void shootCamera(Vector3 _pos)
    {
        if (viewObj == null) return;
        camPos = new Vector3(aim, Hight, Distans);
        float xRot = _pos.x * rotSensitive;
        float yRot = _pos.y * rotSensitive;

        xValue += xRot;
        yValue -= yRot;
        yValue = Mathf.Clamp(yValue, -attacklimitRot, attacklimitRot);

        Quaternion rotation = Quaternion.Euler(yValue, xValue, 0);
        //viewObj.transform.rotation = rotation;
    }
    public void ShootModePosition()
    {
        camPos = new Vector3(aim, Hight, Distans);
        Quaternion targetRotation = Quaternion.LookRotation(camPos.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,Time.deltaTime);
        gameObject.transform.position = camPos;
        //여기 중간에 부드럽게 카메라 위치를 이동시킬 블ㄹ렌드 효과가 필ㄴ요함

        //shootCamera(GunModeOn);
    }
    void Start()
    {
        //PlayerObj = GetComponentInParent<Player>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //transform.LookAt(PlayerObj.transform);
        camAnim = GetComponentInParent<Animator>();
        thisCamera = GetComponent<Camera>();
        //Shared.CameraManager.CameraAdd(this);
    }
    public void MainCamerainitEvent()
    {
        while (MouseScrollQueBase.Count > 0)//key 
        {
            float type = MouseScrollQueBase.Dequeue();
            thisCamera.fieldOfView -= type * zoomSpeed;
            thisCamera.fieldOfView = Mathf.Clamp(thisCamera.fieldOfView, minFOV, maxFOV);
        }
        while (MouseMoveQueBase.Count > 0)//key 
        {
            Vector3 type = MouseMoveQueBase.Dequeue();
            if (playerCameraMode == PlayerCameraMode.CameraRotationMode)
            {
                camRot(type);
            }
            else if (playerCameraMode == PlayerCameraMode.GunAttackMode)
            {
                shootCamera(type);
            }
        }
    }

    private void LateUpdate()
    {
        MainCamerainitEvent();
        
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
