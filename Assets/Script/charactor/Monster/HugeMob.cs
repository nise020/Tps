using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class HugeMob : Monster
{
    public void jump()
    {
        targetOn();
        Vector3 pos = transform.position;
        //Vector3 Dir = playerObj[number] - transform.position;
    }
}
