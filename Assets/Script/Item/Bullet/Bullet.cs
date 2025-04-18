using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Bullet 
{
    //[Header("총알 관련 항목")]
    //public float speed = 0.0f;
    //Vector3 targetPos;//몬스터가 공격할 목표
    //RaycastHit hit;//총알이 맞출 목표
    //public bulletType BulletType;

    public Vector3 moveing(Vector3 _myPos, Vector3 _targetPos, BulletType _tag,float _speed)//대거 수정 필요
    {
        if (_tag == BulletType.MobGranad || _tag == BulletType.Mobbullet)
        {
            Vector3 direction = (_targetPos - _myPos).normalized;
            _myPos += direction * _speed * Time.deltaTime;
        }
        else if (_tag == BulletType.Playerbullet)//세분화 필요
        {
            //Vector3 direction = (_targetPos - _myPos).normalized;
            _myPos += _targetPos.normalized * _speed * Time.deltaTime;
            //switch (BulletType)
            //{
            //    case GunTags.SR:
            //        damage = damage + srGunDmg;
            //        break;
            //}
            //speed = 5.0f;
            //Vector3 target = targetPos.position - _trs.position;
            //_trs.position += (hit.point*2).normalized * speed * Time.deltaTime;//임시
        }
        return _myPos;

    }
    
}
