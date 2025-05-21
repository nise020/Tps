using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public partial class ItemIcon : MonoBehaviour
{
    private Vector2 localMousePos;
    private RectTransform rectTransform;
    private RectTransform beforRectTransform;
    private Canvas canvas;
    private GameObject IconSwapImg;
    //IPointerDownHandler, IPointerUpHandler, IDragHandler

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        //if (EventSystem.current.IsPointerOverGameObject(Mouse.current.deviceId))
        //{
        //    Debug.Log("신 Input 시스템에서 마우스가 UI 위에 있음");
        //}
    }
    public void OnDown(BaseEventData eventData)//누름
    {
        Shared.UiManager.UI_INVENTORY.OnDown(eventData,this,itemData);
    }

    public void OnDrag(BaseEventData eventData)//따라 움직이지
    {
        Shared.UiManager.UI_INVENTORY.OnDrag(eventData);

    }
    public void OnUp(BaseEventData eventData)//땜
    {
        Shared.UiManager.UI_INVENTORY.EndDrag(eventData,this, itemData);

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
