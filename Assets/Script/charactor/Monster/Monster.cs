using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public partial class Monster : Charactor
{
    Condition condition = Condition.health;//상태패턴
    protected virtual void Start()
    {
        FindBodyObject();
    }
    protected virtual void FixedUpdate()
    {
        if (AI == null) { return; }
        AI.State(ref aIState);
        CameraInMonsterCheck();
    }
    protected HpBar HPBAR = new HpBar();
    public void HpInIt(HpBar _hpBar)
    {
        HPBAR = _hpBar;
    }
    protected void CameraInMonsterCheck()
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(gameObject.transform.position);

        bool isVisible = (viewportPos.z > 0 && viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1);
        if (isVisible)
        {
            HPBAR.gameObject.SetActive(true);
        }
        else
        {
            HPBAR.gameObject.SetActive(false);
        }
    }

    public float GetMonsterHeight()
    {
        return GetComponent<Collider>().bounds.size.y;
    }
}
