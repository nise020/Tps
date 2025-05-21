using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class Ui_Inventory : UiBase
{
    public DragState currentState = DragState.None;
    private ItemIcon SwapItemIcon;
    //ItemData swapItemData = null;
    private GameObject draggingVisual;
    [SerializeField] RectTransform dragImageObject;
    RectTransform parentRect;
    private Vector2 dragOffset;

    [SerializeField] RectTransform inventoryTabRect;
    [SerializeField] RectTransform armorTabRect;

    Dictionary<int,ItemIcon> ItemIconData = new Dictionary<int,ItemIcon>();

    public void OnDown(BaseEventData eventData, ItemIcon _itemIcon, ItemData _data)
    {
        if (_itemIcon.IsEmpty()) return;

        //swapItemData = _data;
        SwapItemIcon = _itemIcon;
        Debug.Log($"itemType = {SwapItemIcon.itemType},{SwapItemIcon.acceptedItemType}");

        dragImageObject.transform.SetParent(CanvasInventory.transform, worldPositionStays: false);
        dragImageObject.transform.SetAsLastSibling(); // 항상 위에 그리기

        PointerEventData pointer = eventData as PointerEventData;

        parentRect = dragImageObject.parent as RectTransform;

        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect,pointer.position,
            CanvasInventory.worldCamera,out localPoint
        );
        //eventData.pressEventCamera
        dragImageObject.anchoredPosition = localPoint;

        Image image = dragImageObject.GetComponent<Image>();
        image.sprite = _data.ItemSprite;

        Color color = image.color;
        color.a = 100;
        image.color = color;

        dragImageObject.SetAsLastSibling();

        dragImageObject.gameObject.SetActive(true);
    }
    public void OnDrag(BaseEventData eventData)
    {
        if (dragImageObject.gameObject.activeSelf)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                Input.mousePosition,
                CanvasInventory.worldCamera,
                out pos);
            dragImageObject.anchoredPosition = pos;
        }
    }

    public void EndDrag(BaseEventData eventData,ItemIcon _icon, ItemData _data)
    {
        if (dragImageObject.gameObject.activeSelf)
        {
            Rect rectA = GetWorldRect(dragImageObject);
            Rect rectInventory = GetWorldRect(inventoryTabRect);
            Rect rectArmor = GetWorldRect(armorTabRect);

            if (rectA.Overlaps(rectInventory, true)) //in thr Inventory
            {
                #region Before
                //if(ItemIconData.TryGetValue(draggingIcon.IconId, out ItemIcon icon))
                //    //Dictionary<int,ItemIcon> ItemIconData
                //{
                //    if (icon == draggingIcon) continue; // 자기 자신이면 스킵

                //    Rect rectB = GetWorldRect(data[row].transforms[col]);

                //    if (rectA.Overlaps(rectB, true))
                //    {
                //        SwapIcons(draggingIcon, targetIcon);
                //        goto End;
                //    }
                //}
                //InvenIconLists;

                //for (int row = 0; row < TabObject.Count; row++)
                //{
                //    int count = TabObject[row].gameObject.transform.childCount;

                //    for (int col = 0; col < count; col++)
                //    {
                //        ItemIcon targetIcon = data[row].transforms[col].GetComponent<ItemIcon>();

                //        if (targetIcon == draggingIcon) continue; // 자기 자신이면 스킵

                //        Rect rectB = GetWorldRect(data[row].transforms[col]);

                //        if (rectA.Overlaps(rectB, true))
                //        {
                //            SwapIcons(draggingIcon, targetIcon);
                //            goto End;
                //        }
                //    }
                //}
                #endregion
                for (int row = 0; row < InvenIconLists.Count; row++)
                {
                    ItemIcon targetIcon = InvenIconLists[row].GetComponent<ItemIcon>();

                    RectTransform iconRect = targetIcon.gameObject.GetComponent<RectTransform>();

                    Rect rectB = GetWorldRect(iconRect);

                    if (targetIcon == SwapItemIcon && rectA.Overlaps(rectB, true)) continue; // 자기 자신이면 스킵

                    if (rectA.Overlaps(rectB, true))
                    {
                        SwapIcons(SwapItemIcon, targetIcon);
                        goto End;
                    }

                }

                
            }
            else if((rectA.Overlaps(rectArmor, true)))//in thr armorTab
            {
                for (int row = 0; row < ArmorObject.Count; row++) 
                {
                    ItemIcon targetIcon = ArmorObject[row].GetComponent<ItemIcon>();

                    RectTransform rect = targetIcon.gameObject.GetComponent<RectTransform>();

                    Rect rectB = GetWorldRect(rect);

                    if (targetIcon == SwapItemIcon && rectA.Overlaps(rectB, true)) continue; // 자기 자신이면 스킵

                    if (rectA.Overlaps(rectB, true))
                    {
                        installItem(SwapItemIcon, ArmorObject[row]);
                        goto End;
                    }
                }
            }

        }

        End:
        dragImageObject.gameObject.SetActive(false);
    }


    Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];

        rt.GetWorldCorners(corners); // 0: bottom-left, 1: top-left, 2: top-right, 3: bottom-right

        Vector2 size = corners[2] - corners[0]; // top-right - bottom-left → 가로, 세로 계산

        return new Rect(corners[0], size); // bottom-left 위치, size를 가진 Rect 반환
    }
    private void SwapIcons(ItemIcon _start, ItemIcon _end)
    {
        if (_start.IsEmpty() && _end.IsEmpty()) return;

        ItemData dataA = _start.LoadData();
        ItemData dataB = _end.LoadData();

        if (dataA == dataB) return;

        //더블클릭

        _start.ItemDataSwap(dataB);
        _end.ItemDataSwap(dataA);

        _start.UpdateVisual();
        _end.UpdateVisual();

        #region memo

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
        #endregion

    }
    private void installItem(ItemIcon _start, ItemIcon _end) 
    {
        if (_start.IsEmpty() && _end.IsEmpty()) return;

        ItemData dataA = _start.LoadData();
        ItemData dataB = _end.LoadData();

        if(_end.IsEquipmentSlot && dataA != null)
        {
            if (dataA.itemType != _end.acceptedItemType)
            {
                Debug.LogError("❌ 드래그한 아이템 타입이 장비 슬롯 타입과 맞지 않음");
                return;
            }
        }

        // 아이템 스왑
        _start.ItemDataSwap(dataB);
        _end.ItemDataSwap(dataA);

        _start.UpdateVisual();
        _end.UpdateVisual();

        // 이전에 장착돼 있던 아이템이 인벤으로 돌아가는 경우
        if (_start.IsEquipmentSlot && dataB != null)
        {
            StartCoroutine(AddItemDataCoroutine(dataB, null));
        }
    }
    private bool IsCompatible(IconSlotType slot, ItemType item)
    {
        switch (slot)
        {
            case IconSlotType.InvenTory:
                return true; // 인벤토리는 모든 아이템 허용
            case IconSlotType.Weapon:
                return item == ItemType.Weapon;
            case IconSlotType.Armor:
                return item == ItemType.Armor;
            case IconSlotType.Boots:
                return item == ItemType.Boots;
            case IconSlotType.Glove:
                return item == ItemType.Gloves;
            default:
                return false;
        }
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
