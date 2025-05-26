using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character : Actor
{
    
    protected void groundCheak()
    {
        int layer = LayerMask.NameToLayer(LayerName.Ground.ToString());

        //bool isGround = Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down,
        //    out RaycastHit hit, groundCheckLenght + 0.1f, layer);
        bool isGround = Physics.Raycast(transform.position, Vector3.down,
            out RaycastHit hit, groundCheckLenght + 0.1f, layer);

        if (isGround)
        {
            if (GroundTouchState == GroundTouchState.GroundNoneTouch)
            {
                GroundTouchState = GroundTouchState.GroundTouch;
            }
            if (velocity.y < 0)
                velocity.y = 0f;
        }
        else
        {
            if (GroundTouchState == GroundTouchState.GroundTouch)
            {
                GroundTouchState = GroundTouchState.GroundNoneTouch;
            }
            velocity.y += gravityValue * Time.deltaTime;
        }
        transform.position += velocity * Time.deltaTime;
    }

    //protected abstract void nomalAttack();//순수 가상클래스
    //자식이 무조건 만들어야 하는 기능



    /// <summary>
    /// 일반 공격
    /// </summary>
    //protected virtual void nomalAttack() { }
    //자식들이 사용 할수도 안할수도 있는 기능

}
