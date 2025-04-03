using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    protected PlayerControllState playerControll = PlayerControllState.Off;
    protected CharctorStateEnum charctorState = CharctorStateEnum.Npc;
    protected PlayerjobEnum playerType;
    public void playerTypeInite(out PlayerjobEnum _type)
    {
        _type = playerType;
    }
    public void playerControllCheck(CharctorStateEnum _type)
    {
        charctorState = _type;
    }

    public bool playerEnumCheck(PlayerjobEnum _player)
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
    protected override void dead()
    {
        Shared.BattelManager.PlayerAlive = false;
        gameObject.SetActive(false);
    }
}
