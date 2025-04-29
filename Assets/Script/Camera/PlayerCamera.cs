using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UIWidget;
using static UnityEngine.Rendering.PostProcessing.PostProcessResources;

public partial class PlayerCamera : CameraBase
{
    //Player PlayerObj;
    Transform viewObj;
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
    public Queue<Vector2> MouseMoveQueBase => Shared.InputManager.MouseMoveQueBase;
    Camera thisCamera;
    public float minFOV = 40f;
    public float maxFOV = 80f;
    float zoomSpeed = 2.5f;

    ShakeCamera shakeCamera;
    Transform shakeTrs;
    Transform cameraPivot;
    ShakeState shakeState = ShakeState.Shake_Off;
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
    //public void viewObjInit(GameObject _obj) 
    //{
    //    viewObj = _obj;
    //}
    public void CameraModeInit(PlayerCameraMode _mode)
    {
        playerCameraMode = _mode;
    }
    public void camRot(Vector2 _pos)//수정 필요
    {
        float xRot = _pos.x * rotSensitive;
        float yRot = _pos.y * rotSensitive;

        xValue += xRot;
        yValue -= yRot;

        yValue = Mathf.Clamp(yValue, -limitRot, limitRot);

        Quaternion rotation = Quaternion.Euler(yValue, xValue, 0);
        viewObj.rotation = rotation;

        Vector3 distans = new Vector3(0, 0, -Distans);
        shakeTrs.localPosition = Vector3.zero;

        transform.position = viewObj.position + rotation * distans;
        transform.LookAt(viewObj);

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
        viewObj = transform.parent.parent;
        shakeTrs = transform.parent;
        camAnim = GetComponentInParent<Animator>();
        thisCamera = GetComponent<Camera>();
        shakeCamera = GetComponent<ShakeCamera>();
    }
    public void MainCamerainitEvent()
    {
        while (MouseScrollQueBase.Count > 0)//key 
        {
            float type = MouseScrollQueBase.Dequeue();
            thisCamera.fieldOfView = Mathf.Clamp(thisCamera.fieldOfView, minFOV, maxFOV);
            thisCamera.fieldOfView -= type * zoomSpeed;
        }
        while (MouseMoveQueBase.Count > 0)//key 
        {
            Vector2 type = MouseMoveQueBase.Dequeue();
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
        //camRot(Vector3.zero);
    }
    public void CameraShakeAnimation(int _value) 
    {
        if (_value == 1) 
        {
            if (shakeState != ShakeState.Shake_On)
            {
                //shakeState = ShakeState.Shake_On;
                shakeCamera.Shake(0, 2);
                //Invoke("shakeState", 3);
            }
            else { return; }
            //camAnim.SetInteger("Shake", 1);
        }
        else 
        {
            //camAnim.SetInteger("Shake", 0);
        }
    }
    private void ShakeOff() 
    {
        shakeState = ShakeState.Shake_Off;
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
