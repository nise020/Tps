using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    Player PLAYER;
    Item item;
    TriggerZoneState triggerZoneState = TriggerZoneState.Trigger_Off;
    List<Item> items = new List<Item>();

    public void init(Player _player) 
    {
        PLAYER = _player;
        //PLAYER.GetItem(item);
    }
    public Queue<Item> itemsQueue = new Queue<Item>();
    private void OnTriggerEnter(Collider other)//체크박스에 닿으면 정렬에 추가
    {
        //List<Item> items = new List<Item>();
        int Weapon = LayerMask.NameToLayer(LayerName.Weapon.ToString());
        int Item = LayerMask.NameToLayer(LayerName.Item.ToString());

        if (other.gameObject.layer == Item ||
            other.gameObject.layer == Weapon) 
        {
            Item item = other.gameObject.GetComponentInParent<Item>();
            //itemsQueue.Enqueue(item);
            items.Add(item);
            ItemData data = item.dataLoad();
            Shared.UiManager.UI_INVENTORY.itemDatas.itemDatasDict.Add(item, data);

            GameEvents.OnEnterRange?.Invoke(item);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        int Weapon = LayerMask.NameToLayer(LayerName.Weapon.ToString());
        int Item = LayerMask.NameToLayer(LayerName.Item.ToString());

        if (other.gameObject.layer == Item ||
            other.gameObject.layer == Weapon)
        {
            Item item = other.gameObject.GetComponentInParent<Item>();
            if (Shared.UiManager.UI_INVENTORY.itemDatas.itemDatasDict.ContainsKey(item))
            {
                Shared.UiManager.UI_INVENTORY.itemDatas.itemDatasDict.Remove(item);
                GameEvents.OnExitRange?.Invoke(item);
            }
        }
    }
    
}
