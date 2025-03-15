using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract partial class Monster : Charactor
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
    protected override void checkHp()
    {
        base.checkHp();
        HPBAR.SetHp(maxHP,cheHP);
    }
    protected override void dead() 
    {
        //Shared.BattelMgr.monsterData.Remove(mobKey);
        //GameObject go = Instantiate(deadEffect, transform.position, Quaternion.identity, creatTabObj);
        //StartCoroutine(EffectTime(go));
        mobAnimator.SetInteger("Death", 1);
        stateInIt();
        Shared.BattelManager.Resurrection(mobKey);
        gameObject.SetActive(false);
    }
    IEnumerator EffectTime(GameObject _obj) 
    {
        yield return new WaitForSeconds(4);
        _obj.SetActive(false);
    }

    Vector3 targetPos;
    Vector3 myPos = Vector3.zero;
    int moveNumber = 0;
   // public bool SearchCheack = false;
    float RotSpeed = 30;
    bool checkPos = false;
    public void NextPoint(ref bool _value)//������ ��� ã��
    {
        if (!_value) { return; }
        //������
        mobAnimator.SetInteger("Search", 0);
        float speed = speedValue;
        if (movePos.Count == 0) 
        {
            Debug.Log("�̵��� ��ġ�� ã���� ����");
        }
        if (moveNumber >= movePos.Count) 
        {
            moveNumber = 0;
            return;
        }

        if (checkPos == false) 
        {
            targetPos = gameObject.transform.position + movePos[moveNumber];
            //��ġ�� �������� �ʿ䰡 ����
            checkPos = true;
        }
        Vector3 nowPos = gameObject.transform.position;

        float value = Vector3.Distance(nowPos, targetPos);

        if (value > 0.1f)//Move
        {
            Vector3 dir = (targetPos - nowPos).normalized;

            Quaternion startRot = Quaternion.LookRotation(gameObject.transform.forward);
            Quaternion endRot = Quaternion.LookRotation(dir);
            gameObject.transform.localRotation = Quaternion.Lerp(startRot, endRot, RotSpeed);//* Time.deltaTime

            gameObject.transform.position += new Vector3(dir.x,0, dir.z) * speed * Time.deltaTime;
        }
        else
        {
            mobAnimator.SetInteger("Walk", 0);
            mobAnimator.SetInteger("Search", 1);
            moveNumber += 1;
            checkPos = false;
            _value = false;
        }
    }

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
