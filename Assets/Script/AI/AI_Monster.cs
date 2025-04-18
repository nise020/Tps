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


    public override void State(ref MonsterAiState _aIState)
    {
        switch (aIState)
        {
            case MonsterAiState.Create:
                Create();
                break;
            case MonsterAiState.Search:
                Search();
                break;
            case MonsterAiState.Move:
                Move();
                break;
            case MonsterAiState.Attack:
                Attack();
                break;
            case MonsterAiState.Reset:
                Reset();
                break;
        }
        _aIState = aIState;
    }
    protected override void Create()//생성
    {
        MONSTER.init();
        //animator = MONSTER.mobAnimator;

        //searchPosObj = MONSTER.movePos;//List

        //creatTab = Shared.MonsterManager.creatTab;
        //eyePos = MONSTER.eyeObj.transform;
        //startPos = MONSTER.gameObject.transform.position;
        aIState = MonsterAiState.Search;
    }

    float viewDistance = 10f;
    float viewAngle = 60f;
    public float sphereRadius = 1.0f; // 구 반지름
    public LayerMask playerLayer;
    protected override void Search()//공격할 대상 찾기
    {
        MONSTER.MovePoint();
        if (MONSTER.TargetSearch() == true)
        {
            aIState = MonsterAiState.Attack;
        }
    }
 
    //행렬
    protected override void Move()//이동
    {
        MONSTER.TargetAttackMove();
    }

    protected override void Attack()//공격
    {
        MONSTER.Attack();

        aIState = MonsterAiState.Reset;
    }
    protected override void Reset()//사이클 끝(보통 다시 공격 대상 탐색)
    {
        aIState = MonsterAiState.Search;
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
                aIState = MonsterAiState.Reset;
            }

        }
        else if (_enum == MonsterType.Spider)//거미일 경우 
        {
            if (attackCheck == false)
            {
                GameObject go = Delivery.Instantiator(MONSTER.MobGrenade, eyePos.position, Quaternion.identity, creatTab);
                //리소스 재활용 해야 하기 떄문에 수정필요
                MONSTER.granaidAttack(MONSTER.gameObject.transform.position, targetPos, go);

                aIState = MonsterAiState.Reset;
                attackCheck = true;
            }
            //추가적으로 던져야 하기 떄문에 AddForce를 추가해야함
            //Instantiator가 아닌 SetActive를 사용해서 리소스를 재사용 해야함
            //aIState = AI.Reset;
        }
        else if (_enum == MonsterType.Dron)//드론일 경우 
        {
            MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
            animator.SetInteger(MonsterAnimParameters.Attack.ToString(), 0);
            aIState = MonsterAiState.Reset;
        }
    }




    
}
