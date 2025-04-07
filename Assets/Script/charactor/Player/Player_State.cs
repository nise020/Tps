using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    //protected PlayerControllState playerControll = PlayerControllState.Off;
    [SerializeField] protected CharctorStateEnum charctorState;
    protected CharactorJobEnum playerType;
    float radius = 8f;
    float fieldOfView = 90f;
    //private void OnDrawGizmos()
    //{
    //    if (!Application.isPlaying) return;
    //    //Gizmos.color = Color.red;
    //    //Gizmos.DrawWireCube(mTarget.transform.position, Vector3.one * 0.5f);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(this.transform.position, radius);
    //}

    public bool SearchCheck(out Vector3 _pos) 
    {
        //float radius = 8f;
        //float fieldOfView = 90f;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit in hits)
        {
            if (hit.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster))
            {
                Vector3 dirToTarget = (hit.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, dirToTarget);

                if (angle < fieldOfView / 2f)
                {
                    //Debug.Log("시야 내 적 발견: " + hit.name);
                    _pos = hit.gameObject.transform.position;
                    return true;
                }
            }
        }
        _pos = new Vector3();
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
