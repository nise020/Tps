using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Monster : Charactor
{
    public void DirectAttack(GameObject _obj,Vector3 _pos) 
    {
        Vector3 myPos = _obj.transform.position;
        float speed = speedValue;
        _obj.transform.position += (new Vector3(_pos.x,0, _pos.z) - myPos).normalized * speed * Time.deltaTime;
    }

    public Transform target;
    public float height = 5f;
    public float throuTime = 2f;
    public void granaidAttack(Vector3 _start, Vector3 _end, GameObject _obj) 
    {
        StartCoroutine(Throu(_start, _end, _obj));
    }
    IEnumerator Throu(Vector3 _start,Vector3 _end,GameObject _obj) 
    {    
        float elapsed = 0;

        while (elapsed < throuTime)
        {
            elapsed += Time.deltaTime;
            float time = elapsed / throuTime;

            Vector3 currentPos = Vector3.Lerp(_start, _end, time);
            currentPos.y += Mathf.Sin(time * Mathf.PI) * height;

            _obj.transform.position = currentPos;
            yield return null;
        }
        _obj.transform.position = _end;
    }
    //protected override void checkHp()
    //{
    //    base.checkHp();
    //    HPBAR.SetHp(maxHP,cheHP);
    //}
    protected override void dead() 
    {
        base.dead();
        //Shared.BattelMgr.monsterData.Remove(mobKey);
        //GameObject go = Instantiate(deadEffect, transform.position, Quaternion.identity, creatTabObj);
        //StartCoroutine(EffectTime(go));
        mobAnimator.SetInteger("Death", 1);
        stateInIt();
        Shared.MonsterManager.Resurrection(mobKey);
        gameObject.SetActive(false);
    }

    //int moveNumber = 0;
    // public bool SearchCheack = false;

    //public void Pattern(MonsterType _enum)
    //{
    //    if (_enum == MonsterType.Sphere)//��ü �� ���
    //    {
    //        if (moveing == false) return;
    //        if (moveing == true)//Animation Event
    //        {
    //            MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
    //        }
    //        //��� �̵��ϴ� ���� ����
    //        Vector3 myPos = MONSTER.gameObject.transform.position;
    //        float distanse = Vector3.Distance(myPos, targetPos);
    //        float targetvalue = MONSTER.attackDistanse;//�����Ÿ�

    //        if (distanse < 1.0f)
    //        {
    //            animator.SetInteger("Close", 0);
    //            animator.SetInteger("AttackDilray", 1);
    //            moveing = false;
    //            aIState = AI.Reset;
    //        }

    //    }
    //    else if (_enum == MonsterType.Spider)//�Ź��� ��� 
    //    {
    //        if (attackCheck == false)
    //        {
    //            GameObject go = Delivery.Instantiator(MONSTER.MobGrenade, eyePos.position, Quaternion.identity, creatTab);
    //            //���ҽ� ��Ȱ��
    //            MONSTER.granaidAttack(MONSTER.gameObject.transform.position, targetPos, go);
    //            animator.SetInteger("Attack", 0);
    //            attackCheck = true;
    //        }


    //        //�߰������� ������ �ϱ� ������ AddForce�� �߰��ؾ���
    //        //Instantiator�� �ƴ� SetActive�� ����ؼ� ���ҽ��� ���� �ؾ���
    //        //aIState = AI.Reset;
    //    }
    //    else if (_enum == MonsterType.Dron)//����� ��� 
    //    {
    //        MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
    //        animator.SetInteger("Attack", 0);
    //        aIState = AI.Reset;
    //    }
    //}

}
