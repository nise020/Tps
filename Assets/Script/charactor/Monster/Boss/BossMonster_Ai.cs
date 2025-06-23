using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossMonster : Monster
{
    public override void MovePoint()//Search
    {
        return;
    }

    public override void Ai_TargetMove(Vector3 _pos, float _distance)
    {
        Vector3 direction = _pos - transform.transform.position;
        float distanse = Vector3.Distance(_pos, transform.transform.position);

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        //charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, targetRotation, Time.deltaTime * rotationSpeed);


        transform.transform.position += direction.normalized * StatusData[StatusType.Speed] * Time.deltaTime;

        transform.rotation = Quaternion.Slerp(transform.rotation,
        targetRotation, Time.deltaTime * rotationSpeed);

        monsterStateData.WalkState = MonsterWalkState.Walk_On;
        moveAnimation(monsterStateData.WalkState);
    }

    public override void Ai_Attack(Transform _transform)//거리이내에 있는 적에게 데미지 로직 필요
    {
        if (monsterStateData.AttackState == MonsterAttackState.Attack_On)
        {
            return;
        }

        //charactorModelTrs.rotation = Quaternion.LookRotation(_transform.position);

        //stopDilay = true;
        transform.LookAt(_transform);
        targetTrs = _transform;

        if (monsterStateData.WalkState != MonsterWalkState.Walk_Off) 
        {
            monsterStateData.WalkState = MonsterWalkState.Walk_Off;
            moveAnimation(monsterStateData.WalkState);

            MonsterAttack();
        }

    }

    protected override void MonsterAttack() 
    {
        monsterStateData.AttackState = MonsterAttackState.Attack_On;
        int value = Random.Range(0, 2);   
        monsterAnimator.SetInteger(MonsterAnimParameters.Attack.ToString(), value);
    }
}
