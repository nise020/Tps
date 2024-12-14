using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_Burst_Skill : Burst_Skill_Base
{
    //소총
    public override void BurstSkill(int _bullet, float _damage, float _runTime, float _coolTime) 
    {
        _bullet = 30000;//총알 갯수 
        _damage = 15.0f;//버스트 데미지
        _runTime = 15.0f;//버스트 유지시간
        _coolTime = 20.0f;//쿨타임
    }
}
