using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] Item hillItem;
    [SerializeField] Item BuffItem;
    [SerializeField] Transform creatTab;
    public List<Item> Items;
    Dictionary<Item, GameObject> itemdictionaryData = new Dictionary<Item, GameObject>();
    //Transform creatTab => Shared.GameManager.creatTab;
    private void Awake()
    {
        if (Shared.ItemManager == null)
        {
            Shared.ItemManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private Dictionary<Item, GameObject> creatItemObject()
    {
        Dictionary<Item, GameObject> itemObjectData = new Dictionary<Item, GameObject>();

        for (int iNum = 0; iNum < Items.Count; iNum++)
        {
            GameObject go = Instantiate(Items[iNum].gameObject, Vector3.zero, Quaternion.identity, creatTab);
            Item item = go.GetComponent<Item>();
            go.SetActive(false);
            itemObjectData.Add(item, go);
        }
        return itemObjectData;
    }

    public void ItemDataAdd(Monster _monster) 
    {
        Dictionary<Item, GameObject> itemObjectData = creatItemObject();

        //int count = Random.Range(0, Items.Count);

        //GameObject go = Instantiate(Items[count].gameObject,Vector3.zero,Quaternion.identity, _monster.transform);
        //go.SetActive(false);

        _monster.ItemUpdate(itemObjectData);
        
        //creat();
    }
    private void creat()
    {
        hillItem = CreatEffect(hillItem);
        BuffItem = CreatEffect(BuffItem);
    }
    private Item CreatEffect(Item _particle)
    {
        GameObject go = Instantiate(_particle.gameObject, creatTab);

        _particle = go.GetComponent<Item>();

        _particle.gameObject.SetActive(false);

        return _particle;
    }
}
