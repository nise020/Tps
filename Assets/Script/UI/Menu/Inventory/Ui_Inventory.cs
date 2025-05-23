using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UiInventory : UiBase
{
    //UnityEngine.Object itemIcon => Resources.Load("Prefabs/Ui/ItemIcon")

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
    [SerializeField] GameObject inventoryInterfaceTab;

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
        CanvasInventory = GetComponentInParent<Canvas>();
        uiType = UiType.InvenTory;
    }
    protected override void Start()
    {
        creatInventorySlot();
        GameEvents.OnEnterRange += AddPrompt;
        GameEvents.OnExitRange += RemovePrompt;

        ArmorIconAdd();
        base.Start();
        InventoryTabCheck(false);
        dragImageObject.gameObject.SetActive(false);
    }
    public void InventoryTabCheck(bool _check) 
    {
        if (_check == true && !inventoryInterfaceTab.activeSelf) 
        {
            inventoryInterfaceTab.SetActive(true);
        }
        else if(_check == false && inventoryInterfaceTab.activeSelf)
        {
            inventoryInterfaceTab.SetActive(false);
        }
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
    public override void Open()
    {
        if (!inventoryInterfaceTab.activeSelf)
        {
            inventoryInterfaceTab.SetActive(true);
        }
        else 
        {
            inventoryInterfaceTab.SetActive(false);
        }
    }

    public override void Close()
    {
        //gameObject.SetActive(false);
        UiEvent?.Invoke(); // ���� �� �ݹ� ����
    }
}
