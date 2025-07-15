using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Character
{
    protected virtual void Awake()
    {
        //objType = ObjectType.Player;
    }
    //protected GameObject WeaponObj;
    protected override void Start()
    {      
        base.Start();
        playerAnimator = GetComponent<Animator>();

        FindWeaponObject();

        viewcam = GetComponentInChildren<PlayerCamera>();

        if (playerStateData.ModeState == PlayerModeState.Npc)
        {
            viewcam.gameObject.SetActive(false);
        }

        PLAYERAI.init(this);//FSM
        slotinit();

        rigid = GetComponent<Rigidbody>();//Kinematic Controll

        skillStrategy.PlayerInit(this);
        SkillEffectSystem1 = CreatSkill(SkillEffectObj1, SkillParentObj1);
        SkillEffectSystem2 = CreatSkill(SkillEffectObj2, SkillParentObj2);

        AttackEvent += AiTagetUpdate;

        battelTrigger = MainWeaponObj.GetComponent<BattelTrigger>();
        if (battelTrigger) 
        {
            battelTriggerCol = battelTrigger.gameObject.GetComponent<BoxCollider>();
            battelTriggerCol.enabled = false;
        }

    }

    private void FixedUpdate()
    {
        if (playerStateData.ModeState != PlayerModeState.Player)
        {
            PLAYERAI.State();
        }
    }

    public void init(out PlayerCamera _camera) 
    {
        _camera = viewcam; 
    }

    public void GetItem(Item _item) 
    {
        Shared.UiManager.UI_INVENTORY.itemLists.Add(_item);//이건 아이템 드랍시
    }
}
