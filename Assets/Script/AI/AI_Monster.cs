using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
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
    public bool searchingOnOff = false;
    bool attackCheck = false;
    List<Vector3> searchPosObj;
    Vector3 startPos = Vector3.zero;
    
    int moveNumber = 0;
    public SearchState searching = SearchState.Move;



    //Monster_Skill SKILL = new Monster_Skill();

    //private eMobType MOBTYPE;
    //Monster Monster;
    //FSM
    //캐릭터에서 AI를 호출할 필요


    public override void State(ref AiState _aIState)
    {
        switch (aIState)
        {
            case AiState.Create:
                Create();
                break;
            case AiState.Search:
                Search();
                break;
            case AiState.Move:
                Move();
                break;
            case AiState.Attack:
                Attack();
                break;
            case AiState.Reset:
                Reset();
                break;
        }
        _aIState = aIState;
    }
    protected override void Create()//생성
    {
        animator = MONSTER.mobAnimator;

        searchPosObj = MONSTER.movePos;//List

        creatTab = Shared.BattelManager.creatTab;
        eyePos = MONSTER.eyeObj.transform;
        startPos = MONSTER.gameObject.transform.position;
        aIState = AiState.Search;
    }

    float viewDistance = 10f;
    float viewAngle = 60f;
    public float sphereRadius = 1.0f; // 구 반지름
    public LayerMask playerLayer;
    protected override void Search()//공격할 대상 찾기
    {
        Debug.Log($"Search");
        if (searchAnim == false)
        {
            searchAnim = true;
            if (MobType != MonsterType.Dron)
            {
                animator.SetInteger("Search", 1);
            }
            searching = SearchState.Stop;
        }
        //현제 에러가 나는 이유는 PointMoveAnim에서 OnOff를 제어 하고 있지만
        //Walk 애니메이션이 상시 실행 되고 있는 부분이다

        //애니메이션 끝나는 부분에 SearchState.Stop을 걸어보기

        //if (searching == SearchState.Move) 
        //{
        //    animator.SetInteger("Search", 0);
        //    animator.SetInteger("Walk", 1);
        //    MONSTER.NextPoint(ref searchingOnOff);//서치
        //}
        //else if (searching == SearchState.Stop) 
        //{
        //    animator.SetInteger("Search", 1);
        //    animator.SetInteger("Walk", 0);
        //}



        RaycastHit hit;

        Vector3 position = MONSTER.gameObject.transform.position;
        Vector3 direction = MONSTER.gameObject.transform.forward;

        if (Physics.SphereCast(position, sphereRadius, direction, out hit, viewDistance)) 
        {
            int layer = hit.collider.gameObject.layer;
            if (layer != Delivery.LayerNameEnum(LayerTag.Player)) { return; }

            if (layer == Delivery.LayerNameEnum(LayerTag.Player))
            {
                moveAnim = false;
                animator.SetInteger("Search", 0);
                aIState = AiState.Attack;
                targetPos = hit.collider.gameObject.transform.position;//Vector3
                searchAnim = false;//clear
            }
        }
        MONSTER.NextPoint(ref searchingOnOff);//서치
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
        //animator.SetInteger("Attack", 1);
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
            animator.SetInteger("Attack", 1);
        }

        //ONSTER.Pattern(MobType);
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

            if (distanse <= 1.0f)
            {
                moveing = false;
                aIState = AiState.Reset;
            }

        }
        else if (_enum == MonsterType.Spider)//거미일 경우 
        {
            if (attackCheck == false)
            {
                GameObject go = Delivery.Instantiator(MONSTER.MobGrenade, eyePos.position, Quaternion.identity, creatTab);
                //리소스 재활용 해야 하기 떄문에 수정필요
                MONSTER.granaidAttack(MONSTER.gameObject.transform.position, targetPos, go);

                aIState = AiState.Reset;
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
            aIState = AiState.Reset;
        }
    }



    protected override void Reset()//사이클 끝(보통 다시 공격 대상 탐색)
    {
        Debug.Log($"Reset");
        moveing = false;
        attackOn = true;
        targetNumber = 0;
        aIState = AiState.Search;
    }

    
}
