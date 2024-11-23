using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract partial class Bullet : Actor
{
    int targetnumber;
    public int speed;
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
        if (bulletTag == bulletType.MobGranad) { return; }
        if (bulletTag == bulletType.Mobbullet)//
        {
            //shared.BattelMgr.
        }
        else if (bulletTag == bulletType.Playerbullet) //오토기능도 참고 해야함
        {

        }
        //int speed = 3;
        Vector3 target = targetPos.position - transform.position;
        transform.position += (target).normalized * speed * Time.deltaTime;
        Debug.Log($"{transform.position}");
    }
    void FixedUpdate()
    {
        moveing();
    }

}
