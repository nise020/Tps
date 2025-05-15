using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemData
{
    public int itemID;
    public string itemName;
    public int quantity;
    public Sprite ItemSprite;
    // �ʿ��� �Ӽ��� �߰�
}
//public Dictionary<Item, ItemData> ItemDiction = new Dictionary<Item, ItemData>();
public class ItemDataBase
{
    public List<Item> items;

    public Dictionary<Item, ItemData> itemDatasDict = new Dictionary<Item, ItemData>();

}
