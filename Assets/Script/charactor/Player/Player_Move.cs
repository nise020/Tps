using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public partial class Player : Charactor
{
    //Input ����� ����� ��� Que�� ����ؼ� ���� �Ŀ� ����������
    //ó���ϰ� ���� ������ �Է°��� �ҽ� �ȴ�

    //���� �ð��� ������ ������(�������� ������) ������ �� �����ΰ��� �Ǵ�
    //�л�ý���
    float runDistanseValue = 15.0f;
    float playerStopDistanseValue = 0.3f;
    
    protected PlayerWalkState playerWalkState = PlayerWalkState.Walk_Off;
    protected PlayerRunState playerRunState = PlayerRunState.Run_Off;
    protected PlayerShitState playerShitState = PlayerShitState.ShitUP;
    protected NpcWalkState npcWalkState = NpcWalkState.Stop;
    protected FindMoveObject objectInfo = FindMoveObject.None;
    protected RunState runState = RunState.Walk;
    protected AttackState attackState = AttackState.None;

    protected float notWalkTimer = 0;
    protected float notWalkTime = 3.0f;
    protected Vector3 movePosition = new Vector3();
    protected Vector3 targetPos = new Vector3();
    Queue<Vector3> fsmPosQue = new Queue<Vector3>();
    LayerName layerName = LayerName.None;
    public void Move_Npc(Player _player)
    {
        //�÷��̾��� �Ÿ��� ���� �޸��� �ȴ°� ���� ������ ��� �����̸� �������� �����
        //: ������ �ʿ���
        //y���� �ٸ��� ������ ����

        //������ ��ġ�� �̵��� �Ұ�(�÷��̾� ��ġ�� ��Ȯ�� �̵��� �Ұ�) �� ��� �÷��̾� ���� ��ġ�� �̵�
        //�Ұ� ���� ���� ��� �ش� ��ġ�� ��� �̵�
        //PlayerWalkState state = PlayerWalkState.None;
        //Shared.GameManager.PlayerData(out Player PLAYER);

        if (objectInfo == FindMoveObject.None)//Object Search
        {
            _player.movePointSearch(out LayerName layer);
            layerName = layer;
            objectInfo = FindMoveObject.Find;
        }

        //movePosition = PLAYER.MovePointSearchInit(movePosition);//movePosition

        movePosition = _player.SlotPositionUpdate(layerName);
        Vector3 vector = Vector3.zero;

        float distanse = Vector3.Distance(movePosition, gameObject.transform.position);

        if (distanse < playerStopDistanseValue)//distanse < 0.3
        {
            npcRunStateAnim(distanse);
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
        else
        {
            #region Player Follow
            if (_player.PlayerObjectWalkCheck() == false)//player stop
            {
                fsmPosQue.Clear();
                fsmPosQue.Enqueue(movePosition);//value Plus
            }
            else//player walk
            {
                if (!fsmPosQue.Contains(movePosition))//value != Vector3 or null
                {
                    fsmPosQue.Enqueue(movePosition);//value Plus
                }
            }
            vector = fsmPosQue.Peek();//front value
            #endregion

            //Vector3 vector = movePosition;

            //player Back Position
            //Vector3 stopPoint = targetPos;
            Vector3 stopPoint = vector;
            Vector3 disTance = (stopPoint - gameObject.transform.position);
            disTance.y = 0.0f;//�Ͻ���

            float dist = Vector3.Distance(gameObject.transform.position, stopPoint);
            npcRunStateAnim(dist);
            if (dist > runDistanseValue)//run
            {
                gameObject.transform.position += disTance.normalized * speedValue * 2 * Time.deltaTime;
            }
            else if (dist < runDistanseValue && dist >= playerStopDistanseValue)//walk
            {
                gameObject.transform.position += disTance.normalized * speedValue * Time.deltaTime;
            }

            //player Direction
            Quaternion rotation = Quaternion.LookRotation(disTance.normalized);
            charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, rotation, Time.deltaTime * rotationSpeed);

        }


    }
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
    public bool AttackDistanseCheck(float value) 
    {
        if (value <= 0.1)//���� ����� �ƴѰ����� ���� �ʿ�
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
        charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, rotation, Time.deltaTime * rotationSpeed);

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
                    Vector3 moveDir = _pos; // World
                    moveDir.y = 0;
                    moveDir.Normalize();

                    transform.position += transform.TransformDirection(moveDir) * speed * Time.deltaTime;

                    Quaternion targetRotation = Quaternion.LookRotation(transform.TransformDirection(moveDir.normalized));
                    
                    if (playerType == CharactorJobEnum.Gunner) 
                    {
                        targetRotation *= Quaternion.Euler(0, 60f, 0);//Animation Calibration Value
                    }

                    charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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
                    //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // ȸ���ӵ� ��: 10f
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
    protected float gravityValue = - 9.81f;
    Vector3 velocity;
    CapsuleCollider CpasuleColl;
    [SerializeField] float groundCheckLenght;
    float groundCheckRadius = 0.3f;
    GroundTouchState GroundTouchState = GroundTouchState.GroundNoneTouch;
    protected void groundCheak() 
    {
        int layer = LayerMask.NameToLayer(LayerName.Ground.ToString());
        
        bool isGround = Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, 
            out RaycastHit hit, groundCheckLenght + 0.1f, layer);

        if (isGround)
        {
            if (GroundTouchState == GroundTouchState.GroundNoneTouch) 
            {
                GroundTouchState = GroundTouchState.GroundTouch;
            }
            if (velocity.y < 0)
                velocity.y = 0f;
        }
        else
        {
            if (GroundTouchState == GroundTouchState.GroundTouch)
            {
                GroundTouchState = GroundTouchState.GroundNoneTouch;
            }
            velocity.y += gravityValue * Time.deltaTime;
        }
        transform.position += velocity * Time.deltaTime;
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





