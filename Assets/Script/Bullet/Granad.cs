using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granad : Item
{
    Vector3 GranadPos;
    public Vector3 startPos;
    [SerializeField] GameObject explotionObj;
    private void Active()
    {
        GameObject go = Instantiate(explotionObj,transform.position,Quaternion.identity);
    }
    private void Start()
    {
        //Torque<--È¸Àü·Â
        GranadPos = startPos;
        transform.position = GranadPos;
        GetComponent<Rigidbody>().AddTorque(Vector3.one * 200.0f);
    }
}
