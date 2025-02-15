using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granad : Actor
{
    Vector3 GranadPos;
    public Vector3 startPos;
    private void Active()
    {
        
    }
    protected override void Start()
    {
        //Torque<--È¸Àü·Â
        GranadPos = startPos;
        transform.position = GranadPos;
        GetComponent<Rigidbody>().AddTorque(Vector3.one * 200.0f);
    }
}
