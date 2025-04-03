using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class Gunner : Player
{
    //SkillStrategy SkillStrategy = new SkillStrategy();
    
    protected override void Start()
    {
        base.Start();
        playerType = PlayerjobEnum.Gunner;
        playerControll = PlayerControllState.On; 
        gun = GetComponentInChildren<Gun>();
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableManager.Character.Get(1);
        //Name = info.Img;
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
        skillAttack(playerType);

        ////Time.timeScale = 0;//Faraim Speed up,Down
        ///

        if (charctorState == CharctorStateEnum.Player)
        {
            if ((mouseClick))
            {
                attack(charctorState);
            }
            else if (playerType == PlayerjobEnum.Gunner)//���� �ڵ�����
            {
                if (mouseClickUp || gun.nowbullet <= 0)
                {
                    viewcam.cameraShakeAnim(false);
                    playerAnim.SetInteger("Attack", 0);
                }
            }
            reloding(playerType);//���ε�
            shitdownCheak();//�ɱ�
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
}
