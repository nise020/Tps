using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{



    public bool SearchCheck(out Vector3 _pos) //매니저 한테서 서치 하는 방향으로 고치기 필요
    {
        //float radius = 8f;
        //float fieldOfView = 90f;
        Vector3 position = Shared.MonsterManager.monsterSearch(gameObject, radius);
        if (position == Vector3.zero)
        {
            _pos = Vector3.zero;
            return false;
        }
        else 
        {
            float distance = Vector3.Distance(position, gameObject.transform.position);
            if (radius >= distance)
            {
                _pos = position;
                return true;
            }
            else
            {
                _pos = Vector3.zero;
                return false;
            }
        }
    }
    public void PlayerTypeInite(out PlayerType _type)
    {
        _type = PlayerStateData.PlayerType;
    }
    public void PlayerControllChange(PlayerModeState _type)
    {
        PlayerStateData.PlayerState = _type;
        Debug.Log($"{gameObject}/{PlayerStateData.PlayerState} = {_type}"); 
    }

    public void PlayerControllChack(out PlayerModeState _type)
    {
        _type = PlayerStateData.PlayerState;
    }
    public bool CharactorEnumCheck(PlayerType _player)
    {
        if (_player == PlayerStateData.PlayerType)
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

}
