using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    Image itemImage;
    Item Item;

    int id;
    new int name;
    int quantity;

    private void Start()
    {
        itemImage = GetComponentInChildren<Image>();
    }
    public void UpdateData(ItemData _data) 
    {
        id = _data.itemID;
        name = _data.itemName;
        quantity = _data.quantity;
    }
}
