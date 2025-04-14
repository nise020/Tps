using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    //SkillStrategy SkillStrategy = new SkillStrategy();
    
    protected override void Start()
    {
        base.Start();
        //playerType = CharactorJobEnum.Warrior;
        //charctorState = CharctorStateEnum.Npc;
        weaponState = WeaponState.Sword_Off;
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableManager.Character.Get(0);
        //Name = info.Img;
        skillStrategy.PlayerInit(this);
        //skillStrategy.WeaponInit(gun);
    }
    protected override void skillAttack1(CharactorJobEnum _type)
    {
        //base.skillAttack(_type);
    }
    private void Update()
    {
        //inputrocessing();
        //move(charctorState);
        //runcheck(RunCheck);
        if (charctorState == CharctorStateEnum.Player)
        {
            inputrocessing();
            if ((mouseClick))
            {
                //attack(charctorState);
            }
            //shitdownCheak();//¾É±â
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
