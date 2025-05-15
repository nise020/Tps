using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    Image itemImage;
    [SerializeField] Dictionary< string, Sprite> itemImageDatas = 
        new Dictionary<string, Sprite>();
    Item Item;

    int id_ItemIcon;
    string name_ItemIcon;
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
        itemImage = GetComponentInChildren<Image>();

        gameObject.SetActive(false);
    }

    public void UpdateData(ItemData _data) 
    {
        if (!gameObject.activeSelf) 
        {
            gameObject.SetActive(true);
        }

        id_ItemIcon = _data.itemID;
        name_ItemIcon = _data.itemName;
        quantity_ItemIcon = _data.quantity;

        UpdateSprite(name_ItemIcon);
    }
    
    public void UpdateSprite(string _data)
    {
        if (itemImageDatas.ContainsKey(_data)) 
        {
            Sprite sprite = itemImageDatas[_data];
            itemImage.sprite = sprite;
        }
        else
        {
            Debug.LogError($"itemImageDatas 안에{_data}에 해당하는 값이 없습니다");
        }
    }

}
