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
    List<info> data = new List<info>();//줄
    public List <Item> itemLists = new List<Item>();//player 근처에 드랍 가능한 아이템 list(자주 추가,삭제)
    int itemListsCount = 0;//itemLists의 index

    //public Dictionary <int,Item> itemDatasDict = new Dictionary <int, Item>();

    public Action<Item> GetItemEvent;//아이템 드랍 대리자
    public Action<Item> AddItemEvent;//아이템 Dictionary Add 대리자
    

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

    int adress = 0;//임시

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

                if (icon.IsEmpty())//빈칸
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
                if (icon.HasSameItem(id))//수량 증가
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
            CreatePromptUI(item); // 실제 안내 메시지 생성
        }
    }

    void RemovePrompt(Item item)
    {
        if (itemLists.Contains(item))
        {
            itemLists.Remove(item);
            RemovePromptUI(item); // 해당 메시지 제거
        }
    }

    void CreatePromptUI(Item item)
    {
        int id = (int)item.ItemNumberValueLoad(ItemDataType.Id);
        string image = item.ItemStringValueLoad(ItemDataType.Image);
        string prefabs = item.ItemStringValueLoad(ItemDataType.Prefabs);



        // item.item.name, item.item.icon 등 table에서 불러와 UI 생성
    }

    void RemovePromptUI(Item item)
    {
        //if()
        // 해당 item에 대응되는 UI 오브젝트 제거
    }

    private void creatInventorySlot()//슬롯 생성
    {
        //GameObject InventoryObjct; 인벤토리 속 아이템 칸
        //Transform contantsu;//인벤토리 속 아이템 줄
        //Transform creatTab;//인벤토리 속 contantsu가 생성될 부모의 위치

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
