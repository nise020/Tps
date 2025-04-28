using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public partial class Monster : Charactor
{
    protected Item ITEM;
    protected AiMonster AI = new AiMonster();
    protected Skill_Monster SKILL = new Skill_Monster();

    public int mobKey = 0;
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
    public void mobIndex(int _key)
    {
        mobKey = _key;
    }
    public void ItemUpdate(Item _item) 
    {
        ITEM = _item;
    }
}
