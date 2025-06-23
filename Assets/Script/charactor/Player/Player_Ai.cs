using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public partial class Player : Character
{
    public void AiTagetUpdate(bool _check)
    {
        PLAYERAI.TargetStateUpdate(_check);
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
        //플레이어의 거리에 따라 달리고 걷는게 가능 하지만 찔끔 움직이면 움직임이 더딘다
        //: 개선이 필요함
        //y값이 다르면 문제가 생김

        //동일한 위치로 이동이 불가(플레이어 위치에 정확히 이동이 불가) 할 경우 플레이어 뒤쪽 위치에 이동
        //불가 하지 않을 경우 해당 위치로 계속 이동
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
            npcRunStateAnimation(0.0f); // 정지 애니메이션 강제
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
            disTance.y = 0.0f;//일시적

            float dist = Vector3.Distance(gameObject.transform.position, stopPoint);
            npcRunStateAnimation(dist);
            if (dist > runDistanseValue)//run
            {
                gameObject.transform.position += disTance.normalized * StatusData[StatusType.Speed] * 2 * Time.deltaTime;
            }
            else if (dist < runDistanseValue && dist >= playerStopDistanseValue)//walk
            {
                gameObject.transform.position += disTance.normalized * StatusData[StatusType.Speed] * Time.deltaTime;
            }

            //player Direction
            Quaternion rotation = Quaternion.LookRotation(disTance.normalized);
            charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, rotation, Time.deltaTime * rotationSpeed);

        }


    }
    public void Ai_TargetMove(Vector3 _pos,float _distance) 
    {
        Vector3 direction = _pos - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, targetRotation, Time.deltaTime * rotationSpeed);


        Vector3 moveDir = direction.normalized;
        transform.position += moveDir * StatusData[StatusType.Speed] * Time.deltaTime;

        npcRunStateAnimation(_distance);
    }
    public float TargetDistanseCheck(Vector3 _pos)//수정 필요
    {
        Vector3 direction = _pos - transform.position;
        direction.y = 0f; 

        float distance = direction.magnitude;

        return distance;
    }

    public bool AttackDistanseCheck(float _value)
    {
        float typeVAlue = 0.0f;
        switch (playerStateData.PlayerType) 
        {
            case PlayerType.Gunner:
                typeVAlue = 15.0f;
                break;
            case PlayerType.Warrior:
                typeVAlue = 0.3f;
                break;
            default:
               Debug.LogError($"playerStateData.PlayerType = {playerStateData.PlayerType}") ;
                break;
        }
        if (_value <= typeVAlue)//값을 상수가 아닌값으로 수정 필요
        {
            // _value = 0;
            return true;
        }
        else 
        {
            return false;
        }

    }
    public override bool CharacterStateCheck()
    {
        if (playerStateData.AttackState == AttackState.Attack_Off)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
        

    
    public bool SearchCheck(out Monster _monster)
    {
        //float radius = 8f;
        //float fieldOfView = 90f;
        _monster = Shared.MonsterManager.monsterSearch(gameObject, radius);

        if (_monster == null)
        {
            Debug.LogError($"_monster = {_monster}");
            return false;
        }
        else
        {
            Vector3 targetPos = _monster.BodyObjectLoad().position;
            Vector3 myPos = gameObject.transform.position;

            float distance = Vector3.Distance(targetPos, myPos);

            if (radius >= distance)
            {
                return true;
            }
            else
            {
                _monster = null;
                return false;
            }
        }
    }

    public void Ai_Attack(Transform _transform)//거리이내에 있는 적에게 데미지 로직 필요
    {
        //charactorModelTrs.rotation = Quaternion.LookRotation(_transform.position);
        //charactorModelTrs.LookAt(_transform);
        //Debug.Log($"{_transform.position}");
        //charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        if (_transform == null) return;

        if (playerStateData.NpcWalkState != NpcWalkState.Stop)
            npcRunStateAnimation(0.0f);

        Vector3 newTargetPos = _transform.position;

        //AutoAttack(targetTrs); // 애니메이션 실행+Attack_On


        if (_transform == null) return;

        if (targetTrs == null || Vector3.Distance(_transform.position, targetTrs.position) > 0.01f)
        {
            // 새 타겟이거나 위치 변화 → 코루틴 리셋
            if (UpperBodyColutin != null)
            {
                StopCoroutine(UpperBodyColutin);
                UpperBodyColutin = null;
            }

            targetTrs = _transform;

            forceUpperBody = true;
            UpperBodyColutin = AdjustUpperBodyToTargetLoop(MAINWEAPON as Gun);
            StartCoroutine(UpperBodyColutin);
        }

        AutoAttack(targetTrs);

    }
    //protected IEnumerator AimAndShootRoutine(Gun gun)
    //{
    //    yield return AdjustUpperBodyToTargetOnce(gun); // 상체 보정

    //    //forceUpperBody = false;

    //    AutoAttack(targetTrs);//애니메이션 실행

    //    //forceUpperBody = true;
    //    //UpperBodyColutin = null;
    //}
    protected virtual void AutoAttack(Transform _transform) 
    {

    }
    public override bool DamageEventCheck()
    {
        if (playerStateData.DamageEvent == DamageEvent.Event_On)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }

    public override void DamageEventUpdate(DamageEvent _event)
    {
        playerStateData.DamageEvent = _event;
    }
}
