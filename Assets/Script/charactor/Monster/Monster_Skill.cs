using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public partial class Monster_Skill : MonoBehaviour
{
    public void nomalAttack(List<GameObject> _listObj, int _number,ref int _min,int _max,GameObject _obj,
        Vector3 _pos,Transform _creat)//총알 공격
    {
        if (_min >= _max) { return; }
        targetOn(ref _number, _listObj);
        if (_listObj[_number].transform.position == null) { return; }
        GameObject go = Instantiate(_obj, _pos,Quaternion.identity, _creat);
        Mob_Bullet bullet = go.GetComponent<Mob_Bullet>();
        bullet.targetPos = _listObj[_number].transform.position;
        _min += 1;

    }
    public void grenadeattack(List<GameObject> _listObj, int _number, ref int _min, int _max, GameObject _obj,
        Vector3 _pos, Transform _creat)//수정필요 
    {
        if (_min >= _max) { return; }
        //targetOn(ref _number);
        targetOn(ref _number, _listObj);
        if (_listObj[_number].transform.position == null) { return; }
        GameObject go = Instantiate(_obj, _pos, Quaternion.identity, _creat);
        Mob_Bullet bullet = go.GetComponent<Mob_Bullet>();
        bullet.targetPos = _listObj[_number].transform.position;
        _min += 1;
    }
    public void jumpSkill(bool _canAttack,ref float _minTime,float _runTime,
        bool _search,int _number,  List<GameObject> _listObj,GameObject _myObj,float _jump,Rigidbody _rigid)
        //수정이 많이 필요
    {
        if (_canAttack == false) { return; }

        _minTime += Time.deltaTime;
        float time = _minTime / _runTime;

        if (_minTime < _runTime)
        {
            if (_canAttack == false)
            {
                targetOn(ref _number, _listObj);
                if (_listObj[_number] == null)
                {
                    return;
                }
                _canAttack = true;

            }
            #region Vector3
            //float gravity = Physics.gravity.magnitude;
            //Vector3 mpos = transform.position;
            //Vector3 tpos = playerObj[number].transform.position;
            //Vector3 movePos = Vector3.Lerp(mpos, tpos, time);
            //movePos.y = 0f;

            //float initialYVelocity = Mathf.Sqrt(2 * gravity * jumpHight); // 초기 속도
            //float elapsedTime = moveTimer; // 경과 시간
            //float ypos = mpos.y + initialYVelocity * elapsedTime - 0.5f * gravity * Mathf.Pow(elapsedTime, 2); // Y축 공식 적용

            //// 목표 지점의 Y값 반영
            //ypos = Mathf.Max(ypos, tpos.y); // 목표 Y값보다 아래로 내려가지 않도록 제한
            //movePos.y = ypos;

            // 최종 위치 갱신
            //transform.position = movePos;
            #endregion


            #region AddForce
            float xzSpeed = _runTime;
            Vector3 dir = (_listObj[_number].transform.position - _myObj.transform.position).normalized;
            dir.y = 0;

            float yForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * _jump);//중력이 켜져있아야 한다

            Vector3 jumpPos = dir * xzSpeed + Vector3.up * yForce;

            _rigid.AddForce(jumpPos, ForceMode.VelocityChange);//점프

            float dts = Vector3.Distance(_listObj[_number].transform.position, _myObj.transform.position);
            if (dts <= 0.1)
            {
                //AI.Reset(); 
            }

            _canAttack = false;
            #endregion

            //float ypos = jumpHight * (1 - Mathf.Pow(2 * time - 1, 2)); // 포물선 공식
            //movePos.y = ypos + Mathf.Lerp(mpos.y, tpos.y, time);
            //transform.position = movePos;


            //float ypos = Mathf.Sin(Mathf.PI * time) * jumpHight;
            //Vector3 landingPos = new Vector3(movePos.x, ypos + tpos.y, movePos.z);
        }
        else
        {
            //transform.position = playerObj[_number].transform.position;
            //transform.rotation = Quaternion.identity;
            //moveTimer = 0;
        }
        Debug.Log($"Time: {time}");
        //디스턴스



    }

    public void targetOn(ref int _value, List<GameObject> _listObj)
    {
        int count = _listObj.Count;//공격할 플레이어 정렬
        _value = Random.Range(0, count);//랜덤으로 타겟 번호 선정
    }
}
