using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    //SkillStrategy SkillStrategy = new SkillStrategy();
    protected override void Start()
    {
        playerType = PlayerType.Warrior;
        viewcam = Shared.BattelManager.MOVECAM;
        STATE.init(charactor);
        stateInIt();
        rigid = GetComponent<Rigidbody>();
        playerAnim = GetComponentInChildren<Animator>();
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableManager.Character.Get(1);
        Name = info.Img;
        //gun = GetComponentInChildren<Gun>();
    }

    private void Update()
    {
        runcheck();
        if ((mouseClick))
        {
            nomalAttack();
        }
        shitdownCheak();//¾É±â
        ////Time.timeScale = 0;//Faraim Speed up,Down
    }
    protected override void shitdownCheak()
    {
        base.shitdownCheak();
    }
}
