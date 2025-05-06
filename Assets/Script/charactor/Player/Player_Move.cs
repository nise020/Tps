using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public partial class Player : Charactor
{
    //Input 기능을 사용할 경우 Que를 사용해서 저장 후에 순차적으로
    //처리하게 하지 않으면 입력값이 소실 된다

    //일정 시간이 지나도 가만히(움직이지 않으면) 있으면 안 움직인가는 판단
    //분산시스템
    float runDistanseValue = 15.0f;
    float playerStopDistanseValue = 0.3f;
    
    protected PlayerWalkState playerWalkState = PlayerWalkState.Walk_Off;
    protected PlayerRunState playerRunState = PlayerRunState.Run_Off;
    protected PlayerShitState playerShitState = PlayerShitState.ShitUP;
    protected NpcWalkState npcWalkState = NpcWalkState.Stop;
    protected FindMoveObject objectInfo = FindMoveObject.None;
    protected RunState runState = RunState.Walk;
    //protected AttackState attackState = AttackState.None;

    protected float notWalkTimer = 0;
    protected float notWalkTime = 3.0f;
    protected Vector3 movePosition = new Vector3();
    protected Vector3 targetPos = new Vector3();
    Queue<Vector3> fsmPosQue = new Queue<Vector3>();
    LayerName layerName = LayerName.None;
    public bool PlayerObjectWalkCheck() 
    {
        if (playerWalkState == PlayerWalkState.Walk_On)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public Vector3 MovePointUpdate()
    {
        return movePosition;
    }
   
    //public bool playerwalksateinit()//player state object
    //{
    //    //if (playerWalkState == PlayerWalkState.Walk_Off)
    //    //{
    //    //    return false;
    //    //}
    //    //else
    //    //{
    //    //    return true;
    //    //}
    //}
    
    public void PositionObjectInit(out List<GameObject> _objects)
    {
        _objects = backPositionObject;
    }

   
    protected override void move(CharctorStateEnum _value,Vector3 _pos)//Controll
    {
        if (_value == CharctorStateEnum.Npc)
        {
            return;
        }
        else if (_value == CharctorStateEnum.Player)
        {
            if (playerShitState == PlayerShitState.ShitDown) 
            {
                shitdownCheak();
                playerShitState = PlayerShitState.ShitUP;
            }

            if (_pos.magnitude > 0.1f)
            {
                //playerWalkState = PlayerWalkState.Walk_On;
                notWalkTimer = 0.0f;

                float speed = speedValue;

                if (runState == RunState.Run) 
                {
                    speed = speedValue * 2;
                }

                if (cameraMode == PlayerCameraMode.CameraRotationMode)//nomal
                {
                    Transform camera = viewcam.transform;

                    Vector3 camForward = camera.forward;
                    Vector3 camRight = camera.right;
                    camForward.y = 0;
                    camRight.y = 0;


                    //Vector3 moveDir = _pos; // World
                    Vector3 moveDir = camForward * _pos.z + camRight * _pos.x;
                    moveDir.y = 0;
                    moveDir.Normalize();

                    transform.position += transform.TransformDirection(moveDir) * speed * Time.deltaTime;

                    Quaternion targetRotation = Quaternion.LookRotation(transform.TransformDirection(moveDir.normalized));
                    
                    if (playerType == CharactorJobEnum.Gunner) 
                    {
                        targetRotation *= Quaternion.Euler(0, 60f, 0);//Animation Calibration Value
                    }

                    charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                   //전사 보정치 필요
                }
                else if (cameraMode == PlayerCameraMode.GunAttackMode)//Shoot
                {
                    Transform cam = transform.GetComponentInChildren<Camera>().transform;

                    Vector3 camForward = cam.forward;
                    camForward.y = 0;

                    Vector3 camRight = cam.right;
                    camRight.y = 0;

                    Vector3 moveDir = camForward.normalized * _pos.z + camRight.normalized * _pos.x;

                    Quaternion targetRotation = Quaternion.LookRotation(moveDir.normalized);
                    //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // 회전속도 예: 10f
                    //moveDir.z = moveDir.z - distancingValue;
                    transform.position += moveDir * speed * Time.deltaTime;
                }


            }
        }
        walkAnim(runState, _pos);
        //All
        //moveAnim(_pos.z);
        //Gunner
        //walkAnim(runState, playerType);
    }
    
    float jumpValue = 5f;
    protected void jump(GroundTouchState _state) 
    {

    }
}
    //public Vector3 MovePointSearchInit(Vector3 _pos)//Player State Object init
    //{
    //    if (_pos == null)
    //    {
    //        movePointSearch(out _pos);
    //    }
    //    else //
    //    {

    //    }
    //    movePointSearch(out _pos);
    //    return _pos;
    //}
    //private bool ObjectCheck() 
    //{

    //}
    //public Vector3 MovePointSearch() 
    //{
    //    SlotData slot = slotDatas.Peek();
    //    if (slot.ObjectState == PositionObjectState.Empty)
    //    {
    //        PositionObjectState state = PositionObjectState.Occupied;
    //        slot.ObjectState = state;
    //        GameObject go = slot.FootholdObject;
    //        movePosition = go.transform.position;
    //        slotDatas.Dequeue();
    //    }
    //    return movePosition;
    //}





