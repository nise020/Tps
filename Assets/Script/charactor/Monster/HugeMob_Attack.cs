using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class HugeMob : Monster
{
    float moveTimer = 0.0f;
    float jumpStart = 0.0f;
    float jumpHight = 5.0f;
    float runningTime = 5.0f;
    bool targetCheack = false;
    [SerializeField] bool jumpOn = false;

    

    public void jumpSkill()//조금 수정 필요
    {
        if (jumpOn == false) { return; }
        
        moveTimer += Time.deltaTime;
        float time = moveTimer / runningTime;

        if (moveTimer < runningTime)
        {
            if (targetCheack == false)
            {
                targetOn(ref number);
                if (playerObj[number] == null)
                {
                    return;
                }
                targetCheack = true;

            }
            #region Vector3
            //float gravity = Physics.gravity.magnitude;
            //Vector3 mpos = transform.position;
            //Vector3 tpos = playerObj[number].transform.position;
            //Vector3 movePos = Vector3.Lerp(mpos, tpos, time);
            //movePos.y = 0f;

            //float initialYVelocity = Mathf.Sqrt(2 * gravity * jumpHight); // 초기 속도
            //float elapsedTime = moveTimer; // 경과 시간
            //float ypos = mpos.y + initialYVelocity * elapsedTime - 0.5f * gravity * Mathf.Pow(elapsedTime, 2); // Y축 공식 적용

            //// 목표 지점의 Y값 반영
            //ypos = Mathf.Max(ypos, tpos.y); // 목표 Y값보다 아래로 내려가지 않도록 제한
            //movePos.y = ypos;

            // 최종 위치 갱신
            //transform.position = movePos;
            #endregion


            #region AddForce
            float xzSpeed = runningTime;
            Vector3 dir = (playerObj[number].transform.position - transform.position).normalized;
            dir.y = 0;

            float yForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHight);//중력이 켜져있아야 한다

            Vector3 jumpPos = dir * xzSpeed + Vector3.up * yForce;

            mobRigid.AddForce(jumpPos, ForceMode.VelocityChange);//점프

            jumpOn = false;
            #endregion

            //float ypos = jumpHight * (1 - Mathf.Pow(2 * time - 1, 2)); // 포물선 공식
            //movePos.y = ypos + Mathf.Lerp(mpos.y, tpos.y, time);
            //transform.position = movePos;


            //float ypos = Mathf.Sin(Mathf.PI * time) * jumpHight;
            //Vector3 landingPos = new Vector3(movePos.x, ypos + tpos.y, movePos.z);
        }
        else
        {
            transform.position = playerObj[number].transform.position;
            transform.rotation = Quaternion.identity;
            moveTimer = 0;
        }
        Debug.Log($"Time: {time}");
        //디스턴스

        

    }
    
    protected override void targetOn(ref int _value)
    {
        base.targetOn(ref _value);
    }
    protected override void groundOn_Off(ref bool _check) 
    {
        base.groundOn_Off(ref _check);
    }
    
}
