using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Cinemachine.CinemachineStoryboard;
using static UIWidget;

public partial class Gunner : Player
{
    //SkillStrategy SkillStrategy = new SkillStrategy();
    protected override void Start()
    {
        base.Start();
        skillStrategy.PlayerInit(this);
        skillStrategy.WeaponInit(gun);
    }
    protected override void skillAttack() 
    {
        base.skillAttack();
    }
    private void Update()
    {
        runcheck();
        skillAttack();
        if ((mouseClick))
        {
            nomalAttack();
        }
        else if (playerType == PlayerType.Gunner)//���� �ڵ�����
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
