using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class Gunner : Player
{
    //SkillStrategy SkillStrategy = new SkillStrategy();
    protected override void Start()
    {
        playerType = PlayerEnum.Gunner;
        playerControll = PlayerControll.On;

        viewcam = GetComponentInChildren<MoveCamera>();
        gun = GetComponentInChildren<Gun>();
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableManager.Character.Get(1);
        //Name = info.Img;
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
        skillAttack(playerType);
        if ((mouseClick))
        {
            nomalAttack();
        }
        else if (playerType == PlayerEnum.Gunner)//떼면 자동으로
        {
            if (mouseClickUp || gun.nowbullet <= 0)
            {
                viewcam.cameraShakeAnim(false);
                playerAnim.SetInteger("Attack", 0);
            }
        }
        reloding(playerType);//리로드
        shitdownCheak();//앉기
        ////Time.timeScale = 0;//Faraim Speed up,Down
    }
    protected override void shitdownCheak()
    {
       base.shitdownCheak();
    }
}
