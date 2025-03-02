using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Actor : MonoBehaviour
{
    protected Camera cam;
    //오브젝트
    //리소스 재사용시 start문 사용 해야 하느가?
    protected virtual void Start()//Actor에 이동                              
    {
        //cam = gameObject.Find("MainCamera").GetComponent<Camera>;
        cam = Camera.main;
    }
}
