using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class SphereMob : Monster
{
    
    [SerializeField] bool jumpOn = false;
    [SerializeField] GameObject maxEyeObj;

    public void timer() 
    {
        //float search = Vector3.Distance(eyeObj.transform.forward, maxEyeObj.transform.position);
        //if (search < 50) 
        //{

        //}
        if (Physics.Raycast(eyeObj.transform.position,
            eyeObj.transform.forward, out RaycastHit hit)) 
        {
            string text1 = ($"{LayerTag.Player}");//enum
            string text2 = ($"{LayerTag.Cover}");
            int layer = hit.collider.gameObject.layer;
            string name = LayerMask.LayerToName(layer);

            if (name == text1)
            {

            }
            else if (name == text2) 
            {
                return; 
            }
        }

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
