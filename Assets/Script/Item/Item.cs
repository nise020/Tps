using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Item : Actor
{
    protected ItemStateData ItemStateData = new ItemStateData();
    //protected ItemType Type_Item = ItemType.None;
    int ItemValue = 0;
    
    protected virtual void WeaponItemInit(Table_Item.Info _info)
    {
        
    }
    public virtual void init() 
    {

    }
    public void ItemTypeSetting(ItemType _type) 
    {
        ItemStateData.itemType = _type;
    }
    public void Iteminit(ItemType _type) 
    {
        _type = ItemStateData.itemType;
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
