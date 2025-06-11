using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    public override ObjectType Type => ObjectType.Item;

    [SerializeField] int id;
    [SerializeField] string Image;
    [SerializeField] string prefabs;
    [SerializeField] ItemType itemType = ItemType.None;

    private void Awake()//юс╫ц
    {
        id_Item = id;
        img_Item = Image;
        prefabs_Item = prefabs;//
        ItemStateData.itemType = itemType;
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
