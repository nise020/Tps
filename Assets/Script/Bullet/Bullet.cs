using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Bullet : Gun
{
    int targetnumber;
    int damage = 1;
    public float speed = 0.0f;
    Transform targetPos;//공격할 목표
    enum bulletType 
    {
        Mobbullet,
        Playerbullet,
        MobGranad,
    }
    [SerializeField] bulletType bulletTag;
    
    public void Initialize(Transform target)
    {
        targetPos = target;
    }

    
    public void moveing()
    {
        if (bulletTag == bulletType.MobGranad) 
        {
            damage = 5;
            speed = 3.0f;
            transform.root.rotation = Quaternion.Euler(0,1,0);
        }
        if (bulletTag == bulletType.Mobbullet)
        {
            speed = 5.0f;
            transform.localScale = Vector3.one;
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
        }
        Vector3 target = targetPos.position - transform.position;
        transform.position += (target).normalized * speed * Time.deltaTime;
    }
    void FixedUpdate()
    {
        moveing();
    }

}
