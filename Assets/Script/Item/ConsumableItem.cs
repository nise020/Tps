using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    [SerializeField] int id;
    [SerializeField] string Image;
    [SerializeField] string prefabs;
    private void Awake()//�ӽ�
    {
        id_Item = id;
        img_Item = Image;
        prefabs_Item = prefabs;//

        //id_Item = 1;
        //img_Item = "Number3 7x10";
        //prefabs_Item = "Item_Object";//

    }
}
