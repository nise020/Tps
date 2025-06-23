using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    [Header("Warrior/Animation")]
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
    public override void AnimationOut(string _type) //AnimationEvent
    {
        if (_type == null)
        {
            Debug.LogError($"_type 값의 해당하는 애니메이션이 아닙니다");
            return;
        }

        if (_type == PlayerAnimParameters.Avoidance.ToString())
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Avoidance.ToString(), 0);

            playerStateData.avoidanceState = AvoidanceState.Avoidance_Off;
        }
        else if (_type == PlayerAnimParameters.Dash.ToString())
        {
            playerAnimator.speed = 1f;
            playerStateData.WalkState = PlayerWalkState.Run;
            WalkStateAnimation(playerStateData.WalkState);
        }
        else if (_type == PlayerAnimParameters.Block.ToString())
        {
            battelTriggerCol.enabled = false;

            playerStateData.AttackState = AttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
        }
    }
    public override void AnimationStart(string _type) //AnimationEvent
    {
        if (_type == "")
        {
            Debug.LogError($"_type 값의 해당하는 애니메이션이 아닙니다");
            return;
        }
        if (_type == "Attack")
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Attack.ToString(), 0);
        }
        else if (_type == "ComboAttack")
        {
            playerAnimator.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);
        }
        else if (_type == PlayerAnimParameters.Avoidance.ToString())
        {

            AnimatorStateInfo info = playerAnimator.GetCurrentAnimatorStateInfo(0);
            float animLength = info.length / playerAnimator.speed;
            float normalizedTime = info.normalizedTime % 1f;

            float remainingTime = animLength * (1f - normalizedTime);



            StartCoroutine(BackAvoid(gameObject, remainingTime));
            //AnimationEventTiming(PlayerAnimParameters.Avoidance, 1.5f,0.10f);
        }
        else if (_type == PlayerAnimParameters.Dash.ToString())
        {
            //Debug.Log($"playerStateData.WalkState ={playerStateData.WalkState}");

            //if (dashCheck)
            //{
            //    dashCheck = false;
            //    StartCoroutine(dashMove());
            //}

            //playerStateData.WalkState = PlayerWalkState.Dash;
            //Debug.Log($"playerStateData.WalkState ={playerStateData.WalkState}");
        }
    }


    public void ResetCombo()
    {
        playerStateData.AttackState = AttackState.Attack_Off;
        attackAnimation(playerStateData.AttackState, 0);

        canReceiveInput = false;
    }
    public void ComboEventCheck(int _value) //AnimationEvent
    {

        if (_value == 0) 
        {
            ResetCombo();
            return;
        }

        if (!canReceiveInput)
        {
            attackAnimation(AttackState.Attack_Combo, _value);
            canReceiveInput = true;

            return;
        }
        else 
        {
            ResetCombo();
        }
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
        GameObject go = MainWeaponObj.gameObject;
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
        playerStateData.WeaponState = PlayerWeaponState.Sword_Off;
        GameObject go = MainWeaponObj.gameObject;
        go.transform.SetParent(scabbard.gameObject.transform);
        go.transform.localPosition = weaponOriginalPos;
        go.transform.localRotation = Quaternion.identity;
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
