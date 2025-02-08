using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Monster : Charactor
{

    protected virtual void MobAttackTimecheck() 
    {
        Patterntimer += Time.deltaTime;
        int number = 0;
        if (Patterntimer >  Patternltime) 
        {
            if (NumberOn == false)
            {
                number = Random.Range(0, 1);
                NumberOn = true;
            }
            MobAttackTimer(number);
        }
        
    }
    protected void MobAttackTimer(int number) 
    {
        if (number == 0) 
        {
            //nomalAttack();
        }
        else if (number == 1) 
        {
            //GrenadeAttack();
        }
        //yield return null;
    }

    protected virtual void targetOn(ref int _value,List<GameObject>_listObj) 
    {
        int count = _listObj.Count;//공격할 플레이어 정렬
        _value = Random.Range(0, count);//랜덤으로 타겟 번호 선정
    }

    protected override void Dead() 
    {
        Shared.BattelMgr.monsterData.Remove(mobKey);
        Destroy(this);
    }


    Vector3 targetPos;
    Vector3 myPos = Vector3.zero;
    int moveNumber = 0;
    public bool SearchCheack = false;
    float RotSpeed = 30;
    public void readySearch(ref bool _value)//공격할 대상 찾기
    {
        if (!_value) { return; }
        mobAnimator.SetInteger("Search", 0);
        //재정의
        float speed = moveSpeed;


        if (moveNumber >= movePosObj.Count) 
        {
            moveNumber = 0;
            return;
        }

        Vector3 dir = movePosObj[moveNumber].transform.position;
        Vector3 nowPos = gameObject.transform.position;

        float value = Vector3.Distance(nowPos, dir);

        if (value > 0.1f)//Move
        {
            gameObject.transform.position += (dir - nowPos).normalized * speed * Time.deltaTime;

            Vector3 targetPos = dir;
            Vector3 d = (targetPos - gameObject.transform.position);
            Quaternion startRot = Quaternion.LookRotation(gameObject.transform.forward);
            Quaternion endRot = Quaternion.LookRotation(d.normalized);
            gameObject.transform.localRotation = Quaternion.Lerp(startRot, endRot, RotSpeed);//* Time.deltaTime
        }
        else
        {
            mobAnimator.SetInteger("Walk", 0);
            mobAnimator.SetInteger("Search", 1);
            _value = false;
            moveNumber += 1;
        }
    }

}
