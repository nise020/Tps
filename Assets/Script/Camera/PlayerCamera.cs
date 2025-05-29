using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public partial class PlayerCamera : CameraBase
{
    //Player PlayerObj;
    Transform viewObj;
    Vector3 camPos;
    Quaternion aimRot;
    public Animation Shake;
    public Animator camAnim;
    PlayerType playerType = PlayerType.None;

    public float Distans = 0.0f;
    public float Hight = 0.0f;
    public float aim = 0.0f;

    public float QxValu;
    public float QyValu;

    public float rotSensitive = 10.0f;
    public float limitRot = 40.0f;
    public float attacklimitRot = 40.0f;
    public float rotTime = 1.5f;

    [SerializeField] bool camRotOn = false;
    [SerializeField] bool GunModeOn = false;

    float xValue = 0;
    float yValue = 0;
    [SerializeField] PlayerCameraMode playerCameraMode = PlayerCameraMode.CameraRotationMode;
    public Queue<float> MouseScrollQueBase => Shared.InputManager.MouseScrollQueData;
    public Queue<Vector2> MouseMoveQueBase => Shared.InputManager.MouseMoveQueData;
    Camera thisCamera;
    public float minFOV = 40f;
    public float maxFOV = 80f;
    float zoomSpeed = 2.5f;

    public float minPos = -3f;
    public float maxPos = -7f;


    ShakeCamera shakeCamera;
    Transform shakeTrs;
    Transform cameraPivot;
    ShakeState shakeState = ShakeState.Shake_Off;
    PlayerCameraState state = PlayerCameraState.Rotation_Stop;

    void Start()
    {
        viewObj = transform.parent.parent;
        camAnim = GetComponentInParent<Animator>();
        thisCamera = GetComponent<Camera>();
        shakeCamera = GetComponent<ShakeCamera>();
    }
    private void LateUpdate()
    {
        MainCamerainitEvent();
        //camRot(Vector3.zero);
    }

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
    public void viewObjInit(out PlayerCameraState _state)
    {
        _state = state;
    }
    public void CameraModeInit(PlayerCameraMode _mode)
    {
        playerCameraMode = _mode;
    }
    private void CameraRotion(Vector2 _pos)//수정 필요
    {
        float xRot = _pos.x * rotSensitive;
        float yRot = _pos.y * rotSensitive;

        xValue += xRot;
        yValue -= yRot;

        yValue = Mathf.Clamp(yValue, -limitRot, limitRot);

        shakeTrs = transform.parent;

        Quaternion rotation = Quaternion.Euler(yValue, xValue, 0);
        viewObj.rotation = rotation;//
        //Vector3 distans = rotation * new Vector3(0, 0, -Distans);

        shakeCamera.ShakeMOdeChange(ShakeMode.MoveCamera);
        Vector3 offset = rotation * shakeCamera.ShakePos();


        float shakeLimit = 1.0f;
        offset.x = Mathf.Clamp(offset.x, -shakeLimit, shakeLimit);
        offset.y = Mathf.Clamp(offset.y, -0.1f, 0.1f);
        //shakeTrs.localPosition = viewObj.position + offset;
        shakeTrs.localPosition = offset;

        //transform.position = viewObj.position + rotation * distans;
        //transform.LookAt(viewObj);
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
    
    public void MainCamerainitEvent()
    {
        while (MouseScrollQueBase.Count > 0)//key 
        {
            float type = MouseScrollQueBase.Dequeue();

            //Vector3 position =  transform.localPosition;

            //position.z += type * zoomSpeed;
            //position.z = Mathf.Clamp(position.z, minPos , maxPos);

            //transform.localPosition = position;


            thisCamera.fieldOfView = Mathf.Clamp(thisCamera.fieldOfView, minFOV, maxFOV);
            thisCamera.fieldOfView -= type * zoomSpeed;

        }
        while (MouseMoveQueBase.Count > 0)//key 
        {
            Vector2 type = MouseMoveQueBase.Dequeue();
            if (playerCameraMode == PlayerCameraMode.CameraRotationMode)
            {
                if (state != PlayerCameraState.Rotation_On)
                {
                    state = PlayerCameraState.Rotation_On;
                    CameraRotion(type);
                }
            }
            else if (playerCameraMode == PlayerCameraMode.GunAttackMode)
            {
                shootCamera(type);
            }
        }
        if (MouseMoveQueBase.Count == 0 && 
            state != PlayerCameraState.Rotation_Stop)
        {
            state = PlayerCameraState.Rotation_Stop;
            shakeCamera.ShakeMOdeChange(ShakeMode.StaticCamera);
        }
    }

    
    public void CameraShakeAnimation(int _value) //Attack
    {
        if (_value == 1) 
        {
            if (shakeState != ShakeState.Shake_On)
            {
                //shakeState = ShakeState.Shake_On;
                //shakeCamera.Shake(0, 1);
                //camAnim.SetInteger("Shake", 1);
                //Invoke("shakeState", 3);
            }
            camAnim.SetInteger("Shake", 1);
        }
        else 
        {
            camAnim.SetInteger("Shake", 0);
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
