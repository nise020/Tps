using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    //���� = ��� ����
    //MonoBehaviour�� ��� ���� �ʴ� class�� �����ϱ� ���� ����
    public static GameObject Instantiator(GameObject _OBJ, Vector3 _pos, Quaternion _quater, Transform _parent)
    {
        return Instantiate(_OBJ, _pos, _quater, _parent);
    }
    public static int LayerNameEnum(LayerTag layer) 
    {
        return LayerMask.NameToLayer(layer.ToString());
    }
    
}
