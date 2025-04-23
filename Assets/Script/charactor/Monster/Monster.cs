using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public partial class Monster : Charactor
{
    Condition condition = Condition.health;//상태패턴
    protected virtual void FixedUpdate()
    {
        if (AI == null) { return; }
        AI.State();
    }
    private void LateUpdate()
    {
        CameraInMonsterCheck();
    }
    protected void CameraInMonsterCheck()
    {
        Player player = Shared.GameManager.PlayerLoad();
        Camera camera = player.GetComponentInChildren<Camera>();

        Vector3 viewportPos = camera.WorldToViewportPoint(gameObject.transform.position);

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
