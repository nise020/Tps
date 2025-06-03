using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Item : Actor
{
    protected State STATE = new State();
    protected ItemDataBase itemDatas => Shared.UiManager.UI_INVENTORY.itemDatas;


    protected int id_Item;
    protected byte type_Item;
    protected int state_Item;
    protected string prefabs_Item;
    protected string img_Item;
    protected int name_Item;
    protected int dec_Item;//설명
    //Item_State
    //protected int id_Item_State;
    protected int Power;
    protected int Defense;
    protected int Speed;
    protected int CritRate;
    protected int CritDamage;
    //private void Awake()
    //{
    //    Shared.InutTableMgr();
    //    var info = Shared.TableManager.Item.Get(id_Item);
    //    if (info == null)
    //    {
    //        Debug.LogError($"{gameObject}.info = null");
    //    }
    //    else
    //    {
    //        Init(info, type_Item);
    //        STATE.init(this, state_Item);
    //        stateInIt();
    //    }
    //}
    //protected float Range;//범위
    //리치


    public void Init(Table_Item.Info _info, byte _type)
    {
        ////switch (_type)
        ////{
        ////    case ItemType.Weapon:
        ////        WeaponItemInit(_info);
        ////        break;
        ////    case ItemType.Consumable:
        ////        ConsumableItemInit(_info);
        ////        break;
        ////}

    }
    protected void ConsumableItemInit(Table_Item.Info _info)
    {
        id_Item = _info.Id;
        type_Item = _info.Type;
        img_Item = _info.Img;
        prefabs_Item = _info.Prefabs;
        state_Item = _info.State;//테이블 연결 필요
        //name = _info.Name;//테이블 연결 필요
        //dec = _info.Dec;////테이블 연결 필요

        //Data Setting

        ItemData itemData = new ItemData();
        itemData.itemID = id_Item;
        itemData.quantity = 1;
        //itemData.itemName = name;//테이블 연결 필요
        //itemData.icon =  img//Atlas load 필요

        //Dictionary Add
        Shared.UiManager.UI_INVENTORY.itemDatas.itemDatasDict.Add(this, itemData);
    }
    public ItemData dataLoad() 
    {
        ItemData itemData = new ItemData();
        itemData.itemID = id_Item;
        itemData.itemImage = img_Item;
        itemData.quantity = 1;
        itemData.itemType = ItemStateData.itemType;
        return itemData;
    } 
    public float ItemNumberValueLoad(ItemDataType _type)
    {
        switch (_type)
        {
            case ItemDataType.Id:
                return id_Item;
            case ItemDataType.Name:
                return name_Item;
            case ItemDataType.Dec:
                return dec_Item;


            default:
                return 0.0f;
        }
    }
    public string ItemStringValueLoad(ItemDataType _type)
    {
        switch (_type)
        {
            case ItemDataType.Prefabs:
                return prefabs_Item;
            case ItemDataType.Image:
                return img_Item;
            default:
                return "";
        }
    }
}
