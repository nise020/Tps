using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    //SkillStrategy SkillStrategy = new SkillStrategy();
    protected override void Start()
    {
        base.Start();
        playerType = PlayerjobEnum.Warrior;
        weaponState = WeaponState.Sword_Off;
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableManager.Character.Get(0);
        Name = info.Img;
        skillStrategy.PlayerInit(this);
        //skillStrategy.WeaponInit(gun);
    }
    protected override void skillAttack(PlayerjobEnum _type)
    {
        base.skillAttack(_type);
    }
    private void Update()
    {
        runcheck(RunCheck);
        if (charctorState == CharctorStateEnum.Player)
        {
            if ((mouseClick))
            {
                attack(charctorState);
            }
            shitdownCheak();//�ɱ�
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
