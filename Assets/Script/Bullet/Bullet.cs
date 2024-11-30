using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Bullet : Gun
{
    [Header("총알 관련 항목")]
    int targetnumber;
    int damage = 1;
    public float speed = 0.0f;
    Transform targetPos;//공격할 목표
    RaycastHit hit;
    enum bulletType 
    {
        Mobbullet,
        Playerbullet,
        MobGranad,
    }
    [SerializeField] bulletType bulletTag;
    
    public void Initialize(Vector3 target)
    {
        switch (bulletTag)
        {
            case bulletType.Mobbullet:
                targetPos.position = target;
                break;
            case bulletType.Playerbullet:
                hit.point = target;
                break;
            case bulletType.MobGranad:
                targetPos.position = target;
                break;
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public void moveing()
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
            transform.position += (hit.point*2).normalized * speed * Time.deltaTime;//임시
        }
        
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
    protected override void FixedUpdate()
    {
        moveing();
    }

}
