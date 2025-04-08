using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;

public partial class Player : Charactor
{

    //Input ����� ����� ��� Que�� ����ؼ� ���� �Ŀ� ����������
    //ó���ϰ� ���� ������ �Է°��� �ҽ� �ȴ�

    //���� �ð��� ������ ������(�������� ������) ������ �� �����ΰ��� �Ǵ�
    //�л�ý���
    float playerStopDistanseValue = 0.3f;
    float playerRunDistanseValue = 10.0f;
    float playerWalkDistanseValue = 5.0f;
    protected PlayerWalkState playerWalkState = PlayerWalkState.None;
    protected NpcWalkState npcWalkState = NpcWalkState.Walk;
    protected float walkTimer = 0;
    protected float walkTime = 3.0f;

    Queue<Vector3> fsmPosQue = new Queue<Vector3>();
    public void Move_Npc(Player _player)
    {
        //������ ��ġ�� �̵��� �Ұ�(�÷��̾� ��ġ�� ��Ȯ�� �̵��� �Ұ�) �� ��� �÷��̾� ���� ��ġ�� �̵�
        //�Ұ� ���� ���� ��� �ش� ��ġ�� ��� �̵�
        PlayerWalkState state = PlayerWalkState.None;
        //Shared.GameManager.PlayerData(out Player _player);
        //
        if (_player.PlayerWalkSateInit() == false) //player Stop
        {
            return;
        }
        else if(_player.PlayerWalkSateInit() == true)//player Walk
        {
            //_player.PositionObjectInit(out List<GameObject> _objects);
            _player.PointSearch();
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

            float dist = Vector3.Distance(gameObject.transform.position, stopPoint);

            if (dist <= playerStopDistanseValue)
            {
                if (fsmPosQue.Count == 0)
                {
                    return;
                }
                else 
                {
                    fsmPosQue.Dequeue();
                    return;
                }
            }
            else if (dist <= playerRunDistanseValue && dist >= playerWalkDistanseValue)
            {
                playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
                playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
            }
            else if (dist >= playerRunDistanseValue)
            {
                playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
                playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
            }
            gameObject.transform.position += disTance.normalized * speedValue * Time.deltaTime;
        }
        
    }
    public bool PlayerWalkSateInit() 
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
    public void PointSearch() 
    {
        StartCoroutine(pointSearch());
    }
    public IEnumerator pointSearch()
    {
        foreach (KeyValuePair<GameObject, SlotData> slotData in slotStates) 
        {
            GameObject slotObj = slotData.Key;
            SlotData data = slotData.Value;
            if (slotData.Value.ObjectState == PositionObjectState.None) 
            {
                yield return slotData.Key;
            }
            Transform pos = data.SlotTransform;
        }
        yield return null;//None
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
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // ȸ���ӵ�: 10 ���� ��õ
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
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // ȸ���ӵ� ��: 10f
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
