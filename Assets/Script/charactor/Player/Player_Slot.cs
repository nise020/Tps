using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player : Charactor
{
    protected Slot rightObj;
    PositionObjectState rightObjState = PositionObjectState.None;
    protected Slot reftObj;
    PositionObjectState reftObjState = PositionObjectState.None;
    Transform FollowPosition;
    protected Queue<GameObject> moveObj = new Queue<GameObject>();
    //protected Dictionary<GameObject, SlotData> slotStates = new Dictionary<GameObject, SlotData>();
    //아틀라스
    //큐
    //리스트
    protected FindMoveObject findMoveObject = FindMoveObject.None;
    List<Slot> slotList = new List<Slot>();
    int keynumber = 0;
    //검색
    //정렬
    //이동
    public void slotinit()
    {
        //Transform[] children = GetComponentsInChildren<Transform>();
        //foreach (Transform child in children)
        //{
        //    int layer = child.gameObject.layer;
        //    if (layer == LayerMask.NameToLayer(LayerName.BackPosition1.ToString()))
        //    {
        //        rightObj = child.gameObject;
        //        Slot slot = rightObj.GetComponent<Slot>();
        //        slot.ObjectState = PositionObjectState.Empty;
        //        slotList.Add(rightObj);
        //    }
        //    else if (layer == LayerMask.NameToLayer(LayerName.BackPosition2.ToString()))
        //    {
        //        reftObj = child.gameObject;
        //        Slot slot = reftObj.GetComponent<Slot>();
        //        slot.ObjectState = PositionObjectState.Empty;
        //        slotList.Add(reftObj);
        //    }
        //}
        Slot[] children = GetComponentsInChildren<Slot>();
        foreach (Slot child in children)
        {
            int layer = child.gameObject.layer;
            if (layer == LayerMask.NameToLayer(LayerName.BackPosition1.ToString()))
            {
                Slot slot = child;
                slot.ObjectState = PositionObjectState.Empty;
                rightObj = slot;
                slotList.Add(rightObj);
            }
            else if (layer == LayerMask.NameToLayer(LayerName.BackPosition2.ToString()))
            {
                Slot slot = child;
                slot.ObjectState = PositionObjectState.Empty;
                reftObj = slot;
                slotList.Add(reftObj);
            }
        }
    }
    public Vector3 SlotPositionUpdate(LayerName _layer) 
    {
        if (_layer == LayerName.BackPosition1)
        {
            return rightObj.PositionUpdate();
        }
        else if(_layer == LayerName.BackPosition2)
        {
            return reftObj.PositionUpdate();
        }
        return new Vector3();
    }
    public void movePointSearch(out LayerName _layer)//Player State Object
    {
        int count = slotList.Count;
        for (int i = 0; i < count; i++) 
        {
             Slot slot = slotList[i].gameObject.GetComponent<Slot>();
            if (slot.ObjectState == PositionObjectState.Empty) 
            {
                if (slot.gameObject.layer == LayerMask.NameToLayer(LayerName.BackPosition1.ToString()))
                {
                    layerName = LayerName.BackPosition1;
                    slot.ObjectState = PositionObjectState.Occupied;
                    break;
                }
                else if (slot.gameObject.layer == LayerMask.NameToLayer(LayerName.BackPosition2.ToString()))
                {
                    layerName = LayerName.BackPosition2;
                    slot.ObjectState = PositionObjectState.Occupied;
                    break;
                }
            }
        }
        _layer = layerName;
    }
    //protected void addSlot(GameObject _obj)
    //{
    //    moveObj.Add(_obj);
    //    SlotData slotData = new SlotData()
    //    {
    //        FootholdObject = _obj.gameObject,
    //        ObjectState = PositionObjectState.Empty,
    //        //FootholdObj = null
    //    };
    //    slotDatas.Enqueue(slotData);
    //}
}
