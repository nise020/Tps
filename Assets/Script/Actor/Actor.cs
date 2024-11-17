using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
   [Tooltip("몬스터가 공격할 대상을 총알,투척물이 받아서 사용")] public Transform ActTargetTrs { get; protected set; }
    //protected abstract void nomalAttack();순수 가상 클래스

}
