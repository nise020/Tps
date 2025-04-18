using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    //protected PlayerControllState playerControll = PlayerControllState.Off;
    [SerializeField] protected CharctorStateEnum charctorState;
    protected CharactorJobEnum playerType;
    float radius = 10.0f;
    float fieldOfView = 90f;
    //private void OnDrawGizmos()
    //{
    //    if (!Application.isPlaying) return;
    //    //Gizmos.color = Color.red;
    //    //Gizmos.DrawWireCube(mTarget.transform.position, Vector3.one * 0.5f);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(this.transform.position, radius);
    //}

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
    public void PlayerTypeInite(out CharactorJobEnum _type)
    {
        _type = playerType;
    }
    public void PlayerControllChange(CharctorStateEnum _type)
    {
        charctorState = _type;
        Debug.Log($"{gameObject}/{charctorState} = {_type}"); 
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
    protected override void dead()
    {
        Shared.BattelManager.PlayerAlive = false;
        gameObject.SetActive(false);
    }

}
