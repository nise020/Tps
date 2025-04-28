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
    //    if (_enum == MonsterType.Sphere)//구체 일 경우
    //    {
    //        if (moveing == false) return;
    //        if (moveing == true)//Animation Event
    //        {
    //            MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
    //        }
    //        //계속 이동하는 문제 있음
    //        Vector3 myPos = MONSTER.gameObject.transform.position;
    //        float distanse = Vector3.Distance(myPos, targetPos);
    //        float targetvalue = MONSTER.attackDistanse;//사정거리

    //        if (distanse < 1.0f)
    //        {
    //            animator.SetInteger("Close", 0);
    //            animator.SetInteger("AttackDilray", 1);
    //            moveing = false;
    //            aIState = AI.Reset;
    //        }

    //    }
    //    else if (_enum == MonsterType.Spider)//거미일 경우 
    //    {
    //        if (attackCheck == false)
    //        {
    //            GameObject go = Delivery.Instantiator(MONSTER.MobGrenade, eyePos.position, Quaternion.identity, creatTab);
    //            //리소스 재활용
    //            MONSTER.granaidAttack(MONSTER.gameObject.transform.position, targetPos, go);
    //            animator.SetInteger("Attack", 0);
    //            attackCheck = true;
    //        }


    //        //추가적으로 던져야 하기 떄문에 AddForce를 추가해야함
    //        //Instantiator가 아닌 SetActive를 사용해서 리소스를 재사용 해야함
    //        //aIState = AI.Reset;
    //    }
    //    else if (_enum == MonsterType.Dron)//드론일 경우 
    //    {
    //        MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
    //        animator.SetInteger("Attack", 0);
    //        aIState = AI.Reset;
    //    }
    //}

}
