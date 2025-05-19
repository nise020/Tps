using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class InventoryManager : MonoBehaviour
{
    public DragState currentState = DragState.None;
    private ItemIcon draggingIcon;
    private GameObject draggingVisual;
    [SerializeField] RectTransform dragIcon;
    RectTransform parentRect;
    private Vector2 dragOffset;

    public void OnDown(BaseEventData eventData, ItemIcon _itemIcon, ItemData _data)
    {
        if (_itemIcon.IsEmpty()) return;

        draggingIcon = _itemIcon;

        PointerEventData pointer = eventData as PointerEventData;

        parentRect = dragIcon.parent as RectTransform;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect,pointer.position,
            CanvasInventory.worldCamera,out localPoint
        );
        //eventData.pressEventCamera
        dragIcon.anchoredPosition = localPoint;

        Image image = dragIcon.GetComponent<Image>();
        image.sprite = _data.ItemSprite;

        Color color = image.color;
        color.a = 100;
        image.color = color;

        dragIcon.SetAsLastSibling();

        dragIcon.gameObject.SetActive(true);
    }
    public void OnDrag(BaseEventData eventData)
    {
        if (dragIcon.gameObject.activeSelf)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                Input.mousePosition,
                CanvasInventory.worldCamera,
                out pos);
            dragIcon.anchoredPosition = pos;
        }
    }

    public void EndDrag(BaseEventData eventData)
    {
        if (dragIcon.gameObject.activeSelf) 
        {
            for (int row = 0; row < TabObject.Count; row++)
            {
                int count = TabObject[row].gameObject.transform.childCount;

                for (int col = 0; col < count; col++)
                {
                    Rect rectA = GetWorldRect(dragIcon);
                    Rect rectB = GetWorldRect(data[row].transforms[col]);

                    if (rectA.Overlaps(rectB, true))
                    {
                        ItemIcon icon = data[row].transforms[col].GetComponent<ItemIcon>();
                        SwapIcons(draggingIcon, icon);
                        break;
                    }
                }
            }
        }
        dragIcon.gameObject.SetActive(false);

    }


    Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners); // 0: bottom-left, 1: top-left, 2: top-right, 3: bottom-right
        Vector2 size = corners[2] - corners[0]; // top-right - bottom-left → 가로, 세로 계산
        return new Rect(corners[0], size); // bottom-left 위치, size를 가진 Rect 반환
    }
    private void SwapIcons(ItemIcon a, ItemIcon b)
    {
        if (a.IsEmpty() && b.IsEmpty()) return;

        ItemData dataA = a.LoadData();
        ItemData dataB = b.LoadData();

        a.ItemDataSwap(dataB);
        b.ItemDataSwap(dataA);

        a.UpdateVisual();
        b.UpdateVisual();



        //if (dataB == null || dataB.itemID == 0)
        //    a.Clear(); // a가 빈 칸이 된 경우 초기화

        //if (dataA == null || dataA.itemID == 0)
        //    b.Clear();
        //else 
        //{
        //    ItemData temp = a.LoadData();
        //    a.ItemDataSwap(b.LoadData());
        //    b.ItemDataSwap(temp);
        //}

        //if (!a.IsEmpty() && b.IsEmpty()) //a 
        //{
        //    //a  = Have
        //    //B  = None
        //    b.ItemDataSwap(a.LoadData());
        //    a.Clear();
        //}
        //else if (a.IsEmpty() && !b.IsEmpty()) 
        //{
        //    //a  = None
        //    //b  = Have
        //    a.ItemDataSwap(b.LoadData());
        //    b.Clear();
        //}
        //else
        //{
        //    ItemData temp = a.LoadData();
        //    a.ItemDataSwap(b.LoadData());
        //    b.ItemDataSwap(temp);
        //}
    }

    //public void BeginDrag(ItemIcon icon)
    //{
    //    ItemData date = icon.LoadData();
    //    if (date.itemID == 0) return;

    //    currentState = DragState.Dragging;
    //    draggingIcon = icon;

    //    // 드래그 시 따라다닐 시각적 이미지 생성
    //    draggingVisual = Instantiate(icon.gameObject, transform);
    //    draggingVisual.GetComponent<CanvasGroup>().blocksRaycasts = false;
    //}

    //public void UpdateDrag(Vector2 screenPos)
    //{
    //    if (draggingVisual != null)
    //        draggingVisual.transform.position = screenPos;
    //}

    //public void EndDrag(ItemIcon targetIcon)
    //{
    //    if (currentState != DragState.Dragging)
    //        return;

    //    if (targetIcon != draggingIcon)
    //    {
    //        // 아이템 교체
    //        SwapIcons(draggingIcon, targetIcon);
    //    }

    //    // 드래그 상태 초기화
    //    Destroy(draggingVisual);
    //    draggingVisual = null;
    //    draggingIcon = null;
    //    currentState = DragState.None;
    //}

   

}
