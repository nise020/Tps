using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    [SerializeField] GameObject HandObj;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject scabbard;
    int scabbardCount = 0;
    int scabbardMaxCount = 2;
    Vector3 weaponOriginalPos = Vector3.zero;
    protected void FindWeaponObject(LayerName _name)
    {
        //GameObject go = null;
        SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        int value = LayerMask.NameToLayer(_name.ToString());
        foreach (var skinObj in skin)
        {
            if (skinObj.gameObject.layer == value)
            {
                weapon = skinObj.rootBone.gameObject;
                weaponOriginalPos = weapon.transform.localPosition;
                break;
            }
        }
    }
    public void SkillEffectOff(int _value) 
    {
        if (_value == 1&& SkillObj1.activeSelf) 
        {
            SkillObj1.SetActive(false);
            firstSkillCheck = SkillRunning.SkillOff;
            playerAnim.SetInteger(SkillType.Skill1.ToString(), 0);
        }
        else if (_value == 2 && SkillObj2.activeSelf)
        {
            SkillObj2.SetActive(false);
            secondSkillCheck = SkillRunning.SkillOff;
            playerAnim.SetInteger(SkillType.Skill2.ToString(), 0);
        }
        scabbardCount = 0;
    }
    public void GetSword()//AnimationEvent
    {
        GameObject go = weapon.gameObject;
        go.transform.SetParent(HandObj.gameObject.transform);
        go.transform.localPosition = Vector3.zero;

        GameObject effectObj = Instantiate(SkillEffectObj1,Vector3.zero,
            Quaternion.identity, go.transform);

        SkillObj1 = effectObj;
        SkillObj1.SetActive(false);

        weaponState = WeaponState.Sword_On;
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
        GameObject go = weapon.gameObject;
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

    protected override void clearWalkAnim(CharactorJobEnum _type)
    {
        base.clearWalkAnim(_type);
    }
}
