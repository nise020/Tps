using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Add 
{
    public Dictionary<int, Burst_Skill_Base> burstSkills = new Dictionary<int, Burst_Skill_Base>();
    public Skill_Add()
    {
        AddBurst();
    }

    public void AddSkill_1()
    {

    }
    public void AddSkill_2()
    {

    }
    public void AddBurst()
    {
        burstSkills.Add(1, new AR_Burst_Skill());//AR,//소총
        burstSkills.Add(2, new MG_Burst_Skill());//MG,//머신건
        burstSkills.Add(3, new SG_Burst_Skill());//SG,//샷건
        burstSkills.Add(4, new SMG_Burst_Skill());//SMG,//기간단총
        burstSkills.Add(5, new SR_Burst_Skill());//SR,//저격총
    }
    public void UseBurstSkill(int _key, int _bullet, float _damage, float _runTime, float _coolTime)
    {
        if (burstSkills.ContainsKey(_key))//key 확인
        {
            burstSkills[_key].BurstSkill(_bullet, _damage, _runTime, _coolTime);
        }
        else
        {
            Debug.Log("해당 무기 스킬이 없습니다: " + _key);
        }
    }
}
