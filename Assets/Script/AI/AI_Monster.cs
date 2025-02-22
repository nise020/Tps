using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public partial class AiMonster : AiBase
{
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
    bool attackCheck = false;
    List<Vector3> searchPosObj;
    Vector3 startPos = Vector3.zero;
    
    int moveNumber = 0;
    //Monster_Skill SKILL = new Monster_Skill();

    //private eMobType MOBTYPE;
    //Monster Monster;
    //FSM
    //캐릭터에서 AI를 호출할 필요

    
    public override void State(ref AI _aIState)
    {
        switch (aIState)
        {
            case AI.Create:
                Create();
                break;
            case AI.Search:
                Search();
                break;
            case AI.Move:
                Move();
                break;
            case AI.Attack:
                Attack();
                break;
            case AI.Reset:
                Reset();
                break;
        }
        _aIState = aIState;
    }
    protected override void Create()//생성
    {
        animator = MONSTER.mobAnimator;

        searchPosObj = MONSTER.movePos;//List

        creatTab = Shared.BattelMgr.creatTab;
        eyePos = MONSTER.eyeObj.transform;
        startPos = MONSTER.gameObject.transform.position;
        aIState = AI.Search;
    }

    float viewDistance = 5f;
    float viewAngle = 60f;   

    protected override void Search()//공격할 대상 찾기
    {
        if (searchAnim == false)
        {
            searchAnim = true;
            animator.SetInteger("Search", 1);
        }

        MONSTER.readySearch(ref searching);//서치
        //Debug.Log($"Search");
        Collider[] hitColliders = Physics.OverlapSphere(MONSTER.gameObject.transform.position, viewDistance);

        if (hitColliders == null) { return; }

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))
            {
                Vector3 directionToTarget = (col.transform.position - MONSTER.gameObject.transform.position).normalized;
                float angleBetween = Vector3.Angle(MONSTER.gameObject.transform.forward, directionToTarget);

                if (angleBetween < viewAngle * 0.5f) // 시야각 내에 있는지 확인
                {
                    moveAnim = false;
                    targetPos = col.transform.position;//Vector3
                    aIState = AI.Attack;

                    searchAnim = false;//clear
                }
            }
        }

    }
    //Vector3

    //DIstance: 거리 구하기
    //Dot 내적: 방향, 밀치기
    //Cross 외적: 빛 반사
    //Normalize 정규화: 방향만 구하고 값은 1로 낮춘다

    //행렬
    protected override void Move()//이동
    {
        //MONSTER.readySearch(ref searching);
    }

    protected override void Attack()//공격
    {
        animator.SetInteger("Attack", 1);
        Debug.Log($"Attack");
        //Pattern();

        if (moveAnim == false)//Animation Event
        {
            moveAnim = true;
            animator.SetInteger("Search", 0);// Serching X
            animator.SetInteger("Walk", 0);
            if (MobType == MonsterType.Sphere) 
            {
                animator.SetInteger("Close", 1);
            }
        }

        Pattern(MobType);

    }
    public void Pattern(MonsterType _enum)
    {
        if (_enum == MonsterType.Sphere)//구체 일 경우
        {
            if (moveing == false) return;
            if (moveing == true)//Animation Event
            {
                MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
            }
            //계속 이동하는 문제 있음
            Vector3 myPos = MONSTER.gameObject.transform.position;
            float distanse = Vector3.Distance(myPos, targetPos);
            float targetvalue = MONSTER.attackDistanse;//사정거리

            if (distanse < 1.0f)
            {
                animator.SetInteger("Close", 0);
                animator.SetInteger("AttackDilray", 1);
                moveing = false;
                aIState = AI.Reset;
            }

        }
        else if (_enum == MonsterType.Spider)//거미일 경우 
        {
            if (attackCheck == false)
            {
                GameObject go = Delivery.Instantiator(MONSTER.MobGrenade, eyePos.position, Quaternion.identity, creatTab);
                //리소스 재활용
                MONSTER.granaidAttack(MONSTER.gameObject.transform.position, targetPos, go);
                animator.SetInteger("Attack", 0);
                attackCheck = true;
            }


            //추가적으로 던져야 하기 떄문에 AddForce를 추가해야함
            //Instantiator가 아닌 SetActive를 사용해서 리소스를 재사용 해야함
            //aIState = AI.Reset;
        }
        else if (_enum == MonsterType.Dron)//드론일 경우 
        {
            MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
            animator.SetInteger("Attack", 0);
            aIState = AI.Reset;
        }
    }



    protected override void Reset()//사이클 끝(보통 다시 공격 대상 탐색)
    {
        Debug.Log($"Reset");
        moveing = false;
        attackOn = true;
        targetNumber = 0;
        aIState = AI.Search;

    }

    
}
