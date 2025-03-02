using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (aIState == AiState.Attack)
        {
            if (monster == MonsterType.Sphere)
            {
                mobAnimator.SetInteger("Close", 0);
                mobAnimator.SetInteger("AttackDilray", 1);
                aIState = AiState.Reset;
            }
        }
    }
    protected void moving()//���� ��Ŀ������ �̸� ���ϰ� ���������� ������ �ؼ� ����  
    {
        //bool check = groundOn_Off(groundCheck);
        //if (check == false) { return; }
        //if (check == true)
        //{
        //    if (AI.moveChange) //�¿� ������
        //    {
        //        transform.position += Vector3.right;
        //    }
        //    else
        //    {
        //        transform.position -= Vector3.right;
        //    }
        //}


        //Physics.OverlapSphere(transform.position)
    }
    public static void DrawSphere(Vector3 center, float radius) 
    {

    } 
    protected virtual bool groundOn_Off(bool _check) 
    {
        if (footObj == null) { return false; }
        Ray ray = new Ray(transform.position, Vector3.down);//�Ʒ�����
        LayerMask layerName = LayerMask.GetMask("Ground");//�� �κ� �����ʿ�
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, leagh, layerName))
        {
            _check = true;
            mobRigid.useGravity = false;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
        {
            _check = false;
            mobRigid.useGravity = true;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        return _check;
    }

}
