using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    //SkillStrategy SkillStrategy = new SkillStrategy();
    protected override void Start()
    {
        playerType = PlayerEnum.Warrior;
        weaponState = WeaponState.Sword_Off;
        viewcam = GetComponentInChildren<MoveCamera>();
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableManager.Character.Get(0);
        Name = info.Img;
        skillStrategy.PlayerInit(this);
        //skillStrategy.WeaponInit(gun);
        base.Start();
    }
    protected override void skillAttack(PlayerEnum _type)
    {
        base.skillAttack(_type);
    }
    private void Update()
    {
        runcheck(RunCheck);
        if ((mouseClick))
        {
            nomalAttack();
        }
        shitdownCheak();//¾É±â
        //playerSkillAttack(playerType);
        ////Time.timeScale = 0;//Faraim Speed up,Down
    }

    protected override void shitdownCheak()
    {
        base.shitdownCheak();
    }
}
