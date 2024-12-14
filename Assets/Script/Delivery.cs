using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    //참조 = 기능 전달
    //MonoBehaviour를 상속 받지 않는 class에 전달하기 위해 만듬
    public static GameObject Instantiator(GameObject _OBJ, Vector3 _pos, Quaternion _quater, Transform _parent)
    {
        return Instantiate(_OBJ, _pos, _quater, _parent);
    }
    public static int LayerNameEnum(LayerTag layer) 
    {
        return LayerMask.NameToLayer(layer.ToString());
    }
    
}
