using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player : Charactor
{
    //Input 기능을 사용할 경우 Que를 사용해서 저장 후에 순차적으로
    //처리하게 하지 않으면 입력값이 소실 된다

    //일정 시간이 지나도 가만히(움직이지 않으면) 있으면 안 움직인가는 판단
    //분산시스템
    float runDistanseValue = 15.0f;
    float walkDistanseValue = 10.0f;
    float playerStopDistanseValue = 0.2f;
    protected PlayerWalkState playerWalkState = PlayerWalkState.None;
    protected NpcWalkState npcWalkState = NpcWalkState.Walk;
    protected FindMoveObject objectInfo = FindMoveObject.None;
    protected float notWalkTimer = 0;
    protected float notWalkTime = 3.0f;
    protected Vector3 movePosition = new Vector3();
    protected Vector3 targetPos = new Vector3();
    Queue<Vector3> fsmPosQue = new Queue<Vector3>();
    LayerName layerName = LayerName.None;
    public void Move_Npc(Player _player)
    {
        //플레이어의 거리에 따라 달리고 걷는게 가능 하지만 찔끔 움직이면 움직임이 더딘다
        //: 개선이 필요함


        //동일한 위치로 이동이 불가(플레이어 위치에 정확히 이동이 불가) 할 경우 플레이어 뒤쪽 위치에 이동
        //불가 하지 않을 경우 해당 위치로 계속 이동
        //PlayerWalkState state = PlayerWalkState.None;
        //Shared.GameManager.PlayerData(out Player PLAYER);
        if (objectInfo == FindMoveObject.None)//Object Search
        {
            _player.movePointSearch(out LayerName layer);
            layerName = layer;
            objectInfo = FindMoveObject.Find;
        }

        #region Player Follow
        //movePosition = PLAYER.MovePointSearchInit(movePosition);//movePosition

        movePosition = _player.SlotPositionUpdate(layerName);

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

        if (dist > runDistanseValue)//run
        {
            npcRunStateAnim(dist); 
            gameObject.transform.position += disTance.normalized * speedValue * 2 * Time.deltaTime;
            return;
        }
        else if (dist <= runDistanseValue && dist >= playerStopDistanseValue)//walk
        {
            npcRunStateAnim(dist);
            gameObject.transform.position += disTance.normalized * speedValue * Time.deltaTime;
            return;
        }
        else if (dist <= playerStopDistanseValue)//&& PLAYER.playerwalksateinit() == false
        {
            npcRunStateAnim(dist);
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
            //gameObject.transform.position += disTance.normalized * speedValue * Time.deltaTime;
        }
       
    }
    public Vector3 MovePointUpdate()
    {
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
    
    public void PositionObjectInit(out List<GameObject> _objects)
    {
        _objects = backPositionObject;
    }
    public bool AttackDistanseCheck(float value) 
    {
        if (value <= 0.1)//값을 상수가 아닌값으로 수정 필요
        {
            value = 0;
            return true;
        }
        return false;
    }
    public float TargetMove(Vector3 _pos)
    {
        Vector3 stopPoint = _pos;
        Vector3 disTance = (stopPoint - gameObject.transform.position);

        Quaternion rotation = Quaternion.LookRotation(disTance.normalized);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime * rotSpeed);

        gameObject.transform.position += disTance * speedValue * Time.deltaTime;
        float value = Vector3.Distance(gameObject.transform.position, _pos);

        playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);

        return value;
    }
    protected override void move(CharctorStateEnum _value,Vector3 _pos)//Controll
    {
        if (_value == CharctorStateEnum.Npc)
        {
            return;
        }
        else if (_value == CharctorStateEnum.Player)
        {
            //Vector3 direction = transform.TransformDirection(inPutPos.normalized);

            if (_pos.magnitude > 0.1f)
            {
                playerWalkState = PlayerWalkState.Walk_On;
                notWalkTimer = 0.0f;
                float speed = speedValue;
                if (runState==RunState.Run) 
                {
                    speed = speedValue * 2;
                }
                //transform.localPosition += direction * (speed) * Time.deltaTime;
                //rigid.velocity = direction * speed;

                if (viewcam.GunModeCheck() == false)//nomal
                {
                    Vector3 moveDir = inPutPos; // World

                    Quaternion targetRotation = Quaternion.LookRotation(moveDir.normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // 회전속도: 10 정도 추천
                    //moveDir.z = moveDir.z - distancingValue;
                    transform.position += moveDir * speed * Time.deltaTime;
                }
                else if (viewcam.GunModeCheck() == true)//Shoot
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
                    transform.position += moveDir * speed * Time.deltaTime;
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




}
