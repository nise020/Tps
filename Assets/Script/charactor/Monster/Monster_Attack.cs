using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Progress;

public partial class Monster : Charactor
{
    public Transform target;
    public float height = 5f;
    public float throuTime = 3f;

    public void DirectAttack(GameObject _obj,Vector3 _pos) 
    {
        Vector3 myPos = _obj.transform.position;
        float speed = speedValue;
        _obj.transform.position += (new Vector3(_pos.x,0, _pos.z) - myPos).normalized * speed * Time.deltaTime;
    }

    public void granaidAttack(Vector3 _start, Vector3 _end, GameObject _obj) 
    {
        StartCoroutine(Throu(_start, _end, _obj));
    }
    IEnumerator Throu(Vector3 _start,Vector3 _end,GameObject _obj) 
    {    
        float elapsed = 0;
        Vector3 horizontal = 
            new Vector3(_end.x - _start.x, 0, _end.z - _start.z);

        float destanse = horizontal.magnitude;
        Vector3 direction = horizontal.normalized;

        while (elapsed < throuTime)
        {
            elapsed += Time.deltaTime;
            float time = elapsed / throuTime;

            float parabola = 4 * height * time * (1 - time); // 최고점에서 t=0.5

            Vector3 currentPos = _start + direction * destanse * time;
            currentPos.y = Mathf.Lerp(_start.y, _end.y, time) + parabola;


            //ector3 currentPos = Vector3.Lerp(_start, _end, time);
            //currentPos.y += Mathf.Sin(time * Mathf.PI) * height;

            _obj.transform.position = currentPos;
            yield return null;
        }
        _obj.transform.position = _end;
    }
    protected override void death() 
    {
        deathAnimation(MonsterAnimParameters.Death);

        base.death();

        ITEM.gameObject.SetActive(true);
        ITEM.gameObject.transform.position = charactorModelTrs.position;
        Debug.Log($"ITEM.gameObject.transform.position = {ITEM.gameObject.transform.position}\n" +
                  $"charactorModelTrs.position = {charactorModelTrs.position}");
        Shared.MonsterManager.Resurrection(mobKey);

        stateInIt();
        HPBAR.SetHp(maxHP, cheHP);
        gameObject.SetActive(false);
    }

    

}
