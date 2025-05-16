using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class ItemIcon : MonoBehaviour
{
    //IPointerDownHandler, IPointerUpHandler, IDragHandler
    public void OnPointerDown(BaseEventData eventData)
    {

    }

    public void OnDrag(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        //Shared.InventoryManager.UpdateDrag(pointerData.position);
    }

    public void OnPointerUp(BaseEventData eventData)
    {

    }
    public void OnDrop(BaseEventData eventData)
    {

    }
    public void UpdateData(ItemData _data)
    {
        itemData = _data;
        // 아이콘 이미지, 수량 등 업데이트
    }
}
