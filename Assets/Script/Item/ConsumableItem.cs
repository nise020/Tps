using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    [SerializeField] int id;
    [SerializeField] string Image;
    [SerializeField] string prefabs;
    [SerializeField] ItemType Type = ItemType.None;

    private void Awake()//юс╫ц
    {
        id_Item = id;
        img_Item = Image;
        prefabs_Item = prefabs;//
        Type_Item = Type;
        //id_Item = 1;
        //img_Item = "Number3 7x10";
        //prefabs_Item = "Item_Object";//

    }
    //private void Start()
    //{
        
    //}
    //private void Update()
    //{
        
    //}
}
