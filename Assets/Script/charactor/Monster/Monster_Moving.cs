using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Character
{
    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //    if (aIState == MonsterAiState.Attack)
    //    {
    //        if (monsterType == MonsterType.Sphere)
    //        {
    //            monsterAnimator.SetInteger("Close", 0);
    //            monsterAnimator.SetInteger("AttackDilray", 1);
    //            aIState = MonsterAiState.Reset;
    //        }
    //    }
    //}
    protected void moving()//몬스터 매커니즘을 미리 정하고 세부적으로 만들어야 해서 보류  
    {
        //bool check = groundOn_Off(groundCheck);
        //if (check == false) { return; }
        //if (check == true)
        //{
        //    if (AI.moveChange) //좌우 움직임
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
        Ray ray = new Ray(transform.position, Vector3.down);//아래방향
        LayerMask layerName = LayerMask.GetMask("Ground");//이 부분 수정필요
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, leagh, layerName))
        {
            _check = true;
            monsterRigid.useGravity = false;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
        {
            _check = false;
            monsterRigid.useGravity = true;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        return _check;
    }

}
