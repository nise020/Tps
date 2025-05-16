using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class ItemIcon : MonoBehaviour//,IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]Image itemImage;//아이템 이미지
    [SerializeField]Image itemQuantityImage;//아이템 수량 이미지

    private Dictionary< string, Sprite> itemImageDatas = new Dictionary<string, Sprite>();
    Item Item;

    ItemData itemData;
    public int IconId;

    int id_ItemIcon;
    string img_ItemIcon;
    int quantity_ItemIcon;
    //private void OnEnable()//껴질때
    //{

    //}
    //private void OnDisable()//꺼질때
    //{

    //}

    private void Start()
    {
        itemImageDatas = Shared.AtlasManager.AtlasLoad_Dictionary(AtlasType.Item);

        Debug.Log($"{gameObject},itemImageDatas.Count = {itemImageDatas.Count},Id = {IconId}");
        //gameObject.SetActive(false);
    }
    public bool ItemDataCheck(int _id) 
    {
        if (_id == 0|| id_ItemIcon == _id) 
        {
            Debug.LogError($"id_ItemIcon ={id_ItemIcon},_id = {_id} 입니다");
            return true;
        }
        else
        {
            return false;
        }

    }
    public void ItemDataCheck(ItemData _data) 
    {
        if (!gameObject.activeSelf) 
        {
            gameObject.SetActive(true);
        }

        if (_data.itemID != id_ItemIcon)
        {
            id_ItemIcon = _data.itemID;
            img_ItemIcon = _data.itemImage;
            quantity_ItemIcon = _data.quantity;

            itemData = _data;

            Debug.Log($"{gameObject},{IconId},{id_ItemIcon},{img_ItemIcon},{quantity_ItemIcon}");
            // ItemTab1,1,Number3 7x10,1
            UpdateSprite(img_ItemIcon);
        }
        else 
        {
            Debug.Log($"id_ItemIcon와 {_data.itemID}는 값이 같습니다");
        }

        
    }
    public ItemData LoadData() 
    { 
        return itemData;
    }
    public void UpdateSprite(string _data)
    {
        if (itemImageDatas.ContainsKey(_data)) 
        {
            Sprite sprite = itemImageDatas[_data];
            itemImage.sprite = sprite;
            Debug.Log($"sprite = {sprite}");
        }
        else
        {
            
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
