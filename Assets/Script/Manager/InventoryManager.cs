using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
    

    public ItemDataBase itemDatas = new ItemDataBase();

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
        GameEvents.OnEnterRange += AddPrompt;
        GameEvents.OnExitRange += RemovePrompt;

    }
    private void OnDestroy()
    {
        GameEvents.OnEnterRange -= AddPrompt;
        GameEvents.OnExitRange -= RemovePrompt;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            //AddItemData();
            StartCoroutine(AddItemDataCoroutine());
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

            foreach (Transform t in tab.transform) 
            {
                ItemIcon icon = t.GetComponent<ItemIcon>();
                icon.IconId = number;

                data[row].transforms.Add(t);
                number++;
            }
            TabObject.Add(tab);
            //tab.gameObject.SetActive(false);
        }
    }
    int count = 0;//�ӽ�
    int adress = 0;//�ӽ�
    private IEnumerator AddItemDataCoroutine()
    {
        if (count >= items.Count)
            yield break;

        Item item = items[adress];
        int id = (int)item.ItemNumberValueLoad(ItemDataType.Id);

        for (int i = 0; i < TabObject.Count; i++)
        {
            Transform tab = TabObject[i].transform;

            for (int j = 0; j < tab.childCount; j++)
            {
                Transform iconTransform = tab.GetChild(j);
                ItemIcon icon = iconTransform.GetComponent<ItemIcon>();

                if (!icon.ItemDataCheck(id))
                {
                    Debug.Log($"tab = {tab},iconTransform = {iconTransform}," +
                              $"icon = {icon},item = {item},");

                    ItemData data = itemDatas.itemDatasDict.GetValueOrDefault(item);
                    icon.ItemDataCheck(data);
                    items.RemoveAt(adress);
                    yield break;
                }
                else 
                {
                    Debug.Log($"{icon}�� ID�� {id}�� �����ϴ�");
                }
                
                yield return null;
            }
        }
    }

    public void OnButten() { }


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

    public void OnDwon(PointerEventData eventData) 
    {

    }

    public void OnUP(PointerEventData eventData) 
    {

    }

    public void OnDrag(PointerEventData eventData) 
    {

    }





}
