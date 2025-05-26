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

    //protected abstract void nomalAttack();//���� ����Ŭ����
    //�ڽ��� ������ ������ �ϴ� ���



    /// <summary>
    /// �Ϲ� ����
    /// </summary>
    //protected virtual void nomalAttack() { }
    //�ڽĵ��� ��� �Ҽ��� ���Ҽ��� �ִ� ���

}
