using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class HugeMob : Monster
{
    
    [SerializeField] bool jumpOn = false;

    public void timer() 
    {

    }

    
    
    protected override void targetOn(ref int _value, List<GameObject> _listObj)
    {
        base.targetOn(ref _value, _listObj);
    }

    protected override bool groundOn_Off(bool _check) 
    {
        base.groundOn_Off(_check);
        return _check;
    }


}
