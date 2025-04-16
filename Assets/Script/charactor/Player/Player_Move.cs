using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public partial class Player : Charactor
{
    //Input ����� ����� ��� Que�� ����ؼ� ���� �Ŀ� ����������
    //ó���ϰ� ���� ������ �Է°��� �ҽ� �ȴ�

    //���� �ð��� ������ ������(�������� ������) ������ �� �����ΰ��� �Ǵ�
    //�л�ý���
    float runDistanseValue = 15.0f;
    float walkDistanseValue = 10.0f;
    float playerStopDistanseValue = 0.2f;
    protected PlayerWalkState playerWalkState = PlayerWalkState.None;
    protected PlayerRunState playerRunState = PlayerRunState.None;
    protected PlayerShitState playerShitState = PlayerShitState.None;
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
        disTance.y = 0.0f;//�Ͻ���

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
        charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, rotation, Time.deltaTime * rotSpeed);

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
                playerWalkState = PlayerWalkState.Walk_On;
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
                    charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, targetRotation, Time.deltaTime * rotSpeed);
                }
                else if (cameraMode == PlayerCameraMode.GunAttackMode)//Shoot
                {
                    Transform cam = transform.GetComponentInChildren<Camera>().transform;

                    Vector3 camForward = cam.forward;
                    Vector3 camRight = cam.right;
                    camForward.y = 0;
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





