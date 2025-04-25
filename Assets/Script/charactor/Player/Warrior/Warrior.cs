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
        //HandObj = FindSkinBodyTypeObject(BodyType.RightHand);
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
            transform.rotation = new Quaternion();
            inputrocessing();
        }
        else 
        {
            return;
        }
        
    }
    
    protected override void shitdownCheak()
    {
        base.shitdownCheak();
    }
    protected override void FindWeaponObject(LayerName _name)
    {
        SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        int value = LayerMask.NameToLayer(_name.ToString());
        foreach (var skinObj in skin)
        {
            if (skinObj.gameObject.layer == value)
            {
                weaponObj = skinObj.rootBone.gameObject;
                weaponOriginalPos = weaponObj.transform.localPosition;
                break;
            }
        }
    }
}
