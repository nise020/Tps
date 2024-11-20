using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : charactor
{
    [SerializeField] GameObject footObj;
    [SerializeField] protected bool groundCheck = false;
    [SerializeField] protected float leagh;
    Color leaghColor;
    Rigidbody mobRigid;
    BoxCollider boxColl;
    Collider mobColl;
    protected void moving()//���� ��Ŀ������ �̸� ���ϰ� ������ �ؼ� ����  
    {
        groundOn_Off(ref groundCheck);
        if (groundCheck == false) { return; }
        


    }
    protected void groundOn_Off(ref bool _check) 
    {
        Ray ray = new Ray(transform.position, Vector3.down);//�Ʒ�����
        LayerMask layerName = LayerMask.GetMask("Ground");//�� �κ� �����ʿ�
        if (Physics.Raycast(ray, out RaycastHit hit, leagh, layerName))
        {
            _check = true;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
        {
            _check = false;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
    }

}
