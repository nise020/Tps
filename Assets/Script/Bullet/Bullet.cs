using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Bullet : Gun
{
    [Header("�Ѿ� ���� �׸�")]
    int targetnumber;
    int damage = 1;
    public float speed = 0.0f;
    Transform targetPos;//���Ͱ� ������ ��ǥ
    RaycastHit hit;//�Ѿ��� ���� ��ǥ
    enum bulletType 
    {
        Mobbullet,
        Playerbullet,
        MobGranad,
    }
    [SerializeField] bulletType bulletTag;
    
    public void Initialize(Transform _target)
    {
        switch (bulletTag)
        {
            case bulletType.Mobbullet:
                targetPos.position = _target.position;
                break;
            case bulletType.MobGranad:
                targetPos.position = _target.position;
                break;
            case bulletType.Playerbullet:
                hit.point = _target.position;
                break;
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public void moveing()//��� ���� �ʿ�
    {
        if (bulletTag == bulletType.MobGranad) 
        {
            damage = 5;
            speed = 3.0f;
            transform.root.rotation = Quaternion.Euler(0,1,0);
            Vector3 target = targetPos.position - transform.position;
            transform.position += (target).normalized * speed * Time.deltaTime;
        }
        if (bulletTag == bulletType.Mobbullet)
        {
            speed = 5.0f;
            Vector3 target = targetPos.position - transform.position;
            transform.position += (target).normalized * speed * Time.deltaTime;

        }
        else if (bulletTag == bulletType.Playerbullet) 
        {
            switch (GunEnumType)
            {
                case GunTags.SR:
                    damage = damage + srGunDmg;
                    break;
            }
            speed = 5.0f;
            Vector3 target = targetPos.position - transform.position;
            transform.position += (hit.point*2).normalized * speed * Time.deltaTime;//�ӽ�
        }
        
    }
    
    protected override void Update()
    {
        moveing();
    }

    protected override void GunTargetRaycast()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            
        }
        //Vector3 ray = cam.ScreenToWorldPoint(Input.mousePosition);
        //if (Physics.Raycast(transform.position,ray, out RaycastHit hit))
    }
}
