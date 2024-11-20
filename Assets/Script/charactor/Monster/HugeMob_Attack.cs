using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class HugeMob : Monster
{
    float moveTime = 0.0f;
    int jumpHight = 10;
    float runningTime = 3.0f;
    bool target = false;
    public void jump()
    {
        if (target == false) 
        {
            target = true;
            targetOn(ref number);
        }
        Vector3 pos = transform.position;
        Vector3 Dir = (playerObj[number].transform.position - transform.position).normalized;
        moveTime += Time.deltaTime;
        float time = moveTime / runningTime;
        Vector3 posXZ = Vector3.Lerp(pos, Dir, time);
        float radian = Mathf.Sin(45 * time) * jumpHight;
        transform.position = posXZ + new Vector3(0, radian, 0);


        //Mathf.Sin()
    }
    void FixedUpdate()
    {
        //MobAttackTimecheck();
        jump();
        //moving();
        //StartCoroutine(MobAttackTimecheck());
    }
}
