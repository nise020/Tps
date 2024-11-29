using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class AiMonster : AiBase
{
    Monster Monster;

    //public void Start()
    //{
    //    Type();
    //    state();
    //    Monster = GetComponent<Monster>();
    //}
    public void FixedUpdate()
    {
        state();
    }

}
