using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public partial class Monster_Skill : MonoBehaviour
{
    public void nomalAttack(List<GameObject> LISTOBJ, int _number,ref int _min,int _max,GameObject OBJ,
        Vector3 _pos,Transform _creat)//�Ѿ� ����
    {
        if (_min >= _max) { return; }
        targetOn(ref _number, LISTOBJ);
        if (LISTOBJ[_number].transform.position == null) { return; }
        GameObject GO = Instantiate(OBJ, _pos,Quaternion.identity, _creat);
        Mob_Bullet BULLET = GO.GetComponent<Mob_Bullet>();
        BULLET.targetPos = LISTOBJ[_number].transform.position;
        _min += 1;

    }
    public void grenadeattack(List<GameObject> LISIOBJ, int _number, ref int _min, int _max, GameObject OBJ,
        Vector3 _pos, Transform _creat)//�����ʿ� 
    {
        if (_min >= _max) { return; }
        //targetOn(ref _number);
        targetOn(ref _number, LISIOBJ);
        if (LISIOBJ[_number].transform.position == null) { return; }
        GameObject GO = Instantiate(OBJ, _pos, Quaternion.identity, _creat);
        Mob_Bullet BULLET = GO.GetComponent<Mob_Bullet>();
        BULLET.targetPos = LISIOBJ[_number].transform.position;
        _min += 1;
    }
    public void jumpSkill(bool _canAttack,ref float _minTime,float _runTime,
        bool _search,int _number,  List<GameObject> LISTOBJ,GameObject _myObj,float _jump,Rigidbody RIGID)
        //������ ���� �ʿ�
    {
        if (_canAttack == false) { return; }

        _minTime += Time.deltaTime;
        float time = _minTime / _runTime;

        if (_minTime < _runTime)
        {
            if (_canAttack == false)
            {
                targetOn(ref _number, LISTOBJ);
                if (LISTOBJ[_number] == null)
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

            //float initialYVelocity = Mathf.Sqrt(2 * gravity * jumpHight); // �ʱ� �ӵ�
            //float elapsedTime = moveTimer; // ��� �ð�
            //float ypos = mpos.y + initialYVelocity * elapsedTime - 0.5f * gravity * Mathf.Pow(elapsedTime, 2); // Y�� ���� ����

            //// ��ǥ ������ Y�� �ݿ�
            //ypos = Mathf.Max(ypos, tpos.y); // ��ǥ Y������ �Ʒ��� �������� �ʵ��� ����
            //movePos.y = ypos;

            // ���� ��ġ ����
            //transform.position = movePos;
            #endregion


            #region AddForce
            float xzSpeed = _runTime;
            Vector3 dir = (LISTOBJ[_number].transform.position - _myObj.transform.position).normalized;
            dir.y = 0;

            float yForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * _jump);//�߷��� �����־ƾ� �Ѵ�

            Vector3 jumpPos = dir * xzSpeed + Vector3.up * yForce;

            RIGID.AddForce(jumpPos, ForceMode.VelocityChange);//����

            float dts = Vector3.Distance(LISTOBJ[_number].transform.position, _myObj.transform.position);
            if (dts <= 0.1)
            {
                //AI.Reset(); 
            }

            _canAttack = false;
            #endregion

            //float ypos = jumpHight * (1 - Mathf.Pow(2 * time - 1, 2)); // ������ ����
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
        //���Ͻ�



    }
    //public void DirectAttackSkill(bool _canAttack,bool _target,List<GameObject> LISTOBJ,) //����ó�� �ʿ�
    //{
    //    if (directAtkON == false) { return; }
    //    //moveTimer += Time.deltaTime;
    //    if (targetCheack == false)
    //    {
    //        targetOn(ref number);
    //        if (playerObj[number] == null || coverObj[number] == null)
    //        {
    //            return;
    //        }
    //        targetCheack = true;
    //        Debug.Log($"{number}");
    //    }
    //    //Vector3 dir = (playerObj[number].transform.position - transform.position).normalized;
    //    Vector3 dir = (coverObj[number].transform.position - transform.position).normalized;
    //    transform.position += dir * speed * Time.deltaTime;
    //}
    public void targetOn(ref int _value, List<GameObject> LISTOBJ)
    {
        int count = LISTOBJ.Count;//������ �÷��̾� ����
        _value = Random.Range(0, count);//�������� Ÿ�� ��ȣ ����
    }
}
