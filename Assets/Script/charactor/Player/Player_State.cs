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
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(mTarget.transform.position, Vector3.one * 0.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

    public bool SearchCheck(out Vector3 _pos) //매니저 한테서 서치 하는 방향으로 고치기 필요
    {
        //float radius = 8f;
        //float fieldOfView = 90f;

        string layer = LayerName.Monster.ToString();//8
        int layermask = LayerMask.GetMask(layer);
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layermask);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster))
            {
                _pos = hit.gameObject.transform.position;
                return true;
            }
            //else 
            //{
            //    _pos = new Vector3();
            //    return false; 
            //}
        }
        _pos = Vector3.zero;
        return false;
    }
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
    protected override void dead()
    {
        Shared.BattelManager.PlayerAlive = false;
        gameObject.SetActive(false);
    }

}
