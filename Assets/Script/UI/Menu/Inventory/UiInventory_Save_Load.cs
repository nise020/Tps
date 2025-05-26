using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class UiInventory : UiBase
{
    public void LoadObject()
    {
        //UnityEngine.Object itemIcon => Resources.Load("Prefabs/Ui/ItemIcon");
        //ItemIcon icon = itemIcon as ItemIcon;

        GameObject go = Resources.Load<GameObject>("Prefabs/Ui/ItemIcon");
        ItemIcon itemIcon = go.GetComponent<ItemIcon>();
        ITEMICON = itemIcon;
        //Debug.LogError($"ITEMICON = {ITEMICON}");
    }
    private string GetSavePath()
    {
        //return Path.Combine(Application.persistentDataPath, "inventory.json");
        return Path.Combine("/Assets/Resources/Json", "inventory.json");
    }

    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(ITEMICON, true); // true는 들여쓰기 옵션
        File.WriteAllText(GetSavePath(), json);
        Debug.Log("Inventory saved to: " + GetSavePath());
    }

    public void LoadInventory()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ITEMICON = JsonUtility.FromJson<ItemIcon>(json);
            Debug.Log("Inventory loaded.");
        }
        else
        {
            ITEMICON = new ItemIcon(); // 새 인벤토리 생성
            Debug.Log("No save file found. Created new inventory.");
        }
    }

}
