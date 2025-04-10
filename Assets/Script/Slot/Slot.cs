using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Slot : MonoBehaviour
{
    public Transform SlotTransform; // 위치 참조
    public PositionObjectState ObjectState = PositionObjectState.None; // 현재 비어 있는지 여부
    public GameObject FootholdObj = null;

    public Vector3 PositionUpdate() 
    {
        Vector3 position = gameObject.transform.position;
        return position;
    }
    
    //protected GameObject rightObj;
    //protected GameObject reftObj;
    //protected List<GameObject> moveObj = new List<GameObject>();
    //protected Dictionary<GameObject, SlotData> slotStates = new Dictionary<GameObject, SlotData>();
    //protected FindMoveObject findMoveObject = FindMoveObject.None;

    //int keynumber = 0;

    //protected class SlotData
    //{
    //    public Transform SlotTransform; // 위치 참조
    //    public PositionObjectState ObjectState = PositionObjectState.Empty; // 현재 비어 있는지 여부
    //    public GameObject FootholdObj = null;
    //}
    //protected void slotAdd()
    //{
    //    Transform[] children = GetComponentsInChildren<Transform>();
    //    foreach (Transform child in children)
    //    {
    //        int layer = child.gameObject.layer;
    //        if (layer == LayerMask.NameToLayer(LayerName.BackPosition1.ToString()))
    //        {
    //            rightObj = child.gameObject;
    //            addSlot(rightObj);
    //        }
    //        else if (layer == LayerMask.NameToLayer(LayerName.BackPosition1.ToString()))
    //        {
    //            reftObj = child.gameObject;
    //            addSlot(reftObj);
    //        }
    //    }
    //}
    //protected void addSlot(GameObject _obj)
    //{
    //    moveObj.Add(_obj);
    //    slotStates[_obj] = new SlotData()
    //    {
    //        SlotTransform = _obj.transform,
    //        ObjectState = PositionObjectState.None,
    //        FootholdObj = null
    //    };
    //}
}
