using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public partial class AiMonster : AiBase
{
    List<Player> target;
    Transform creatTab;
    bool attackOn = true;
    int targetNumber;//공격할 목표의 번호
    float timer = 0.0f;
    float time = 5.0f;
    //MobAnim mobAnimState = MobAnim.Idle;
    Transform eyePos;
    Vector3 targetPos;
    Animator animator;
    bool searchAnim = false;
    bool moveAnim = false;
    public bool moveing = false;
    public bool searching = false;
    bool moveChack = false;
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

    public override void State(ref eAI _aIState)
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
        _aIState = aIState;
    }
    protected override void Create()//생성
    {
        animator = MONSTER.mobAnimator;

        searchPosObj = MONSTER.movePosObj;//List

        creatTab = Shared.BattelMgr.creatTab;
        eyePos = MONSTER.eyeObj.transform;
        startPos = MONSTER.gameObject.transform.position;
        aIState = eAI.Search;
    }

    protected override void Search()//공격할 대상 찾기
    {
        if (searchAnim == false)
        {
            searchAnim = true;
            animator.SetInteger("Search", 1);
        }
        if (Physics.Raycast(eyePos.position,
           eyePos.transform.forward, out RaycastHit hit))//target Serching
                                                         //플레이어가 걸렸을때
        {
            int layer = hit.collider.gameObject.layer;
            string name = LayerMask.LayerToName(layer);

            if (name == "Player")
            {
                moveAnim = false;
                targetPos = hit.point;//Vector3
                aIState = eAI.Attack;//next Pattern

                searchAnim = false;//clear

            }
        }
        MONSTER.readySearch(ref searching);
    }

    protected override void Move()//이동
    {
        Debug.Log($"Move");
    }

    protected override void Attack()//공격
    {
        animator.SetInteger("Attack", 1);
        Debug.Log($"Attack");
        //Pattern();

        if (moveAnim == false)
        {
            moveAnim = true;
            animator.SetInteger("Search", 0);// Serching X
            animator.SetInteger("Walk", 0);
            animator.SetInteger("Close", 1);
        }
        if (moveing == true)
        {
            Vector3 myPos = MONSTER.gameObject.transform.position;
            float speed = MONSTER.moveSpeed;
            MONSTER.gameObject.transform.position += (targetPos - myPos).normalized * speed * Time.deltaTime;

            float distanse = Vector3.Distance(myPos, targetPos);
            float targetvalue = MONSTER.attackDistanse;//사정거리
            if (distanse < targetvalue)
            {
                animator.SetInteger("Close", 0);
                animator.SetInteger("AttackDilray", 1);
                aIState = eAI.Reset;
            }
        }

        //if (nextOn_Off == true)
        //{
        //    animator.SetInteger("AttackDilray", 1);
        //    nextOn_Off = false;
        //    aIState = eAI.Reset;
        //}
    }
    protected override void Reset()//사이클 끝(보통 다시 공격 대상 탐색)
    {
        Debug.Log($"Reset");
        attackOn = true;
        targetNumber = 0;
        aIState = eAI.Search;
    }


}
