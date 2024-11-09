using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    Gun guns;
    void Start()
    {
        guns = GetComponentInParent<Gun>();
    }
    private void OnTriggerEnter(Collider other)//레이저 사이트에 닿았는지 체크
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
