using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    [SerializeField] GameObject scabbard;

    int scabbardCount = 0;
    int scabbardMaxCount = 2;
    
   
    public void GetSword()//AnimationEvent
    {
        GameObject go = weaponObj.gameObject;
        go.transform.SetParent(weaponHandObject.gameObject.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        PlayerStateData.WeaponState = PlayerWeaponState.Sword_On;

        //CreatSkill(SkillEffectObj1, SkillParentObj1);
        //CreatSkill(SkillEffectObj2, SkillParentObj2);

    }
   
    public void ClearlSword(int _value)//AnimationEvent
    {
        if (scabbardMaxCount == scabbardCount)
        {
            playerAnimtor.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 0);
            scabbardCount = 0;
        }
        else 
        {
            scabbardCount += _value;
        }
            
    }
    public void ScabbardInTheSword() //AnimationEvent
    {
        GameObject go = weaponObj.gameObject;
        go.transform.SetParent(scabbard.gameObject.transform);
        go.transform.localPosition = weaponOriginalPos;
        go.transform.localRotation = Quaternion.identity;
        PlayerStateData.WeaponState = PlayerWeaponState.Sword_Off;
    }
    protected override void walkAnim(RunState _runState, Vector3 _pos)
    {
        if (_runState == RunState.Walk)
        {
            if (PlayerStateData.WalkState == PlayerWalkState.Walk_On) { return; }

            PlayerStateData.WalkState = PlayerWalkState.Walk_On;
            playerAnimtor.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
        }
        else if (_runState == RunState.Run)
        {
            if (PlayerStateData.RunState == PlayerRunState.Run_On) { return; }

            PlayerStateData.RunState = PlayerRunState.Run_On;
            playerAnimtor.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
        }
    }
    
    protected override void clearWalkAnimation(PlayerType _type)
    {
        base.clearWalkAnimation(_type);
    }
}
