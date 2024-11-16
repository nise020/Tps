using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract partial class Bullet : Actor
{
    int targetnumber;
    public int speed;
    Transform targetPos;
    enum bulletType 
    {
        Mobbullet,
        Playerbullet,
        MobGranad,
    }
    [SerializeField] bulletType bulletTag;
    private void Start()
    {
        //Initialize(targetPos);
    }
    public void Initialize(Transform target)
    {
        targetPos = target;
    }
    public int targetNumber(int number)
    {
        return number;
    }
    public void moveing()
    {
        if (bulletTag == bulletType.MobGranad) { return; }
        if (bulletTag == bulletType.Mobbullet)//
        {
            //shared.BattelMgr.
        }
        else if (bulletTag == bulletType.Playerbullet) //�����ɵ� ���� �ؾ���
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
