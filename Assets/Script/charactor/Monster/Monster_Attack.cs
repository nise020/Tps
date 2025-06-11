using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Progress;

public partial class Monster : Character
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
        deathAnimation(MonsterDeathsState.Deaths_On);
    }

    protected void attackRangeCheck()
    {
        //attackAnimation(MonsterAttackState.Attack_On);

        float dist = Vector3.Distance(charactorModelTrs.position, targetTrs.position);
        if (dist < hitRange)
        {
            Player player = targetTrs.gameObject.GetComponentInParent<Player>();
            Shared.BattelManager.DamageCheck(this, HItPalyer, null);
        }
    }

}
