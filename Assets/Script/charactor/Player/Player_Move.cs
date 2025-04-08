using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player : Charactor
{

    //Input 기능을 사용할 경우 Que를 사용해서 저장 후에 순차적으로
    //처리하게 하지 않으면 입력값이 소실 된다

    //일정 시간이 지나도 가만히(움직이지 아노으면) 있으면 안 움직인가는 판단
    //분산시스템
    float playerStopDistanseValue = 1.0f;
    float playerRunDistanseValue = 10.0f;
    float playerWalkDistanseValue = 5.0f;
    protected PlayerWalkState playerWalkState = PlayerWalkState.None;
    protected float walkTimer = 0;
    protected float walkTime = 3.0f;

    Queue<Vector3> fsmPosQue = new Queue<Vector3>();
    public void Move_Npc()
    {
        //동일한 위치로 이동이 불가(플레이어 위치에 정확히 이동이 불가) 할 경우 플레이어 뒤쪽 위치에 이동
        //불가 하지 않을 경우 해당 위치로 계속 이동
        PlayerWalkState state = PlayerWalkState.None;
        Shared.GameManager.PlayerData(out Player _player);
        //

        if (_player.PlayerWalkSateInit(state) == false) //player Stop
        {
            return;
        }
        else if(_player.PlayerWalkSateInit(state) == true)//player Walk
        {
            _player.PositionObjectInit(out List<GameObject> _objects);

            //targetPos = Shared.GameManager.PlayerPos(targetPos);
            targetPos = _player.gameObject.transform.position;

            if (!fsmPosQue.Contains(targetPos))//value != Vector3
            {
                fsmPosQue.Enqueue(targetPos);//value Plus
            }
            int value = fsmPosQue.Count;
            Vector3 vector = fsmPosQue.Peek();//front value

            //player Back Position
            //Vector3 stopPoint = targetPos;
            Vector3 stopPoint = vector;
            Vector3 disTance = (stopPoint - gameObject.transform.position);

            //player Direction
            Quaternion rotation = Quaternion.LookRotation(disTance.normalized);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime * rotSpeed);

            if (Vector3.Distance(gameObject.transform.position, disTance) <= playerStopDistanseValue)//거리가 엄청 가까울때
            {
                playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
                fsmPosQue.Dequeue();
                //StartCoroutine(point());
                return;
            }
            else if (Vector3.Distance(gameObject.transform.position, disTance) <= playerRunDistanseValue &&
                     Vector3.Distance(gameObject.transform.position, disTance) >= playerWalkDistanseValue) 
            {
                playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
                playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            }
            else if (Vector3.Distance(gameObject.transform.position, disTance) >= playerRunDistanseValue)
            {
                playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
                //예외 처리가 필요함
            }
            gameObject.transform.position += disTance.normalized * speedValue * Time.deltaTime;
        }
        
    }
    public bool PlayerWalkSateInit(PlayerWalkState _state) 
    {
        _state = playerWalkState;

        if (_state == PlayerWalkState.Walk_Off)
        {
            return false;
        }
        else 
        {
            return true; 
        }
    }
    IEnumerator point()
    {
        Shared.GameManager.PlayerData(out Player _player);
        _player.PositionObjectInit(out List<GameObject> _objects);
        for (int number = 0; number < _objects.Count; number++) 
        {
            GameObject go = _objects[number];

        }

        yield return null;
    }
    public void PositionObjectInit(out List<GameObject> _objects)
    {
        _objects = backPositionObject;
    }
    public bool TargetMove(Vector3 _pos)
    {
        if (Vector3.Distance(transform.position, _pos) < 0.1)
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
                walkTimer = 0.0f;
                //float speed = runValue ? speedValue * 2 : speedValue;
                //transform.localPosition += direction * (speed) * Time.deltaTime;
                //rigid.velocity = direction * speed;

                if (playerType == CharactorJobEnum.Warrior || viewcam.GunModeCheck() == false)//nomal
                {
                    Vector3 moveDir = inPutPos; // World

                    Quaternion targetRotation = Quaternion.LookRotation(moveDir.normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // 회전속도: 10 정도 추천
                    moveDir.z = moveDir.z - distancingValue;
                    transform.position += moveDir * speedValue * Time.deltaTime;
                }
                else if (playerType == CharactorJobEnum.Gunner)
                {
                    Transform cam = transform.GetComponentInChildren<Camera>().transform;

                    Vector3 camForward = cam.forward;
                    Vector3 camRight = cam.right;
                    camForward.y = 0;
                    camRight.y = 0;

                    Vector3 moveDir = camForward.normalized * inPutPos.z + camRight.normalized * inPutPos.x;

                    Quaternion targetRotation = Quaternion.LookRotation(moveDir.normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // 회전속도 예: 10f
                    moveDir.z = moveDir.z - distancingValue;
                    transform.position += moveDir * speedValue * Time.deltaTime;
                }


            }
            else
            {
                return;
            }
        }
        else
        {
            walkTimer += Time.deltaTime;
            if (walkTimer > walkTime) 
            {
                playerWalkState = PlayerWalkState.Walk_Off;
            } 
            
        }
        //All
        //moveAnim(movePos.z);
        //Gunner
        //sideWalkAnim(movePos.x, playerType);
    }






}
