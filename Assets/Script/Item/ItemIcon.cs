using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    [System.Serializable]
    public class ItemDatas
    {
        public string itemName;
        public int itemID;
        public int quantity;
        // 필요한 속성들 추가
    }

    Image itemImage;
    Item Item;

    private void Start()
    {
        itemImage = GetComponentInChildren<Image>();
    }
}
