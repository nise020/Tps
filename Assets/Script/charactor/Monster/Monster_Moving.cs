using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    [SerializeField] GameObject footObj;
    [SerializeField] protected bool groundCheck = false;
    [SerializeField] protected float leagh;
    protected Rigidbody mobRigid;
    Color leaghColor;
    BoxCollider boxColl;
    Collider mobColl;
    
    protected void moving()//몬스터 매커니즘을 미리 정하고 세부적으로 만들어야 해서 보류  
    {
        groundOn_Off(ref groundCheck);
        if (groundCheck == false) { return; }
        if (groundCheck == true) 
        {
            mobRigid.velocity = Vector3.zero;
            //transform.position += new Vector3(1,0,0) * Time.deltaTime;
        }
    }
    protected void groundOn_Off(ref bool _check) 
    {
        Ray ray = new Ray(transform.position, Vector3.down);//아래방향
        LayerMask layerName = LayerMask.GetMask("Ground");//이 부분 수정필요
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
