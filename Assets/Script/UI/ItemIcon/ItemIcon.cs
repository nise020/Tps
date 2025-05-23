using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public partial class ItemIcon : MonoBehaviour//,IPointerDownHandler//, IPointerUpHandler, IDragHandler
{
    [SerializeField]Image itemImage;//아이템 이미지
    [SerializeField]Image itemQuantityImage;//아이템 수량 이미지

    private Dictionary< string, Sprite> itemImageDatas = new Dictionary<string, Sprite>();
    private Dictionary< int, Sprite> itemQuantityDatas = new Dictionary<int, Sprite>();
    Item Item;

    ItemData itemData = new ItemData();
    public int IconId;

    public ItemIconState itemIconState = ItemIconState.None_Item_Data;
    public IconSlotType iconSlotType = IconSlotType.None;
    public ItemType acceptedItemType = ItemType.None;
    public ItemType itemType = ItemType.None;
    public bool IsEquipmentSlot;

    [SerializeField] int item_Id_ItemIcon;
    [SerializeField] string item_Img_ItemIcon;
    [SerializeField] int item_Quantity_ItemIcon;
    Sprite DefoltSprite;
    //private void OnDisable()//꺼질때
    //{

    //}
    //private void OnEnable()//껴질때
    //{
    //    
    //    
    //}
 
    //private void Start()
    //{
    //    DefoltSprite = itemImage.sprite;//BackGround
    //    itemImageDatas = Shared.AtlasManager.AtlasLoad_Dictionary(AtlasType.Item);
    //    //itemQuantityDatas = Shared.AtlasManager.AtlasLoad_Dictionary(AtlasType.Item);<- 수량으로 바꿔야함

    //    //Debug.Log($"{gameObject},itemImageDatas.Count = {itemImageDatas.Count}," +
    //     //   $"Id = {IconId}");
    //    //gameObject.SetActive(false);
    //}
    public void Initialize() 
    {
        rectTransform = GetComponent<RectTransform>();
        DefoltSprite = itemImage.sprite;//BackGround
        itemImageDatas = Shared.AtlasManager.AtlasLoad_Dictionary(AtlasType.Item);
    }
    public bool ItemDataCheck(int _id) 
    {
        if (_id == item_Id_ItemIcon)
        {
            Debug.LogError($"id_ItemIcon ={item_Id_ItemIcon},_id = {_id} 입니다");
            return true;
        }
        else
        {
            return false;
        }

    }
    private void UpdateQuantityDisplay(int _count)
    {
        if (itemQuantityDatas.ContainsKey(_count))
        {
            Sprite sprite = itemQuantityDatas[_count];
            itemImage.sprite = sprite;
            //itemQuantityImage.sprite = sprite;
            Debug.Log($"sprite = {sprite}");
        }
        else
        {

            Debug.LogError($"itemImageDatas 안에{_count}에 해당하는 값이 없습니다");
        }
    }

    public bool HasSameItem(int targetID)
    {
        return itemIconState == ItemIconState.Have_a_Item_Data && item_Id_ItemIcon == targetID;
    }

    public bool IsEmpty()
    {
        return itemIconState == ItemIconState.None_Item_Data || itemData.itemID == 0;
    }

    public void IncreaseQuantity(ItemData _data)
    {
        item_Quantity_ItemIcon += _data.quantity;
    }

 
    public void ItemDataSave(ItemData _data) 
    {
        if (!gameObject.activeSelf) 
        {
            gameObject.SetActive(true);
        }

        item_Id_ItemIcon = _data.itemID;
        item_Img_ItemIcon = _data.itemImage;
        item_Quantity_ItemIcon = _data.quantity;
        itemType = _data.itemType;
        //acceptedItemType = _data.itemType;

        itemData = _data;

        itemIconState = ItemIconState.Have_a_Item_Data;
        UpdateSprite(item_Img_ItemIcon);

        Debug.Log($"{gameObject},{IconId},{item_Id_ItemIcon},{item_Img_ItemIcon},{item_Quantity_ItemIcon}");
    }

    private void Clear()
    {
        itemIconState = ItemIconState.None_Item_Data;
        itemType = ItemType.None;

        itemData = null;
        item_Img_ItemIcon = null;

        item_Id_ItemIcon = 0;
        item_Quantity_ItemIcon = 0;
        itemImage.sprite = DefoltSprite;
    }

    public void ItemDataSwap(ItemData _data) 
    {
        if (_data == null || _data.itemID == 0)
        {
            Clear();
            return;
        }

        item_Id_ItemIcon = _data.itemID;
        item_Img_ItemIcon = _data.itemImage;
        item_Quantity_ItemIcon = _data.quantity;
        itemType = _data.itemType;
        //acceptedItemType = _data.itemType;

        itemData = _data;

        itemIconState = ItemIconState.Have_a_Item_Data;

        UpdateSprite(_data.itemImage);
        //UpdateQuantityDisplay();//수량 이미지 교체
    }

    public void UpdateVisual()
    {
        if (itemData == null || itemData.itemID == 0)
        {
            itemImage.sprite = DefoltSprite;
            itemIconState = ItemIconState.None_Item_Data;
            item_Quantity_ItemIcon = 0;
        }
        else
        {
            itemImage.sprite = itemData.ItemSprite; // 또는 UpdateSprite(itemData.ItemSprite)
            itemIconState = ItemIconState.Have_a_Item_Data;
            item_Quantity_ItemIcon = itemData.quantity;
        }
    }

    public ItemData LoadData() 
    { 
        return itemData;
    }
    public void UpdateSprite(string _data)
    {
        //if (string.IsNullOrEmpty(_data))
        //{
        //    Debug.LogWarning("UpdateSprite called with null or empty string.");
        //    itemImage.sprite = DefoltSprite;
        //    return;
        //}
        //if (!itemImageDatas.TryGetValue(_data, out var sprite) || sprite == null)
        //{
        //    Debug.LogError($"❌ Sprite not found for key: {_data}");
        //    itemImage.sprite = null;
        //    return;
        //}

        if (itemImageDatas.ContainsKey(_data)) 
        {
            Sprite itemSprite = itemImageDatas[_data];

            itemImage.sprite = itemSprite;
            itemData.ItemSprite = itemSprite;
            //Debug.Log($"sprite: {itemSprite.name}, texture: {itemSprite.texture}");
            //itemQuantityImage.sprite = sprite;
            //Debug.Log($"sprite = {itemSprite}");
        }
        else
        {
            itemImage.sprite = DefoltSprite;
            Debug.LogError($"itemImageDatas 안에{_data}에 해당하는 값이 없습니다");
        }
    }
    public void Use_Item_Butten()
    {
        
    }
    public void Clear_Item_Butten()
    {

    }
}
