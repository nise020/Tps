using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class UiInventory : UiBase
{
    ItemIcon ITEMICON;
    Item ITEM;

    private void ArmorIconAdd()
    {
        armorIcon.AcceptedTypeUpdate(ItemType.Armor);
        weaponIcon.AcceptedTypeUpdate(ItemType.Weapon);
        bootsIcon.AcceptedTypeUpdate(ItemType.Boots);
        gloveIcon.AcceptedTypeUpdate(ItemType.Gloves);

        armorIcon.IsEquipmentSlot = true;
        weaponIcon.IsEquipmentSlot = true;
        bootsIcon.IsEquipmentSlot = true;
        gloveIcon.IsEquipmentSlot = true;

        armorIcon.Initialize();
        weaponIcon.Initialize();
        bootsIcon.Initialize();
        gloveIcon.Initialize();


        ArmorObject.Add(armorIcon);
        ArmorObject.Add(weaponIcon);
        ArmorObject.Add(bootsIcon);
        ArmorObject.Add(gloveIcon);
    }

    private IEnumerator AddItemDataCoroutine(ItemData _data, Item _item)
    {
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

                    if (itemDatas.itemDatasDict.ContainsKey(_item) && _item != null)
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

        int maxRows = 30;
        int IconIdNumber = 0;
        //for (int i = 0; i < maxRows; i++)
        //{
        //    data.Add(new info());
        //}

        //for (int row = 0; row < maxRows; row++)
        //{
        //    GameObject tab = Instantiate(contantsu, creatTab);

        //    int count = contantsu.transform.childCount;

        //    foreach (RectTransform t in tab.transform)
        //    {
        //        ItemIcon icon = t.GetComponent<ItemIcon>();

        //        icon.SlotDataUpdate(IconIdNumber, IconSlotType.InvenTory);

        //        icon.Initialize();

        //        ItemIconData.Add(icon.IconId, icon);
        //        InvenIconLists.Add(icon);

        //        data[row].transforms.Add(t);
        //        IconIdNumber++;
        //    }
        //    TabObject.Add(tab);
        //    //tab.gameObject.SetActive(false);
        //}


        for (int row = 0; row < maxRows; row++)
        {
            GameObject tab = Instantiate(contantsu, creatTab);

            int count = contantsu.transform.childCount;

            ItemIcon icon = tab.GetComponent<ItemIcon>();

            icon.SlotDataUpdate(IconIdNumber, IconSlotType.InvenTory);

            icon.Initialize();

            ItemIconData.Add(icon.IconId, icon);
            InvenIconLists.Add(icon);

            IconIdNumber++;
            TabObject.Add(tab);
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
