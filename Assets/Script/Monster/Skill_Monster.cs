using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Skill_Monster 
{

    protected bool NumberOn = false;
    //protected int number;
    Vector3 targetpos;
    [Header("������ Ÿ��(����)")]

    [Header("�⺻ Ÿ�̸�")]
    //ublic int Patternt = 0f;
    protected float Patterntimer = 0f;
    protected float Patternltime = 10.0f;

    [Header("�Ϲ� ���� Ƚ��(DefoltMob)")]
    protected int AttackMinCount = 0;
    protected int AttackMaxCount = 6;

    [Header("��ô�� Ƚ��")]
    protected int ThroutMinCount = 0;
    protected int ThroutMaxCount = 2;
    public int ID = 1;

    [Header("��ô�� Ƚ��")]
    float moveTimer = 0.0f;
    float jumpStart = 0.0f;
    float jumpHight = 5.0f;
    float runningTime = 5.0f;
    bool targetCheack = false;

    [Header("��ġ�� �ӵ� Ƚ��")]
    float speed = 10.0f;
    public void targetOn(ref int _value,List<Player> _OBJ)
    {
        int count = _OBJ.Count;//������ �÷��̾� ����
        _value = Random.Range(0, count);//�������� Ÿ�� ��ȣ ����
    }
    public void NomalAttack(ref bool _attackOff,int _number, GameObject _bullet , List<Player> targetObj, Vector3 _arm_Pos,Transform CREATTSR)//�Ѿ� ����
    {

        if (targetObj[_number].transform.position == null) { return; }

        GameObject GO = Delivery.Instantiate(_bullet, _arm_Pos, Quaternion.identity, CREATTSR);

        Bullet_Monster BULLET = GO.GetComponent<Bullet_Monster>();

        BULLET.targetPos = targetObj[_number].transform.position;

        if (AttackMinCount >= AttackMaxCount)
        {
            _attackOff = true;
            AttackMinCount = 0;
            return;
        }
        else 
        {
            AttackMinCount += 1; 
        }
    }
    public void Grenadeattack(int _number, GameObject _Grenade, List<GameObject> targetObj, Vector3 _armPos, Transform CREATTSR)//�����ʿ� 
    {
        if (ThroutMinCount >= ThroutMaxCount) { return; }

        if (targetObj[_number].transform.position == null) { return; }

        GameObject GO = Delivery.Instantiate(_Grenade, _armPos, Quaternion.identity, CREATTSR);

        Bullet_Monster BULLET = GO.GetComponent<Bullet_Monster>();

        BULLET.targetPos = targetObj[_number].transform.position;

        ThroutMinCount += 1;
    }
    public Vector3 DirectAttackSkill(int _number,List<Player> targetObj,Vector3 _pos) //����ó�� �ʿ�
    {
        Vector3 dir = (targetObj[_number].transform.position - _pos).normalized;
        //Vector3 dir = (coverObj[_number].transform.position - transform.position).normalized;
        _pos += dir * speed * Time.deltaTime;
        return _pos;
    }
    public void JumpSkill(int _number, List<Player> targetObj, ref bool _canAttack,Vector3 _pos,Rigidbody _rigid)
        //������ ���� �ʿ�
    {
        moveTimer += Time.deltaTime;
        float time = moveTimer / runningTime;

        if (moveTimer < runningTime)
        {
            if (_canAttack == false)
            {
                if (targetObj[_number] == null)
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
            float xzSpeed = runningTime;
            Vector3 dir = (targetObj[_number].transform.position - _pos).normalized;//���� ���� ����
            dir.y = 0;

            float yForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHight);//�߷��� �����־ƾ� �Ѵ�

            Vector3 jumpPos = dir * xzSpeed + Vector3.up * yForce;

            _rigid.AddForce(jumpPos, ForceMode.VelocityChange);//����

            float dts = Vector3.Distance(targetObj[_number].transform.position, _pos);
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
        //Debug.Log($"Time: {time}");
        //���Ͻ�



    }
}
