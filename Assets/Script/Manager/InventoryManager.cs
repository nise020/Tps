using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using WebSocketSharp;

public partial class InventoryManager : MonoBehaviour 
{
    //UnityEngine.Object itemIcon => Resources.Load("Prefabs/Ui/ItemIcon");
    ItemIcon ITEMICON;

    Item ITEM;

    public class info 
    {
        public List<Transform> transforms = new List<Transform>();//slot

    }
    List<info> data = new List<info>();//��
    public List <Item> items = new List<Item>();//��� ������ ������ list
    
    //public Dictionary <int,Item> itemDatasDict = new Dictionary <int, Item>();

    public Action<Item> GetItemEvent;//������ ��� �븮��
    public Action<Item> AddItemEvent;//������ Dictionary Add �븮��
    

    public ItemDataBase itemData = new ItemDataBase();

    [SerializeField] GameObject InventoryObjct;
    [SerializeField] GameObject contantsu;
    [SerializeField] Transform creatTab;
    
    List<GameObject> TabObject = new List<GameObject>();

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
        creatInventorySlot();
        GameEvents.OnExitRange += AddPrompt;
        GameEvents.OnEnterRange += RemovePrompt;

    }
    private void OnDestroy()
    {
        GameEvents.OnEnterRange -= AddPrompt;
        GameEvents.OnExitRange -= RemovePrompt;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.K)) 
        {
            AddItemData();
        }
    }
    void AddPrompt(Item item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            CreatePromptUI(item); // ���� �ȳ� �޽��� ����
        }
    }

    void RemovePrompt(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            RemovePromptUI(item); // �ش� �޽��� ����
        }
    }

    void CreatePromptUI(Item item)
    {
        int id = (int)item.ItemNumberValueLoad(ItemDataType.Id);
        string image = item.ItemStringValueLoad(ItemDataType.Image);
        string prefabs = item.ItemStringValueLoad(ItemDataType.Prefabs);



        // item.item.name, item.item.icon �� table���� �ҷ��� UI ����
    }

    void RemovePromptUI(Item item)
    {
        // �ش� item�� �����Ǵ� UI ������Ʈ ����
    }

    private void creatInventorySlot()
    {
        //GameObject InventoryObjct; �κ��丮 �� ������ ĭ
        //Transform contantsu;//�κ��丮 �� ������ ��
        //Transform creatTab;//�κ��丮 �� contantsu�� ������ �θ��� ��ġ

        int maxRows = 6;

        for (int i = 0; i < maxRows; i++)
        {
            data.Add(new info());
        }

        for (int row = 0; row < maxRows; row++)
        {
            GameObject tab = Instantiate(contantsu, creatTab);

            int count = contantsu.transform.childCount;

            foreach (Transform t in contantsu.transform) 
            {
                data[row].transforms.Add(t);
            }
            TabObject.Add(tab);
            tab.gameObject.SetActive(false);
        }
    }
    int count = 0;
    public void AddItemData() 
    {
        Item item = items[count];

        for (int i = 0; i < TabObject.Count; i++) 
        {
            if (TabObject[i].gameObject.transform.childCount!=null) 
            {

            }
        }


        items.Remove(items[count]);
        count++;

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
        string json = JsonUtility.ToJson(ITEMICON, true); // true�� �鿩���� �ɼ�
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
            ITEMICON = new ItemIcon(); // �� �κ��丮 ����
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
