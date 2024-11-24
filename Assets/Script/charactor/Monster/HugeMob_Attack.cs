using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class HugeMob : Monster
{
    float moveTime = 0.0f;
    float jumpStart = 0.0f;
    float jumpHight = 5.0f;
    float runningTime = 5.0f;
    bool targetCheack = false;
    [SerializeField] bool jumpOn = false;

    

    public void jumpAttack()//���� ���� �ʿ�
    {
        if (jumpOn == false) { return; }
        if (groundCheck == true)//���� ��� ������
        {
            //transform.position = Vector3.zero;
        }
        else if (targetCheack == false) 
        {
            targetOn(ref number);
            if (playerObj[number] == null)
            {
                return;
            }
            targetCheack = true;
            
        }
        moveTime += Time.deltaTime;
        float time = moveTime/ runningTime;
        Vector3 mpos = transform.position;
        Vector3 tpos = playerObj[number].transform.position;
        Vector3 dir = Vector3.Lerp(new Vector3(mpos.x,0, mpos.z), new Vector3(tpos.x, 0, tpos.z), time).normalized;
        float gravity = Physics.gravity.magnitude;
        //float ypos = Mathf.Sqrt( * jumpHight;
        //transform.position = new Vector3(dir.x, ypos, dir.z);
        //���Ͻ�

        #region AddForce
        //float xzSpeed = runningTime;
        //Vector3 dir = (playerObj[number].transform.position - transform.position).normalized;
        //dir.y = 0;

        //float yForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHight);//�߷��� �����־ƾ� �Ѵ�

        //Vector3 jumpPos = dir * xzSpeed + Vector3.up * yForce;

        //mobRigid.AddForce(jumpPos, ForceMode.VelocityChange);//����

        //jumpOn = false;
        #endregion

    }
    void FixedUpdate()
    {
        groundOn_Off(ref groundCheck);
        jumpAttack();
        //float gravity = Mathf.Abs(Physics.gravity.y);
        //moving();
        //StartCoroutine(MobAttackTimecheck());
    }
}
