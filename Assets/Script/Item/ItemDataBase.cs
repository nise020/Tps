using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemData
{
    public string itemName;
    public int itemID;
    public int quantity;
    public Sprite icon;
    // �ʿ��� �Ӽ��� �߰�
}

public class ItemDataBase 
{
    

    public List<Item> items;

    public Dictionary<Item, ItemData> itemDatasDict = new Dictionary<Item, ItemData>();

    //public void 
}
