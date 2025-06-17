using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossMonster : Monster
{
    [Header("Boss/Animation")]
    [SerializeField] GameObject scabbard;
    int scabbardCount = 0;
    int scabbardMaxCount = 4;
    bool isChecking = true;
    
    public void RangCheckStart(string _Range) //AnimationEvent
    {
        if (_Range == "Front")
        {
            StartCoroutine(CheckThrustHit());
        }
        else
        {
            StartCoroutine(CheckSlashHit());
        }
    }

    public void RangCheckEnd()//AnimationEvent
    {
        isChecking = false;
    }

    public void TurnEnd()//AnimationEvent
    {
        monsterAnimator.SetInteger(MonsterAnimParameters.Trun.ToString(), 0);
        charactorModelTrs.Rotate(0, RootTransform.rotation.y, 0);
    }
}
