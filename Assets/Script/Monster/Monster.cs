using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    [SerializeField] MonsterType monster;
    protected override void Start()//Actor에 이동
    {
        base.Start();
        STATE.monsterState(monster);
        mobAnimator = GetComponent<Animator>();
        //NowHp();
        creatTabObj = Shared.BattelMgr.creatTab;//오브젝트 생성 탭(ex.총알)
        mobRigid = GetComponent<Rigidbody>();
        mobColl = GetComponent<Collider>();
        boxColl = GetComponentInChildren<BoxCollider>();//발
        AI.init(this, SKILL);
        AI.Type(eType);
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
