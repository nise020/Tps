using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    Guns guns;
    void Start()
    {
        guns = GetComponentInParent<Guns>();
    }
    private void OnTriggerEnter(Collider other)//������ ����Ʈ�� ��Ҵ��� üũ
    {
        //guns
    }
    private void OnTriggerExit(Collider other)
    {
        //guns
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
