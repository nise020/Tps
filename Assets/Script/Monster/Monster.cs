using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    [SerializeField] MonsterType monster;
    Condition condition = Condition.health;//��������
    protected override void Start()//Actor�� �̵�
    {
        base.Start();
        mobAnimator = GetComponent<Animator>();
        //NowHp();
        creatTabObj = Shared.BattelMgr.creatTab;//������Ʈ ���� ��(ex.�Ѿ�)
        mobRigid = GetComponent<Rigidbody>();
        mobColl = GetComponent<Collider>();
        if (mobColl == null) 
        {
            mobColl = GetComponentInChildren<Collider>();
        }
        boxColl = GetComponentInChildren<BoxCollider>();//��
        AI.init(this, SKILL);
        AI.Type(eType);

        STATE.MonsterState(monster);
        inIt();
    }
    private void FixedUpdate()
    {
        if (AI == null) { return; }
        AI.State(ref aIState);
        CameraInMonsterCheck();
    }
    protected HpBar HPBAR = new HpBar();
    public void HpInIt(HpBar _hpBar)
    {
        HPBAR = _hpBar;
    }
    public void CameraInMonsterCheck()
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(gameObject.transform.position);

        bool isVisible = (viewportPos.z > 0 && viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1);
        if (isVisible)
        {
            HPBAR.gameObject.SetActive(true);
        }
        else
        {
            HPBAR.gameObject.SetActive(false);
        }
    }

    public float GetMonsterHeight()
    {
        return GetComponent<Collider>().bounds.size.y;
    }
}
