using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    //SkillStrategy SkillStrategy = new SkillStrategy();
    GameObject Weapon;
    protected override void Start()
    {
        base.Start();
        skillStrategy.PlayerInit(this);
        FindWeaponObject(LayerName.Weapon);
        weaponState = WeaponState.Sword_Off;
        //playerType = CharactorJobEnum.Warrior;
        //charctorState = CharctorStateEnum.Npc;
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableManager.Character.Get(0);
        //Name = info.Img;
        //skillStrategy.WeaponInit(gun);
    }

    private void Update()
    {
        //inputrocessing();
        //move(charctorState);
        //runcheck(RunCheck);
        if (charctorState != CharctorStateEnum.Npc)
        {
            inputrocessing();
        }
        else 
        {
            return;
        }
        //playerSkillAttack(playerType);
        ////Time.timeScale = 0;//Faraim Speed up,Down
    }
    protected void FindWeaponObject(LayerName _name)
    {
        //GameObject go = null;
        SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        int value = LayerMask.NameToLayer(_name.ToString());
        foreach (var skinObj in skin)
        {
            if (skinObj.gameObject.layer == value)
            {
                weapon = skinObj.rootBone.gameObject;
                weaponOriginalPos = weapon.transform.localPosition;
                break;
            }
        }
    }
    protected override void shitdownCheak()
    {
        base.shitdownCheak();
    }
}
