using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG_Burst_Skill : Burst_Skill_Base
{
    //�Ⱓ����
    public override void BurstSkill(int _bullet, float _damage, float _runTime, float _coolTime) 
    {
        _bullet = 30000;//�Ѿ� ���� 
        _damage = 15.0f;//����Ʈ ������
        _runTime = 15.0f;//����Ʈ �����ð�
        _coolTime = 20.0f;//��Ÿ��
    }
}
