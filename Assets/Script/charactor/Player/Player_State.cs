using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    //protected PlayerControllState playerControll = PlayerControllState.Off;
    [SerializeField] protected CharctorStateEnum charctorState;
    protected CharactorJobEnum playerType;
    public void PlayerTypeInite(out CharactorJobEnum _type)
    {
        _type = playerType;
    }
    public void PlayerControllChange(CharctorStateEnum _type)
    {
        charctorState = _type;
    }
    public void PlayerControllChack(out CharctorStateEnum _type)
    {
        _type = charctorState;
    }
    public bool CharactorEnumCheck(CharactorJobEnum _player)
    {
        if (_player == playerType)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //public bool playerenumcheck(CharctorStateEnum _player)
    //{
    //    //if (_player == CharctorStateEnum)//Npc
    //    //{
    //    //    return true;
    //    //}
    //    //else
    //    //{
    //    //    return false;
    //    //}
    //}
    protected override void dead()
    {
        Shared.BattelManager.PlayerAlive = false;
        gameObject.SetActive(false);
    }
}
