using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public partial class Player : Character
{

    public bool PlayerObjectWalkCheck() 
    {
        if (PlayerStateData.WalkState == PlayerWalkState.Walk_On)
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
    
    public void PositionObjectInit(out List<GameObject> _objects)
    {
        _objects = backPositionObject;
    }

   
    protected override void move(PlayerModeState _value,Vector3 _pos)//Controll
    {
        if (_value == PlayerModeState.Npc)
        {
            return;
        }
        else if (_value == PlayerModeState.Player)
        {
            if (PlayerStateData.ShitState == PlayerShitState.ShitDown) 
            {
                shitdownCheak();
                PlayerStateData.ShitState = PlayerShitState.ShitUP;
            }

            if (_pos.magnitude > 0.1f)
            {
                //playerWalkState = PlayerWalkState.Walk_On;
                notWalkTimer = 0.0f;

                float speed = speedValue;

                if (PlayerStateData.runState == RunState.Run) 
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
                    
                    if (PlayerStateData.PlayerType == PlayerType.Gunner) 
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
        walkAnim(PlayerStateData.runState, _pos);
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





