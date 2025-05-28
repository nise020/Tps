using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    [SerializeField] GameObject scabbard;

    int scabbardCount = 0;
    int scabbardMaxCount = 2;

    public void ComboEventCheck(int _value) //AnimationEvent
    {
        attackAnimation(PlayerAttackState.Attack_Off);
        if (_value >= 2) // 마지막 콤보 (더 이상 연결 없음)
        {

            ResetCombo();
            return;
        }

        // 중간 콤보에서 다음 입력이 들어왔을 경우
        if (currentCombo == _value + 1) // 다음 콤보로 연결할 조건
        {
            playerAnimtor.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), currentCombo);
            playerAnimtor.speed = 1.5f;
            canReceiveInput = true;
            return;
        }

        // 입력 없으면 리셋

        ResetCombo();


        //float clipLength = GetClipLength($"Attack_Combo_Weapon_{currentCombo}");

        //if (clipLength <= 0f)
        //{
        //    Debug.LogWarning("해당 클립을 찾지 못했습니다.");
        //    //break;
        //}

        //float adjustedTime = clipLength / playerAnimtor.speed;
        //StartCoroutine(PlayAttackComboCheck(adjustedTime, _value));
    }
    private void ResetCombo()
    {
        currentCombo = 0;
        canReceiveInput = false;

        playerAnimtor.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);
        playerAnimtor.speed = 1.0f;

        PlayerStateData.AttackState = PlayerAttackState.Attack_Off;
    }
    IEnumerator PlayAttackComboCheck(float _timer,int _value) 
    {
        yield return new WaitForSeconds(_timer);
        if (_value == currentCombo)
        {
            canReceiveInput = true;
            playerAnimtor.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), currentCombo);
        }
        else 
        {
            currentCombo = 0;
            playerAnimtor.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);
        }


    }



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
            //currentCombo = 0;//reset
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
