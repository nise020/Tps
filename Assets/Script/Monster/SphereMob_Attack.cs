using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.XR;

public partial class SphereMob : Monster
{
    
    [SerializeField] bool jumpOn = false;
    [SerializeField] GameObject maxEyeObj;
    
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
