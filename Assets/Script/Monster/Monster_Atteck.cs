using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    public void DirectAttack(GameObject _obj,Vector3 _pos) 
    {
        Vector3 myPos = _obj.transform.position;
        float speed = moveSpeed;
        _obj.transform.position += (_pos - myPos).normalized * speed * Time.deltaTime;
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
    protected override void dead() 
    {
        Shared.BattelMgr.monsterData.Remove(mobKey);
        GameObject go = Instantiate(deadEffect, transform.position, Quaternion.identity, creatTabObj);
        StartCoroutine(EffectTime(go));
        Destroy(this);
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
    public void readySearch(ref bool _value)//공격할 대상 찾기
    {
        if (!_value) { return; }
        mobAnimator.SetInteger("Search", 0);
        //재정의
        float speed = moveSpeed;
        if (movePosObj.Count == 0) 
        {
            Debug.Log("이동할 위치를 찾을수 없음");
        }

        if (moveNumber >= movePosObj.Count) 
        {
            moveNumber = 0;
            return;
        }

        Vector3 dir = movePosObj[moveNumber].transform.position;//에러
        Vector3 nowPos = gameObject.transform.position;

        float value = Vector3.Distance(nowPos, dir);

        if (value > 0.1f)//Move
        {

            Vector3 targetPos = (dir - nowPos).normalized;

            Quaternion startRot = Quaternion.LookRotation(gameObject.transform.forward);
            Quaternion endRot = Quaternion.LookRotation(targetPos);
            gameObject.transform.localRotation = Quaternion.Lerp(startRot, endRot, RotSpeed);//* Time.deltaTime

            gameObject.transform.position += targetPos * speed * Time.deltaTime;
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
