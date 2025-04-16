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
        if (charctorState == CharctorStateEnum.Player)
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

    protected override void shitdownCheak()
    {
        base.shitdownCheak();
    }
}
