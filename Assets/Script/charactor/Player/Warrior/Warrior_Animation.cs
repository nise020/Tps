using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    [SerializeField] GameObject scabbard;

    int scabbardCount = 0;
    int scabbardMaxCount = 2;

    //protected override void inPutCameraAnimation(bool _check)
    //{
    //    viewcam.cameraShakeAnim(_check);
    //    //if (_type == MouseInputType.Release)
    //    //{
    //    //    viewcam.cameraShakeAnim(_check);
    //    //}
    //    //else if (_type == MouseInputType.Hold)
    //    //{
    //    //    viewcam.cameraShakeAnim(_check);
    //    //}
    //}

    public void RangeCheak() 
    {
        Vector3 weaponPos = new Vector3();
        if (firstSkillCheck == SkillRunning.SkillOn)
        {
            weaponPos = SkillParentObj1.transform.position;
        }
        else if (secondSkillCheck == SkillRunning.SkillOn) 
        {
            weaponPos = SkillParentObj2.transform.position;
        }
        else 
        {
            weaponPos = weaponObj.transform.position;
        }

        List <Monster> monsetrPos = Shared.MonsterManager.MonsterList;

        for (int iNum = 0; iNum < monsetrPos.Count; iNum++)
        {
            Transform body = monsetrPos[iNum].BodyObjectLoad();
            float dist = Vector3.Distance(weaponPos, body.position);

            if (dist < 3.5f)
            {
                Shared.BattelManager.DamageCheck(this, monsetrPos[iNum]);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (weaponObj != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(SkillParentObj1.transform.position, 3.5f);
        }
    }
    public void SkillEffectOff(int _value) 
    {
        if (firstSkillCheck == SkillRunning.SkillOn)
        {
            SkillAnimation(SkillType.Skill1, false);
            SkillParentObj1.SetActive(false);
            SkillEffectSystem1.Pause();
        }
        else if (secondSkillCheck == SkillRunning.SkillOn) 
        {
            SkillAnimation(SkillType.Skill2, false);
            SkillParentObj1.SetActive(false);
            SkillEffectSystem2.Pause();
        }
        else
        {
            attackAnimation(AttackState.AttackOff);
        }
        weaponState = WeaponState.Sword_Off;
        scabbardCount = 0;

    }
    public void GetSword()//AnimationEvent
    {
        GameObject go = weaponObj.gameObject;
        go.transform.SetParent(HandObj.gameObject.transform);
        go.transform.localPosition = Vector3.zero;
        weaponState = WeaponState.Sword_On;

        //CreatSkill(SkillEffectObj1, SkillParentObj1);
        //CreatSkill(SkillEffectObj2, SkillParentObj2);

    }
   
    public void ClearlSword(int _value)//AnimationEvent
    {
        if (scabbardMaxCount == scabbardCount)
        {
            playerAnim.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 0);
            scabbardCount = 0;
        }
        else 
        {
            scabbardCount += _value;
        }
            
    }
    public void ScabbardInTheSword() 
    {
        GameObject go = weaponObj.gameObject;
        go.transform.SetParent(scabbard.gameObject.transform);
        go.transform.localPosition = weaponOriginalPos;
        weaponState = WeaponState.Sword_Off;
    }
    protected override void walkAnim(RunState _runState, Vector3 _pos)
    {
        if (_runState == RunState.Walk)
        {
            if (playerWalkState == PlayerWalkState.Walk_On) { return; }

            playerWalkState = PlayerWalkState.Walk_On;
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
        }
        else if (_runState == RunState.Run)
        {
            if (playerRunState == PlayerRunState.Run_On) { return; }

            playerRunState = PlayerRunState.Run_On;
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
        }
    }
    
    protected override void clearWalkAnimation(CharactorJobEnum _type)
    {
        base.clearWalkAnimation(_type);
    }
}
