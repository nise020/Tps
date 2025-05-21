using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] Item hillItem;
    [SerializeField] Item BuffItem;
    [SerializeField] Transform creatTab;
    public List<Item> Items;
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
    
    public void ItemDataAdd(Monster _monster) 
    {
        int count = Random.Range(0, Items.Count);
        GameObject go = Instantiate(Items[count].gameObject, _monster.transform);
        _monster.ItemUpdate(Items[count]);
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
