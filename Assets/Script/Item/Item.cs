using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Item : Actor
{
    protected ItemType itemType = ItemType.None;
    int ItemValue = 0;
    public void ItemTypeSetting(ItemType _type) 
    {
        itemType = _type;
    }
    public void Iteminit(ItemType _type) 
    {
        _type = itemType;
    }
    public virtual int useitem(ItemType _type) 
    {
        switch (_type) 
        {
            case ItemType.Hill:
                ItemValue = 10;
                break;
            case ItemType.SpeedUP:
                ItemValue = 5;
                break;
        }
        return ItemValue;
    }
    protected virtual int RemoveEfect() 
    {
        return 0; 
    }
}
