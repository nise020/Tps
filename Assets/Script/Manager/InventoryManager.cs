using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using static ItemIcon;

public partial class InventoryManager : MonoBehaviour 
{
    UnityEngine.Object itemIcon => Resources.Load("Prefabs/Ui/ItemTab");

    public List<Item> items;
    public Dictionary<int,Item> itemDatasDict = new Dictionary<int, Item>();

    public Action<Item> ItemEvent;

    ItemDataBase itemData = new ItemDataBase();

    public ItemIcon ITEMICON;
    private void Awake()
    {
        if (Shared.InventoryManager == null)
        {
            Shared.InventoryManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        //Debug.Log($"{itemIcon}");
        //ItemEvent += AddItem;
        //inventoryItemObject = Resources.Load("Prefabs/Ui/ItemTab");

        LoadInventory();

    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "inventory.json");
    }

    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(ITEMICON, true); // true는 들여쓰기 옵션
        File.WriteAllText(GetSavePath(), json);
        Debug.Log("Inventory saved to: " + GetSavePath());
    }

    public void LoadInventory()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ITEMICON = JsonUtility.FromJson<ItemIcon>(json);
            Debug.Log("Inventory loaded.");
        }
        else
        {
            ITEMICON = new ItemIcon(); // 새 인벤토리 생성
            Debug.Log("No save file found. Created new inventory.");
        }
    }
    public void AddItem(Item _item, int _id, int _quantity) 
    {
        items.Add(_item);
        //ItemDataBase existingItem = itemData.items.Find(item => ItemDataBase.info.itemID == _id);

        //if (existingItem != null) 
        //{
        //    //existingItem.quantity += _quantity;
        //}

    }
    public void RemoveItem(Item _item) 
    {

    }
}
