using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public partial class SphereMob : Monster
{
    
    [SerializeField] bool jumpOn = false;
    [SerializeField] GameObject maxEyeObj;
    
    protected override bool groundOn_Off(bool _check) 
    {
        base.groundOn_Off(_check);
        return _check;
    }
  

}
