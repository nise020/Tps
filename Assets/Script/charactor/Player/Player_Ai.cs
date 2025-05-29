using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    public void AiTagetUpdate(bool _check)
    {
        PLAYERAI.DefenderState(_check);
    }
    public void AiUpdate(Player _player) 
    {
        PLAYERAI.ChangePlayer(_player);
    }
    public void PlayerModeUpdate(PlayerModeState _playerMode) 
    {
        playerStateData.ModeState = _playerMode;
    }
    public void Ai_Move(Player _player)
    {
        //�÷��̾��� �Ÿ��� ���� �޸��� �ȴ°� ���� ������ ��� �����̸� �������� �����
        //: ������ �ʿ���
        //y���� �ٸ��� ������ ����

        //������ ��ġ�� �̵��� �Ұ�(�÷��̾� ��ġ�� ��Ȯ�� �̵��� �Ұ�) �� ��� �÷��̾� ���� ��ġ�� �̵�
        //�Ұ� ���� ���� ��� �ش� ��ġ�� ��� �̵�
        //PlayerWalkState state = PlayerWalkState.None;
        //Shared.GameManager.PlayerData(out Player PLAYER);

        if (playerStateData.objectInfo == FindMoveObject.None)//Object Search
        {
            playerBackObject = _player.movePointSearch(out LayerName layer);
            slotlayerName = layer;
            playerStateData.objectInfo = FindMoveObject.Find;
        }

        //movePosition = PLAYER.MovePointSearchInit(movePosition);//movePosition

        movePosition = _player.SlotPositionUpdate(slotlayerName);
        Vector3 vector = Vector3.zero;

        float distanse = Vector3.Distance(movePosition, gameObject.transform.position);

        if (distanse <= 3.0f)
        {
            npcRunStateAnimation(0.0f); // ���� �ִϸ��̼� ����
            return;
        }

        if (distanse < playerStopDistanseValue)//distanse < 0.3
        {
            npcRunStateAnimation(distanse);
            #region Player Follow
            if (aiMovePosQue.Count == 0)
            {
                return;
            }
            else
            {
                aiMovePosQue.Dequeue();
                return;
            }
            #endregion
        }
        else
        {
            #region Player Follow
            if (_player.PlayerObjectWalkCheck() == false)//player stop
            {
                if (aiMovePosQue.Count != 0) 
                {
                    aiMovePosQue.Clear();
                }
                aiMovePosQue.Enqueue(movePosition);//value Plus
            }
            else//player walk
            {
                if (!aiMovePosQue.Contains(movePosition))//value != Vector3 or null
                {
                    aiMovePosQue.Enqueue(movePosition);//value Plus
                }
            }
            vector = aiMovePosQue.Peek();//front value
            #endregion

            Vector3 stopPoint = movePosition;
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

        float value = Vector3.Distance(gameObject.transform.position, _pos);

        playerAnimtor.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        playerAnimtor.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);

        return value;
    }

    public bool AttackDistanseCheck(float _value)
    {
        float typeVAlue = 0.0f;
        switch (playerStateData.PlayerType) 
        {
            case PlayerType.Gunner:
                typeVAlue = 3.0f;
                break;
            case PlayerType.Warrior:
                typeVAlue = 0.3f;
                break;
            default:
               Debug.LogError($"playerStateData.PlayerType = {playerStateData.PlayerType}") ;
                break;
        }

        if (_value <= typeVAlue)//���� ����� �ƴѰ����� ���� �ʿ�
        {
            // _value = 0;
            return true;
        }

        return false;
    }
    public bool SearchCheck(out Vector3 _pos) //�Ŵ��� ���׼� ��ġ �ϴ� �������� ��ġ�� �ʿ�
    {
        //float radius = 8f;
        //float fieldOfView = 90f;
        Vector3 position = Shared.MonsterManager.monsterSearch(gameObject, radius);
        if (position == Vector3.zero)
        {
            _pos = Vector3.zero;
            return false;
        }
        else
        {
            float distance = Vector3.Distance(position, gameObject.transform.position);
            if (radius >= distance)
            {
                _pos = position;
                return true;
            }
            else
            {
                _pos = Vector3.zero;
                return false;
            }
        }
    }

    public void Ai_Attack()//�Ÿ��̳��� �ִ� ������ ������ ���� �ʿ�
    {
        attackMovement();
    }
    protected virtual void attackMovement() 
    {

    }


}
