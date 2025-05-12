using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Actor : MonoBehaviour
{
    protected Camera cam;
    //protected State STATE = new State();
    
    protected ObjectType objType = ObjectType.None;//현재 몬스터만 적용

    public void TypeInit(ObjectType _objType) 
    {
        objType = _objType;
    }

    //오브젝트
    //리소스 재사용시 start문 사용 해야 하느가?

}
