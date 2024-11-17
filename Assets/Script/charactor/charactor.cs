using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class charactor : Actor
{
    //protected abstract void nomalAttack();//순수 가상클래스
    //자식이 무조건 만들어야 하는 기능
    protected virtual void nomalAttack(){}
    //자식들이 사용 할수도 안할수도 있는 기능
}
