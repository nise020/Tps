using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpiderMob : Monster
{
    public override void MovePoint()//Search
    {

        if (movePosition == Vector3.zero)
        {
            movePosition = FindSlot();
        }

        Vector3 disTance = (movePosition - charactorModelTrs.position);
        disTance.y = 0.0f;

        float dist = Vector3.Distance(movePosition, charactorModelTrs.position);

        if (dist <= stopDistanseValue)
        {
            //stopDilay = false;
            movePosition = Vector3.zero;
            if (monsterStateData.WalkState != MonsterWalkState.Walk_Off)
            {
                monsterStateData.WalkState = MonsterWalkState.Walk_Off;
            }
            //moveAnimation(monsterStateData.WalkState);
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(disTance.normalized);

            charactorModelTrs.position += disTance.normalized * speedValue * Time.deltaTime;

            charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation,
                rotation, Time.deltaTime * rotationSpeed);

            monsterStateData.WalkState = MonsterWalkState.Walk_On;

        }

            moveAnimation(monsterStateData.WalkState);
    }

    public override void Ai_TargetMove(Vector3 _pos, float _distance)
    {
        Vector3 direction = _pos - charactorModelTrs.transform.position;
        float distanse = Vector3.Distance(_pos, charactorModelTrs.transform.position);

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        //charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, targetRotation, Time.deltaTime * rotationSpeed);


        charactorModelTrs.position += direction.normalized * speedValue * Time.deltaTime;

        charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation,
        targetRotation, Time.deltaTime * rotationSpeed);

        monsterStateData.WalkState = MonsterWalkState.Walk_On;
        moveAnimation(monsterStateData.WalkState);//<- 여기 수정 필요
    }

    public override void Ai_Attack(Transform _transform)//거리이내에 있는 적에게 데미지 로직 필요
    {
        if (monsterStateData.AttackState == MonsterAttackState.Attack_On)
        {
            return;
        }

        //charactorModelTrs.rotation = Quaternion.LookRotation(_transform.position);

        //stopDilay = true;

        monsterStateData.WalkState = MonsterWalkState.Walk_Off;
        moveAnimation(monsterStateData.WalkState);

        charactorModelTrs.LookAt(_transform);
        targetTrs = _transform;

        MonsterAttack();

        //Debug.Log($"{_transform.position}");
        //charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        //npcRunStateAnimation(0.0f);

        //AutoAttack();
    }
}
