using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player : Character
{
    public void slotinit()
    {
        Slot[] children = GetComponentsInChildren<Slot>();
        foreach (Slot child in children)
        {
            int layer = child.gameObject.layer;
            if (layer == LayerMask.NameToLayer(LayerName.BackPosition1.ToString()))
            {
                Slot slot = child;
                slot.ObjectState = PositionObjectState.Empty;
                rightObj = slot;
                slotLists.Add(rightObj);
            }
            else if (layer == LayerMask.NameToLayer(LayerName.BackPosition2.ToString()))
            {
                Slot slot = child;
                slot.ObjectState = PositionObjectState.Empty;
                reftObj = slot;
                slotLists.Add(reftObj);
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
        int count = slotLists.Count;
        for (int i = 0; i < count; i++) 
        {
             Slot slot = slotLists[i].gameObject.GetComponent<Slot>();
            if (slot.ObjectState == PositionObjectState.Empty) 
            {
                if (slot.gameObject.layer == LayerMask.NameToLayer(LayerName.BackPosition1.ToString()))
                {
                    slotlayerName = LayerName.BackPosition1;
                    slot.ObjectState = PositionObjectState.Occupied;
                    break;
                }
                else if (slot.gameObject.layer == LayerMask.NameToLayer(LayerName.BackPosition2.ToString()))
                {
                    slotlayerName = LayerName.BackPosition2;
                    slot.ObjectState = PositionObjectState.Occupied;
                    break;
                }
            }
        }
        _layer = slotlayerName;
    }

}
