using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    public void Ai_Move(Player _player)
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
            npcRunStateAnimation(distanse);
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
            npcRunStateAnimation(dist);
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

    public float TargetPosition_Move(Vector3 _pos)//���� �ʿ�
    {
        Vector3 stopPoint = _pos;
        Vector3 disTance = (stopPoint - gameObject.transform.position);

        Quaternion rotation = Quaternion.LookRotation(disTance.normalized);
        charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, rotation, Time.deltaTime * rotationSpeed);
        
        float diatanse = Vector3.Distance(stopPoint , gameObject.transform.position);

        if (diatanse > 0) 
        {
            gameObject.transform.position += disTance.normalized * speedValue * Time.deltaTime;
        }

        //gameObject.transform.position += disTance.normalized * speedValue * Time.deltaTime;
        float value = Vector3.Distance(gameObject.transform.position, _pos);

        playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);

        return value;
    }

    public bool AttackDistanseCheck(float value)
    {
        //float value = WEAPON.WeaponStatusLoad(ItemStatusType.Range);
        if (value <= 0.1)//���� ����� �ƴѰ����� ���� �ʿ�
        {
            value = 0;
            return true;
        }

        return false;
    }
    public void AiAttack()//�Ÿ��̳��� �ִ� ������ ������ ���� �ʿ�
    {
        attackMovement();
    }
    protected virtual void attackMovement() 
    {

    }
}
