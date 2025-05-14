using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Item : Actor
{  
    protected State STATE = new State();
    protected ItemType itemType = ItemType.None;
    int ItemValue = 0;

    //protected float Range;//범위
    //리치
    protected int id;
    protected byte type;
    protected int skill;
    //protected int state;
    protected string prefabs;
    protected string img;
    protected new int name;
    protected int dec;//설명

    public void Init(Table_Item.Info _info , ItemType _type)
    {
        switch (_type) //이대로 사용하면 모노비헤이비어를 상속 못 받음
        {
            case ItemType.Weapon:
                WeaponItemInit(_info);
                break;
            case ItemType.Consumable:
                ConsumableItemInit(_info);
                break;
        }
        
    }
    protected virtual void WeaponItemInit(Table_Item.Info _info)
    {
        
    }
    protected void ConsumableItemInit(Table_Item.Info _info)
    {
        id = _info.Id;
        type = _info.Type;
        skill = _info.Skill;
        //state = _info.State;//테이블 연결 필요
        img = _info.Img;
        prefabs = _info.Prefabs;
        //name = _info.Name;//테이블 연결 필요
        //dec = _info.Dec;////테이블 연결 필요

        //Data Setting
        ItemData itemData = new ItemData();
        itemData.itemID = id;
        itemData.quantity = 1;
        //itemData.itemName = name;//테이블 연결 필요
        //itemData.icon =  img//Atlas load 필요

        //Dictionary Add
        Shared.InventoryManager.itemData.itemDatasDict.Add(this, itemData);
    }
    public void ItemTypeSetting(ItemType _type) 
    {
        itemType = _type;
    }
    public void Iteminit(ItemType _type) 
    {
        _type = itemType;
    }
    //public virtual int useitem(ItemType _type) 
    //{
    //    //switch (_type) 
    //    //{
    //    //    case ItemType.Hill:
    //    //        ItemValue = 10;
    //    //        break;
    //    //    case ItemType.SpeedUP:
    //    //        ItemValue = 5;
    //    //        break;
    //    //}
    //    //return ItemValue;
    //}
    protected virtual int RemoveEfect() 
    {
        return 0; 
    }

    public virtual float ItemStatusLoad(ItemStatusType _status)
    {
        return 0.0f;
    }
}
