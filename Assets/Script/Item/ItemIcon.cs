using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    

    Image itemImage;
    Item Item;

    private void Start()
    {
        itemImage = GetComponentInChildren<Image>();
    }
}
