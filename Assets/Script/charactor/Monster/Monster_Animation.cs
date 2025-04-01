using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Charactor
{
    public Animator mobAnimator;
    public bool animCheack = false;
    public void PointMoveAnim(int _value)
    {
        if (_value != 0) 
        {
            mobAnimator.SetInteger("Walk", 1);
            Debug.Log("PointMoveAnim");
            AI.searchingOnOff = true;
            //AI.searching = SearchState.Move;
            //mobAnimator.SetInteger("Search", 0);
        }
    }
    public void SearchAnim(int _value) 
    {

    }
    public void AttackEndAnim(int _value)
    {
        if (_value != 0)
        {
            //AI.moveing = true;
            mobAnimator.SetInteger("Attack", 0);
            Debug.Log("AttackReadyAnim");
            //mobAnimator.SetInteger("Search", 0);
        }
    }
    public void AttackReadyAnim(int _value)//cicle
    {
        if (_value != 0)
        {
            mobAnimator.SetInteger("Attack", 1);
            Debug.Log("AttackReadyAnim");
            AI.moveing = true;
            //mobAnimator.SetInteger("Search", 0);
        }
    }
    public IEnumerator EndAinmation(Animator _anim,string _animText) 
    {
        //AnimatorStateInfo animStateInfo = _anim.GetCurrentAnimatorStateInfo(0);//layer
        //float time = animStateInfo.normalizedTime;

        //if (time >= 1.0f && animStateInfo.IsName(_animText))
        //{

        //}
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName(_animText)) 
        {
            float time = _anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        yield return new WaitForSeconds(0.5f);
        _anim.Play(_animText, 0, 0f);
    }

    //이더 스크롤

}
