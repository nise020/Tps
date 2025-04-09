using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;

public partial class Player : Charactor
{

    //Input 기능을 사용할 경우 Que를 사용해서 저장 후에 순차적으로
    //처리하게 하지 않으면 입력값이 소실 된다

    //일정 시간이 지나도 가만히(움직이지 않으면) 있으면 안 움직인가는 판단
    //분산시스템
    float playerStopDistanseValue = 0.1f;
    float runDistanseValue = 10.0f;
    float walkDistanseValue = 5.0f;
    protected PlayerWalkState playerWalkState = PlayerWalkState.None;
    protected NpcWalkState npcWalkState = NpcWalkState.Walk;
    protected FindMoveObject objectInfo = FindMoveObject.None;
    protected float notWalkTimer = 0;
    protected float notWalkTime = 3.0f;
    protected Vector3 movePosition = new Vector3();
    protected Vector3 targetPos = new Vector3();
    Queue<Vector3> fsmPosQue = new Queue<Vector3>();
    public void Move_Npc(Player _player)
    {
        //동일한 위치로 이동이 불가(플레이어 위치에 정확히 이동이 불가) 할 경우 플레이어 뒤쪽 위치에 이동
        //불가 하지 않을 경우 해당 위치로 계속 이동
        //PlayerWalkState state = PlayerWalkState.None;
        Shared.GameManager.PlayerData(out Player PLAYER);


        if (objectInfo == FindMoveObject.None)//Object Search
        {
            movePosition = PLAYER.MovePointSearch();
            objectInfo = FindMoveObject.Find;
        }
        else //FindMoveObject.Find;
        {
            movePosition = PLAYER.MovePointUpdate();
        }
        #region Player Follow
        //movePosition = PLAYER.MovePointSearchInit(movePosition);//movePosition

        if (!fsmPosQue.Contains(movePosition))//value != Vector3 or null
        {
            fsmPosQue.Enqueue(movePosition);//value Plus
        }
        //int value = fsmPosQue.Count;
        Vector3 vector = fsmPosQue.Peek();//front value

        #endregion

        //Vector3 vector = movePosition;

        //player Back Position
        //Vector3 stopPoint = targetPos;
        Vector3 stopPoint = vector;
        Vector3 disTance = (stopPoint - gameObject.transform.position);

        //player Direction
        Quaternion rotation = Quaternion.LookRotation(disTance.normalized);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime * rotSpeed);

        float dist = Vector3.Distance(gameObject.transform.position, stopPoint);
        
        if (dist <= runDistanseValue && dist >= walkDistanseValue)
        {
            gameObject.transform.position += disTance.normalized * speedValue * Time.deltaTime;
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        }
        else if (dist >= runDistanseValue)
        {
            gameObject.transform.position += disTance.normalized * speedValue * Time.deltaTime;
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        }
        else if (dist <= playerStopDistanseValue && PLAYER.playerwalksateinit()==false)
        {
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
            #region Player Follow
            if (fsmPosQue.Count == 0)
            {
                return;
            }
            else
            {
                fsmPosQue.Dequeue();
                return;
            }
            #endregion
        }

    }
    public Vector3 MovePointUpdate() 
    {
        return movePosition;
    }
    public Vector3 MovePointSearch() 
    {
        SlotData slot = slotDatas.Peek();
        if (slot.ObjectState == PositionObjectState.Empty)
        {
            PositionObjectState state = PositionObjectState.Occupied;
            slot.ObjectState = state;
            GameObject go = slot.FootholdObject;
            movePosition = go.transform.position;
            slotDatas.Dequeue();
        }
        return movePosition;
    }

    public bool playerwalksateinit()//player state object
    {
        if (playerWalkState == PlayerWalkState.Walk_Off)
        {
            return false;
        }
        else
        {
            return true;
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
    //private void movePointSearch(out Vector3 _pos)//Player State Object
    //{

    //    //사망시 포인트 비워주기가 필요
    //    Vector3 Value = Vector3.zero;
    //    foreach (KeyValuePair<GameObject, SlotData> slotData in slotStates) 
    //    {
    //        SlotData dataInfo = slotData.Value;
    //        if (dataInfo.ObjectState == PositionObjectState.None)
    //        {
    //            GameObject slotObj = slotData.Key;
    //            Debug.Log($"\nslotObj={slotObj}" +
    //                $"\ndataInfo={dataInfo}");

    //            //_pos = slotObj.transform.position;
    //            Value = slotObj.transform.position;
    //            PositionObjectState state = PositionObjectState.Occupied;
    //            slotStates[slotObj].ObjectState = state;
    //            _pos = Value;
    //            break;
    //            //slotData.Value.ObjectState = PositionObjectState.Occupied;
    //            //yield return _pos;
    //        }
    //        //Transform pos = dataInfo.SlotTransform;
    //        //_pos = pos.position;
    //    }
    //    _pos = Value;
    //    //yield return null;//None
    //}
    public void PositionObjectInit(out List<GameObject> _objects)
    {
        _objects = backPositionObject;
    }
    public bool TargetMove(Vector3 _pos)
    {
        float value = Vector3.Distance(transform.position, _pos);
        if (value < 0.1)
        {
            return true;
        }

        Quaternion targetRotation = Quaternion.LookRotation(_pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
        _pos.y = 0.0f;
        transform.position += _pos.normalized * speedValue * Time.deltaTime;
        return false;
    }
    protected override void move(CharctorStateEnum _value)//Controll
    {
        if (_value == CharctorStateEnum.Npc)
        {
            return;
        }
        else if (_value == CharctorStateEnum.Player)
        {
            //Vector3 direction = transform.TransformDirection(inPutPos.normalized);

            if (inPutPos.magnitude > 0.1f)
            {
                playerWalkState = PlayerWalkState.Walk_On;
                notWalkTimer = 0.0f;
                //float speed = runValue ? speedValue * 2 : speedValue;
                //transform.localPosition += direction * (speed) * Time.deltaTime;
                //rigid.velocity = direction * speed;

                if (viewcam.GunModeCheck() == false)//nomal
                {
                    Vector3 moveDir = inPutPos; // World

                    Quaternion targetRotation = Quaternion.LookRotation(moveDir.normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // 회전속도: 10 정도 추천
                    moveDir.z = moveDir.z - distancingValue;
                    transform.position += moveDir * speedValue * Time.deltaTime;
                }
                else if (viewcam.GunModeCheck() == true)
                {
                    Transform cam = transform.GetComponentInChildren<Camera>().transform;

                    Vector3 camForward = cam.forward;
                    Vector3 camRight = cam.right;
                    camForward.y = 0;
                    camRight.y = 0;

                    Vector3 moveDir = camForward.normalized * inPutPos.z + camRight.normalized * inPutPos.x;

                    Quaternion targetRotation = Quaternion.LookRotation(moveDir.normalized);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // 회전속도 예: 10f
                    moveDir.z = moveDir.z - distancingValue;
                    transform.position += moveDir * speedValue * Time.deltaTime;
                }


            }
            else
            {
                notWalkTimer += Time.deltaTime;
                if (notWalkTimer > notWalkTime)
                {
                    playerWalkState = PlayerWalkState.Walk_Off;
                }
            }
        }
        //All
        //moveAnim(movePos.z);
        //Gunner
        //sideWalkAnim(movePos.x, playerType);
    }






}
