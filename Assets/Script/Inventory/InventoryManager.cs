using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public partial class InventoryManager : MonoBehaviour 
{
    UnityEngine.Object itemIcon => Resources.Load("Prefabs/Ui/ItemTab");

    public List<Item> items;
    public Dictionary<int,Item> itemData = new Dictionary<int, Item>();

    public Action<Item> ItemEvent;

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
        Debug.Log($"{itemIcon}");
        //ItemEvent += AddItem;
        //inventoryItemObject = Resources.Load("Prefabs/Ui/ItemTab");
    }
    public void AddItem(Item _item) 
    {
        items.Add(_item);
    }
    public void RemoveItem(Item _item) 
    {

    }
}
