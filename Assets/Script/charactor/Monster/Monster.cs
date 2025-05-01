using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public partial class Monster : Charactor
{
    protected Item ITEM;
    [SerializeField] Item viewItem;
    protected AiMonster AI = new AiMonster();
    protected Skill_Monster SKILL = new Skill_Monster();
    
    public int mobKey = 0;

    protected bool viewHpBar = false;
    protected override void Start() 
    {
        base.Start();
        mobAnimator = GetComponent<Animator>();
        creatTabObj = Shared.GameManager.creatTab;//¿ÀºêÁ§Æ® »ý¼º ÅÇ(ex.ÃÑ¾Ë)
        AI.init(this, SKILL);
        AI.Type(monsterType);
        STATUS.MonsterState(monsterType);
        stateInIt();
    }
    protected virtual void FixedUpdate()
    {
        if (AI == null) { return; }
        AI.State();
    }
    private void LateUpdate()
    {
        cameraInMonsterCheck();
    }
    protected override void FindWeaponObject(LayerName _name)
    {
        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
        int value = LayerMask.NameToLayer(_name.ToString());
        foreach (var skinObj in mesh)
        {
            if (skinObj.gameObject.layer == value)
            {
                Transform weapontrs = skinObj.transform.parent;

                weaponObj = weapontrs.gameObject;

                weaponOriginalPos = weaponObj.transform.localPosition;
                break;
            }
        }
    }


    protected void cameraInMonsterCheck()
    {
        Player player = Shared.GameManager.PlayerLoad();
        Camera camera = player.GetComponentInChildren<Camera>();

        Vector3 viewportPos = camera.WorldToViewportPoint(gameObject.transform.position);

        bool isVisible = (viewportPos.z > 0 && viewportPos.x > 0 && 
                          viewportPos.x < 1 && viewportPos.y > 0 && 
                          viewportPos.y < 1);
        if (isVisible)
        {
            hbBarCheck(true);
        }
        else
        {
            hbBarCheck(false);
        }
    }
    
    public void KeyUpdate(int _key)
    {
        mobKey = _key;
    }
    public void ItemUpdate(Item _item) 
    {
        ITEM = _item;
        viewItem = ITEM;
    }
}
