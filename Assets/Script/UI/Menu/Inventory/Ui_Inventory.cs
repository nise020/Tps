using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Ui_Inventory : UiBase
{
    //UnityEngine.Object itemIcon => Resources.Load("Prefabs/Ui/ItemIcon");
    ItemIcon ITEMICON;

    Item ITEM;

    public class info 
    {
        public List<RectTransform> transforms = new List<RectTransform>();//slot
    }
    List<info> data = new List<info>();//��
    public List <Item> itemLists = new List<Item>();//player ��ó�� ��� ������ ������ list(���� �߰�,����)
    int itemListsCount = 0;//itemLists�� index

    //public Dictionary <int,Item> itemDatasDict = new Dictionary <int, Item>();

    public Action<Item> GetItemEvent;//������ ��� �븮��
    public Action<Item> AddItemEvent;//������ Dictionary Add �븮��
    

    public ItemDataBase itemDatas = new ItemDataBase();

    [SerializeField] GameObject InventoryObjct;
    [SerializeField] GameObject contantsu;
    [SerializeField] Transform creatTab;

    [SerializeField] ItemIcon armorIcon;
    [SerializeField] ItemIcon weaponIcon;
    [SerializeField] ItemIcon gloveIcon;
    [SerializeField] ItemIcon bootsIcon;

    List<GameObject> TabObject = new List<GameObject>();//InvenTorySlotList
    List<ItemIcon> ArmorObject = new List<ItemIcon>();//ArmorSlotList
    List<ItemIcon> InvenIconLists = new List<ItemIcon>();//InvenIconSlotList
    Canvas CanvasInventory;

    int adress = 0;//�ӽ�

    private void Awake()
    {
        //if (Shared.InventoryManager == null)
        //{
        //    Shared.InventoryManager = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(this);
        //}
        CanvasInventory = GetComponentInParent<Canvas>();
        dragImageObject.gameObject.SetActive(false);
        uiType = UiType.InvenTory;
    }
    protected override void Start()
    {
        base.Start();
        ArmorIconAdd();
        creatInventorySlot();
        GameEvents.OnEnterRange += AddPrompt;
        GameEvents.OnExitRange += RemovePrompt;

    }

    private void ArmorIconAdd()
    {
        armorIcon.acceptedItemType = ItemType.Armor;
        weaponIcon.acceptedItemType = ItemType.Weapon;
        bootsIcon.acceptedItemType = ItemType.Boots;
        gloveIcon.acceptedItemType = ItemType.Gloves;

        armorIcon.IsEquipmentSlot = true;
        weaponIcon.IsEquipmentSlot = true;
        bootsIcon.IsEquipmentSlot = true;
        gloveIcon.IsEquipmentSlot = true;


        ArmorObject.Add(armorIcon);
        ArmorObject.Add(weaponIcon);
        ArmorObject.Add(bootsIcon);
        ArmorObject.Add(gloveIcon);
    }

    private void OnDestroy()
    {
        GameEvents.OnEnterRange -= AddPrompt;
        GameEvents.OnExitRange -= RemovePrompt;
    }
    private void Update()
    {
        getItem();
    }
    private void getItem() 
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (itemListsCount >= itemLists.Count) return;

            Item item = itemLists[adress];
            ItemData data = itemDatas.itemDatasDict.GetValueOrDefault(item);
            int id = (int)item.ItemNumberValueLoad(ItemDataType.Id);

            StartCoroutine(AddItemDataCoroutine(data, item));
        }
    }
    private IEnumerator AddItemDataCoroutine(ItemData _data,Item _item)
    {
        //if (count >= items.Count)
        //    yield break;

        int id = 0;

        if (_item != null) 
        {
            id = (int)_item.ItemNumberValueLoad(ItemDataType.Id);
        }

        for (int i = 0; i < TabObject.Count; i++)
        {
            Transform tab = TabObject[i].transform;
            for (int j = 0; j < tab.childCount; j++)
            {
                Transform iconTransform = tab.GetChild(j);
                ItemIcon icon = iconTransform.GetComponent<ItemIcon>();

                if (icon.IsEmpty())//��ĭ
                {
                    icon.ItemDataSave(_data);

                    if (itemLists.Count != 0) 
                    {
                        itemLists.RemoveAt(adress);
                    }

                    if (itemDatas.itemDatasDict.ContainsKey(_item) && _item != null)
                    {
                        itemDatas.itemDatasDict.Remove(_item);
                        _item.gameObject.SetActive(false);
                    }

                    yield break;
                }
                if (icon.HasSameItem(id))//���� ����
                {
                    icon.IncreaseQuantity(_data);

                    if (itemLists.Count != 0)
                    {
                        itemLists.RemoveAt(adress);
                    }

                    if (itemDatas.itemDatasDict.ContainsKey(_item)&& _item != null) 
                    {
                        itemDatas.itemDatasDict.Remove(_item);
                        _item.gameObject.SetActive(false);
                    }
                    yield break;
                }


                yield return null;
            }

        }
    }


    void AddPrompt(Item item)
    {
        if (!itemLists.Contains(item))
        {
            itemLists.Add(item);
            CreatePromptUI(item); // ���� �ȳ� �޽��� ����
        }
    }

    void RemovePrompt(Item item)
    {
        if (itemLists.Contains(item))
        {
            itemLists.Remove(item);
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
        //if()
        // �ش� item�� �����Ǵ� UI ������Ʈ ����
    }

    private void creatInventorySlot()//���� ����
    {
        //GameObject InventoryObjct; �κ��丮 �� ������ ĭ
        //Transform contantsu;//�κ��丮 �� ������ ��
        //Transform creatTab;//�κ��丮 �� contantsu�� ������ �θ��� ��ġ

        int maxRows = 6;
        int number = 0;
        for (int i = 0; i < maxRows; i++)
        {
            data.Add(new info());
        }

        for (int row = 0; row < maxRows; row++)
        {
            GameObject tab = Instantiate(contantsu, creatTab);

            int count = contantsu.transform.childCount;

            foreach (RectTransform t in tab.transform) 
            {
                ItemIcon icon = t.GetComponent<ItemIcon>();

                icon.IconId = number;
                icon.iconSlotType = IconSlotType.InvenTory;

                ItemIconData.Add(icon.IconId, icon);
                InvenIconLists.Add(icon);

                data[row].transforms.Add(t);
                number++;
            }
            TabObject.Add(tab);
            //tab.gameObject.SetActive(false);
        }
    }
    
   

    public void OnButten() { }


    public void AddItem(Item _item, int _id, int _quantity) 
    {
        itemLists.Add(_item);
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
