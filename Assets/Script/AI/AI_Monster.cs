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
    int targetNumber;//������ ��ǥ�� ��ȣ
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
    //ĳ���Ϳ��� AI�� ȣ���� �ʿ�


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
    protected override void Create()//����
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
    public float sphereRadius = 1.0f; // �� ������
    public LayerMask playerLayer;
    protected override void Search()//������ ��� ã��
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
        //���� ������ ���� ������ PointMoveAnim���� OnOff�� ���� �ϰ� ������
        //Walk �ִϸ��̼��� ��� ���� �ǰ� �ִ� �κ��̴�

        //�ִϸ��̼� ������ �κп� SearchState.Stop�� �ɾ��

        //if (searching == SearchState.Move) 
        //{
        //    animator.SetInteger("Search", 0);
        //    animator.SetInteger("Walk", 1);
        //    MONSTER.NextPoint(ref searchingOnOff);//��ġ
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
        MONSTER.NextPoint(ref searchingOnOff);//��ġ
    }
    //Vector3

    //DIstance: �Ÿ� ���ϱ�
    //Dot ����: ����, ��ġ��
    //Cross ����: �� �ݻ�
    //Normalize ����ȭ: ���⸸ ���ϰ� ���� 1�� �����

    //���
    protected override void Move()//�̵�
    {
        //MONSTER.readySearch(ref searching);
    }

    protected override void Attack()//����
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
        if (_enum == MonsterType.Sphere)//��ü �� ���
        {
            if (moveing == false) return;
            if (moveing == true)//Animation Event
            {
                MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
            }
            //��� �̵��ϴ� ���� ����
            Vector3 myPos = MONSTER.gameObject.transform.position;
            float distanse = Vector3.Distance(myPos, targetPos);
            float targetvalue = MONSTER.attackDistanse;//�����Ÿ�

            if (distanse <= 1.0f)
            {
                moveing = false;
                aIState = AiState.Reset;
            }

        }
        else if (_enum == MonsterType.Spider)//�Ź��� ��� 
        {
            if (attackCheck == false)
            {
                GameObject go = Delivery.Instantiator(MONSTER.MobGrenade, eyePos.position, Quaternion.identity, creatTab);
                //���ҽ� ��Ȱ�� �ؾ� �ϱ� ������ �����ʿ�
                MONSTER.granaidAttack(MONSTER.gameObject.transform.position, targetPos, go);

                aIState = AiState.Reset;
                attackCheck = true;
            }
            //�߰������� ������ �ϱ� ������ AddForce�� �߰��ؾ���
            //Instantiator�� �ƴ� SetActive�� ����ؼ� ���ҽ��� ���� �ؾ���
            //aIState = AI.Reset;
        }
        else if (_enum == MonsterType.Dron)//����� ��� 
        {
            MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
            animator.SetInteger("Attack", 0);
            aIState = AiState.Reset;
        }
    }



    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
    {
        Debug.Log($"Reset");
        moveing = false;
        attackOn = true;
        targetNumber = 0;
        aIState = AiState.Search;
    }

    
}
