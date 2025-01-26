using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR;

public partial class AiMonster : AiBase
{
    List<Player> target;
    Transform creatTab;
    bool attackOn = true;
    int targetNumber;//������ ��ǥ�� ��ȣ
    float timer = 0.0f;
    float time = 5.0f;
    MobAnim mobAnimState = MobAnim.Idle;
    Transform eyePos;
    Vector3 targetPos;
    Animator animator;
    string idleAnim = ($"{MobAnim.Idle}");
    string walkAnim = ($"{MobAnim.Walk}");
    string attackAnim = ($"{MobAnim.Attack}");
    string serchAnim = ($"{MobAnim.Serch}");
    string dilrayAnim = ($"{MobAnim.AttackDilray}");
    bool searchchack = false;
    bool movechack = false;
    List<GameObject> searchPosObj;
    Vector3 startPos = Vector3.zero;
    Vector3 myPos = Vector3.zero;
    int moveNumber = 0;
    //Monster_Skill SKILL = new Monster_Skill();

    //private eMobType MOBTYPE;
    //Monster Monster;
    //FSM
    //ĳ���Ϳ��� AI�� ȣ���� �ʿ�


    public void Pattern()
    {
        switch (MobType)
        {
            case eMobType.Defolt://�Ϲ� ��Ÿ (Ƚ������)
                SKILL.NomalAttack(ref nextOn_Off, targetNumber, MONSTER.MobBullet, target, MONSTER.AttackArm.transform.position, creatTab);
                break;
            case eMobType.Flying://���� ����
                MONSTER.gameObject.transform.position = SKILL.DirectAttackSkill
                    (targetNumber, target, MONSTER.gameObject.transform.position);
                break;
            case eMobType.Huge://���� ����
                SKILL.JumpSkill(targetNumber, target, ref attackOn, 
                    MONSTER.gameObject.transform.position, MONSTER.mobRigid);
                break;
            case eMobType.Sphere:
                SKILL.JumpSkill(targetNumber, target, ref attackOn,
                    MONSTER.gameObject.transform.position, MONSTER.mobRigid);
                break;
        }
    }

    public override void State()
    {
        switch (aIState)
        {
            case eAI.Create:
                Create();
                break;
            case eAI.Search:
                Search();
                break;
            case eAI.Move:
                Move();
                break;
            case eAI.Attack:
                Attack();
                break;
            case eAI.Reset:
                Reset();
                break;
        }
        //_aIState = aIState;
    }
    protected override void Create()//����
    {
        animator = MONSTER.mobanimator;
        //string open = ($"{mobAnimInfoName.Open}");

        string idle = ($"{mobAnimInfoName.Idle}");
        Debug.Log($"Create");

        if (animCheck(idleAnim, idle))
        {

            searchPosObj = MONSTER.movePosObj;

            creatTab = Shared.BattelMgr.creatTab;
            eyePos = MONSTER.eyeObj.transform;
            startPos = MONSTER.gameObject.transform.position;
            aIState = eAI.Search;

        }
        else { return; }
    }

    protected override void Search()//������ ��� ã��
    {
        if (searchchack == false) 
        {
            searchchack = true;
            animator.SetInteger(serchAnim, 1);
        }

        Debug.Log($"Search");
        string serch = ($"{mobAnimInfoName.Serch}");

        if (Physics.Raycast(eyePos.position,
           eyePos.transform.forward, out RaycastHit hit) && animCheck(serchAnim, serch))
        //�÷��̾ �ɷ�����
        {
            string player = ($"{LayerTag.Player}");//enum
            string cover = ($"{LayerTag.Cover}");
            int layer = hit.collider.gameObject.layer;
            string name = LayerMask.LayerToName(layer);


            if (name == player)
            {
                targetPos = hit.point;
                animator.SetInteger(serchAnim, 0);
                aIState = eAI.Move;

                searchchack = false;
            }
            else if (name == cover)
            {
                return;
            }
        }
        else 
        {
            //if (animCheck(serchAnim, serch)) //�÷��̾ ����,�ִϸ��̼��� ��������
            //{
            //    if (movechack == false) 
            //    {
            //        animator.SetInteger(serchAnim, 0);

            //        animator.SetInteger(walkAnim, 1);

            //        targetPos = searchPosObj[moveNumber].transform.position;//��ġ ����Ʈ ��ġ
            //        myPos = MONSTER.gameObject.transform.position;//�� ��ġ

            //        movechack = true;
            //    }
            //    Vector3 dir = MONSTER.gameObject.transform.position;

            //    dir = searchPointMove(searchPosObj,myPos) * Time.deltaTime;

            //    MONSTER.gameObject.transform.position = dir;
            //    MONSTER.gameObject.transform.rotation = Quaternion.Euler(myPos);

            //    //MONSTER.gameObject.transform.LookAt(searchPosObj[moveNumber].transform);

            //}
            //else { return; }
        }
        
    }
    public void PointMove(string _value) 
    {
        Debug.Log($"PointMove");
        if (_value == "test") 
        {
            animator.SetInteger(walkAnim, 1);
        }
        else 
        {
            animator.SetInteger(walkAnim, 0);
        }
    }
    protected Vector3 searchPointMove(List <GameObject> _searchObj,Vector3 _pos) 
    {
        Vector3 dir = _searchObj[moveNumber].transform.position;

        if (Vector3.Dot(dir, _pos) < 0.0f) 
        {
            moveNumber += 1;
            movechack = false;

            if (_searchObj[moveNumber] == null) 
            {
                moveNumber = 0;
            }
        }
        dir = startPos;
        Vector3 distanse = dir - MONSTER.gameObject.transform.position;
        return distanse.normalized;
    }
    protected override void Move()//�̵�
    {
        Debug.Log($"Move");
        //animator.SetInteger(moveAnim, 1);
        Vector3 myPos = MONSTER.gameObject.transform.position;
        float distanse = Vector3.Distance(myPos, targetPos);
        float targetvalue = MONSTER.attackDistanse;
        if (distanse < targetvalue) 
        {
            aIState = eAI.Attack;
        }
    }

    protected override void Attack()//����
    {
        animator.SetInteger(attackAnim, 1);
        Debug.Log($"Attack");
        Pattern();



        if (nextOn_Off == true)
        {
            nextOn_Off = false;
            aIState = eAI.Reset;
        }
    }
    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
    {
        Debug.Log($"Reset");
        attackOn = true;
        targetNumber = 0;
        aIState = eAI.Search;
    }

    public bool animCheck(string _parameter, string _animText)
    {
        //int index = attackLayerIndex;

        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);//layer
        float time = animStateInfo.normalizedTime;

        //Debug.Log($"{time}");
        if (time >= 1.0f && animStateInfo.IsName(_animText))//�ִϸ��̼� ������
        {
            Debug.Log($"{time}");
            string idle = ($"{mobAnimInfoName.Idle}");
            string open = ($"{mobAnimInfoName.Open}");
            string serch = ($"{mobAnimInfoName.Serch}");
            if (_animText == idle)
            {
                Debug.Log($"{idle}");
            }
            else if (_animText == open)
            {
                Debug.Log($"{open}");
            }
            else if ( _animText == serch) 
            {
                Debug.Log($"{serch}");
            }

            //animator.SetLayerWeight(0, 0.0f);

            animator.SetInteger(_parameter, 0);

            //Debug.Log($"{time} end");
            time = 0.0f;
            return true;
        }
        return false;//������
    }

}
