using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{



    
    public void PlayerTypeInite(out PlayerType _type)
    {
        _type = playerStateData.PlayerType;
    }
    public void PlayerControllChange(PlayerModeState _type)
    {
        playerStateData.ModeState = _type;
        Debug.Log($"{gameObject}/{playerStateData.ModeState} = {_type}"); 
    }

    public void PlayerControllChack(out PlayerModeState _type)
    {
        _type = playerStateData.ModeState;
    }
    public bool CharactorEnumCheck(PlayerType _player)
    {
        if (_player == playerStateData.PlayerType)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected override void death()
    {
        base.death();
        gameObject.SetActive(false);
    }

    protected int AttackComboStateCheck()
    {
        int stateValue = 0;
        if (playerStateData.AttackState == AttackState.Attack_On) 
        {
            stateValue = (int)StatusData[StatusType.Power];
        }
        else if (playerStateData.AttackState == AttackState.Attack_Combo) 
        {
            float combo = StatusData[StatusType.Power] * 1.5f;
            stateValue = (int)combo;
        }
        return stateValue;
    }
    
}
