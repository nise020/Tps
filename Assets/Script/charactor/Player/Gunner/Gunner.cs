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
        else if (playerType == PlayerEnum.Gunner)//���� �ڵ�����
        {
            if (mouseClickUp || gun.nowbullet <= 0)
            {
                viewcam.cameraShakeAnim(false);
                playerAnim.SetInteger("Attack", 0);
            }
        }
        reloding(playerType);//���ε�
        shitdownCheak();//�ɱ�
        ////Time.timeScale = 0;//Faraim Speed up,Down
    }
    protected override void shitdownCheak()
    {
       base.shitdownCheak();
    }
}
