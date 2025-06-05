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
        
        if (_value == 0 || (_value <= 2 && canReceiveInput)) // 마지막 콤보 (더 이상 연결 없음)
        {
            playerAnimator.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);

            attackAnimation(PlayerAttackState.Attack_Off);

            playerAnimator.speed = 1.0f;

            if (!canReceiveInput) 
            {
                canReceiveInput = true;
            }

            playerStateData.AttackState = PlayerAttackState.Attack_Off;

            currentCombo = 0;
            return;
        }
        else if (canReceiveInput == false && _value <= 2) 
        {
            attackAnimation(PlayerAttackState.Attack_Off);
            playerStateData.AttackState = PlayerAttackState.Attack_Combo;

            playerAnimator.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), _value);
            playerAnimator.speed = 1.5f;

            canReceiveInput = true;
        }

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

        playerAnimator.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);
        playerAnimator.speed = 1.0f;

        playerStateData.AttackState = PlayerAttackState.Attack_Off;
    }
    IEnumerator PlayAttackComboCheck(float _timer,int _value) 
    {
        yield return new WaitForSeconds(_timer);
        if (_value == currentCombo)
        {
            canReceiveInput = true;
            playerAnimator.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), currentCombo);
        }
        else 
        {
            currentCombo = 0;
            playerAnimator.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);
        }


    }



    public void GetSword()//AnimationEvent
    {
        GameObject go = weaponObj.gameObject;
        go.transform.SetParent(weaponHandObject.gameObject.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        playerStateData.WeaponState = PlayerWeaponState.Sword_On;

        //CreatSkill(SkillEffectObj1, SkillParentObj1);
        //CreatSkill(SkillEffectObj2, SkillParentObj2);

    }
   
    public void ClearlSword(int _value)//AnimationEvent
    {
        if (scabbardMaxCount == scabbardCount)
        {
            playerAnimator.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 0);
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
        playerStateData.WeaponState = PlayerWeaponState.Sword_Off;
    }
    protected override void walkAnim(PlayerWalkState _state, Vector3 _pos)
    {
        if (_state == PlayerWalkState.Walk)
        {
            playerStateData.WalkState = PlayerWalkState.Walk;
            playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        }
        else if (_state == PlayerWalkState.Run)
        {
            playerStateData.WalkState = PlayerWalkState.Run;
            playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
            playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        }
    }
    
    protected override void clearWalkAnimation()
    {
        base.clearWalkAnimation();
    }
}
