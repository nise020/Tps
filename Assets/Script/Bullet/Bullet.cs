using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Bullet 
{
    //[Header("�Ѿ� ���� �׸�")]
    //public float speed = 0.0f;
    //Vector3 targetPos;//���Ͱ� ������ ��ǥ
    //RaycastHit hit;//�Ѿ��� ���� ��ǥ
    //public bulletType BulletType;

    public Vector3 moveing(Vector3 _myPos, Vector3 _targetPos, bulletType _tag,float _speed)//��� ���� �ʿ�
    {
        if (_tag == bulletType.MobGranad || _tag == bulletType.Mobbullet)
        {
            Vector3 direction = (_targetPos - _myPos).normalized;
            _myPos += direction * _speed * Time.deltaTime;
        }
        else if (_tag == bulletType.Playerbullet)//����ȭ �ʿ�
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
            //_trs.position += (hit.point*2).normalized * speed * Time.deltaTime;//�ӽ�
        }
        return _myPos;

    }
    
}
