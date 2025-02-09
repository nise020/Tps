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
            case eMobType.Spider:
                SKILL.NomalAttack(ref nextOn_Off, targetNumber, MONSTER.MobBullet, target, MONSTER.AttackArm.transform.position, creatTab);
                break;
            case eMobType.Sphere:
                SKILL.JumpSkill(targetNumber, target, ref attackOn,
                    MONSTER.gameObject.transform.position, MONSTER.mobRigid);
                break;
            case eMobType.Flying:
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

        Debug.Log($"Search");

        if (Physics.Raycast(eyePos.position,
           eyePos.transform.forward, out RaycastHit hit))//target Serching
                                                         //플레이어가 걸렸을때
        {
            int layer = hit.collider.gameObject.layer;
            string name = LayerMask.LayerToName(layer);

            if (name == "Player")
            {
                Debug.Log($"{name}");
                moveAnim = false;
                targetPos = hit.point;//Vector3
                //aIState = eAI.Move;//next Pattern
                aIState = eAI.Attack;

                searchAnim = false;//clear

            }
        }
        MONSTER.readySearch(ref searching);
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

        if (moveAnim == false)
        {
            moveAnim = true;
            animator.SetInteger("Search", 0);// Serching X
            animator.SetInteger("Walk", 0);
            if (MobType == eMobType.Sphere) 
            {
                animator.SetInteger("Close", 1);
            }
        }
        switch (MobType) 
        {
            case eMobType.Sphere:
                break;
            case eMobType.Spider:

                break;
            case eMobType.Flying:

                break;
        }
        if (MobType == eMobType.Sphere)//구체 일 경우
        {
            if (moveing == false) return;
            Vector3 myPos = MONSTER.gameObject.transform.position;
            float speed = MONSTER.moveSpeed;
            MONSTER.gameObject.transform.position += (targetPos - myPos).normalized * speed * Time.deltaTime;
            //계속 이동하는 문제 있음

            float distanse = Vector3.Distance(myPos, targetPos);
            float targetvalue = MONSTER.attackDistanse;//사정거리
            if (distanse < targetvalue)
            {
                if (MobType == eMobType.Sphere)
                {
                    animator.SetInteger("Close", 0);
                    animator.SetInteger("AttackDilray", 1);
                }
                aIState = eAI.Reset;
            }
        }
        else if (MobType == eMobType.Spider)//거미일 경우 
        {
            GameObject go = Delivery.Instantiator(MONSTER.MobGrenade, eyePos.position, Quaternion.identity,creatTab);
            animator.SetInteger("Attack", 0);
            //추가적으로 던져야 하기 떄문에 AddForce를 추가해야함
            //Instantiator가 아닌 SetActive를 사용해서 리소스를 재사용 해야함
            aIState = eAI.Reset;
        }

    }
    protected override void Reset()//사이클 끝(보통 다시 공격 대상 탐색)
    {
        Debug.Log($"Reset");
        moveing = false;
        attackOn = true;
        targetNumber = 0;
        aIState = eAI.Search;

    }

    
}
