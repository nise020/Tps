using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Actor : MonoBehaviour
{
    protected Camera cam;
    //������Ʈ
    //���ҽ� ����� start�� ��� �ؾ� �ϴ���?
    protected virtual void Start()//Actor�� �̵�                              
    {
        //cam = gameObject.Find("MainCamera").GetComponent<Camera>;
        cam = Camera.main;
    }
}
