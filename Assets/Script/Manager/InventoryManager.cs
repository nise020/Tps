using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using static Photon.Pun.UtilityScripts.TabViewManager;

public partial class InventoryManager : MonoBehaviour 
{
    //UnityEngine.Object itemIcon => Resources.Load("Prefabs/Ui/ItemIcon");
    ItemIcon ITEMICON;

    Item ITEM;
    public List <Item> items = new List<Item>();
    //public Dictionary <int,Item> itemDatasDict = new Dictionary <int, Item>();

    public Action<Item> GetItemEvent;//아이템 드랍 대리자
    public Action<Item> AddItemEvent;//아이템 Dictionary Add 대리자
    

    public ItemDataBase itemData = new ItemDataBase();

    [SerializeField] GameObject InventoryObjct;
    [SerializeField] Transform contantsu;
    
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
        LoadObject();

        //GetItemEvent += creatInventory;
        //AddItemEvent += creatInventory;
        //creatInventory();
        //Debug.Log($"{itemIcon}");
        //ItemEvent += AddItem;
        //inventoryItemObject = Resources.Load("Prefabs/Ui/ItemTab");

        //LoadInventory();

    }

    private void creatInventory(Item _item)
    {
        creatItem();
        if (itemData.itemDatasDict.TryGetValue(ITEM,out ItemData data)) 
        {
        }
    }

    private void creatItem()
    {
        if (items.Count<=0) 
        {
            return;
        }

        GameObject tab = null ;// = Instantiate(contantsu.gameObject, InventoryObjct.transform);
        int maxTabValue = 6;
        for (int i = 0; i < items.Count; i++) 
        {
            if (i % maxTabValue == 0)
            {
                tab = Instantiate(contantsu.gameObject, InventoryObjct.transform);
            }

            if (itemData.itemDatasDict.TryGetValue(items[i], out ItemData data))
            {
                GameObject go = Instantiate(ITEMICON.gameObject, tab.transform);
                ItemIcon icon = go.GetComponent<ItemIcon>();
                icon.UpdateData(data);
                items.RemoveAt(i);
            }
            else
            {
                tab = Instantiate(contantsu.gameObject, InventoryObjct.transform);
            }
        }
        //for (int i = 0; i < itemData.itemDatasDict.Count; i++)
        //{
        //    GameObject go = Instantiate(ITEMICON.gameObject, contantsu);
        //    ItemIcon icon = go.GetComponent<ItemIcon>();
        //    if ()
        //}
        //GameObject go = Instantiate(ITEMICON.gameObject, contantsu);

    }

    public void LoadObject()
    {
        //UnityEngine.Object itemIcon => Resources.Load("Prefabs/Ui/ItemIcon");
        //ItemIcon icon = itemIcon as ItemIcon;

        GameObject go = Resources.Load<GameObject>("Prefabs/Ui/ItemIcon");
        ItemIcon itemIcon = go.GetComponent<ItemIcon>();
        ITEMICON = itemIcon;
        //Debug.LogError($"ITEMICON = {ITEMICON}");
    }
    private string GetSavePath()
    {
        //return Path.Combine(Application.persistentDataPath, "inventory.json");
        return Path.Combine("/Assets/Resources/Json", "inventory.json");
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
