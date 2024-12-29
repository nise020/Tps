using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Add 
{
    Skill SKILL = new Skill();
    public Dictionary<int, Burst_Skill_Base> burstSkills = new Dictionary<int, Burst_Skill_Base>();
    public Skill_Add()
    {
        AddBurst();
        AddSkill1();
        AddSkill2();
    }

    public void AddSkill1()
    {

    }
    public void AddSkill2()
    {

    }
    public void AddBurst()
    {
        burstSkills.Add(1, new AR_Burst_Skill());//AR,//����
        burstSkills.Add(2, new MG_Burst_Skill());//MG,//�ӽŰ�
        burstSkills.Add(3, new SG_Burst_Skill());//SG,//����
        burstSkills.Add(4, new SMG_Burst_Skill());//SMG,//�Ⱓ����
        burstSkills.Add(5, new SR_Burst_Skill());//SR,//������
    }
    public void UseBurstSkill(int _key, int _bullet, float _damage, float _runTime, float _coolTime)
    {
        if (burstSkills.ContainsKey(_key))//key Ȯ��
        {
            burstSkills[_key].BurstSkill(_bullet, _damage, _runTime, _coolTime);
        }
        else
        {
            Debug.Log("�ش� ���� ��ų�� �����ϴ�: " + _key);
        }
    }

    //public Character GetCharacter(int index) //Dictionary ����
    //{
    //   // CharacterMap.Add(index,player);
    //    if (CharacterMap.ContainsKey(index))
    //        return CharacterMap[index];

    //    CharacterMap.Remove(index);
    //    CharacterMap.Clear();

    //    var pair = CharacterMap.GetEnumerator();

    //    while (pair.MoveNext()) 
    //    {
    //        Character Character = pair.Current.Value;
    //    }

    //    return null;
    //}
}
