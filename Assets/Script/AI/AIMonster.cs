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
    int targetNumber;//공격할 목표의 번호
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
    //캐릭터에서 AI를 호출할 필요


    public void Pattern()
    {
        switch (MobType)
        {
            case eMobType.Defolt://일반 평타 (횟수제한)
                SKILL.NomalAttack(ref nextOn_Off, targetNumber, MONSTER.MobBullet, target, MONSTER.AttackArm.transform.position, creatTab);
                break;
            case eMobType.Flying://자폭 공격
                MONSTER.gameObject.transform.position = SKILL.DirectAttackSkill
                    (targetNumber, target, MONSTER.gameObject.transform.position);
                break;
            case eMobType.Huge://자폭 공격
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
    protected override void Create()//생성
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

    protected override void Search()//공격할 대상 찾기
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
        //플레이어가 걸렸을때
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
            //if (animCheck(serchAnim, serch)) //플레이어가 없고,애니메이션이 끝났을때
            //{
            //    if (movechack == false) 
            //    {
            //        animator.SetInteger(serchAnim, 0);

            //        animator.SetInteger(walkAnim, 1);

            //        targetPos = searchPosObj[moveNumber].transform.position;//서치 포인트 위치
            //        myPos = MONSTER.gameObject.transform.position;//내 위치

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
    protected override void Move()//이동
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

    protected override void Attack()//공격
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
    protected override void Reset()//사이클 끝(보통 다시 공격 대상 탐색)
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
        if (time >= 1.0f && animStateInfo.IsName(_animText))//애니메이션 끝날때
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
        return false;//진행중
    }

}
