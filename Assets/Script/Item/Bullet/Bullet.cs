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

    public Vector3 moveing(Vector3 _myPos, Vector3 _direction, BulletType _tag,float _speed)//��� ���� �ʿ�
    {
        if (_tag == BulletType.MobGranad || _tag == BulletType.Mobbullet)
        {
            Vector3 direction = (_direction - _myPos).normalized;
            _myPos += direction * _speed * Time.deltaTime;
        }
        else if (_tag == BulletType.Playerbullet)//����ȭ �ʿ�
        {
            _myPos += _direction.normalized * _speed * Time.deltaTime;

        }
        return _myPos;

    }
    
}
