using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    [Header("�ٸ�,�̵� ����(FlyingMob ����)")]
    [SerializeField] GameObject footObj;
    [SerializeField] protected bool groundCheck = false;
    [SerializeField] protected float leagh;
    protected Rigidbody mobRigid;
    Color leaghColor;
    BoxCollider boxColl;
    Collider mobColl;
    public eMobType eType;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.layer == LayerMask.NameToLayer("BackGround"))
        {
            if (AI.moveChange) //�¿� ������
            {
                AI.moveChange = false;
            }
            else
            {
                AI.moveChange = true;
            }
            //AI.moveChange = true;
        }
    }
    protected void moving()//���� ��Ŀ������ �̸� ���ϰ� ���������� ������ �ؼ� ����  
    {
        bool check = groundOn_Off(groundCheck);
        if (check == false) { return; }
        if (check == true) 
        {
            if (AI.moveChange) //�¿� ������
            {
                transform.position += Vector3.right;
            }
            else
            {
                transform.position -= Vector3.right;
            }
            //transform.position += new Vector3(1,0,0) * Time.deltaTime;
        }
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
