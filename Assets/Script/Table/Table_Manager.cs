
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TableManager //: MonoBehaviour
{
    public Table_Charactor Character = new Table_Charactor();
    public Table_Item Item = new Table_Item();

    public void Init() 
    {
#if UNITY_EDITOR
        Character.Init_Csv(TableType.Character.ToString(), 1, 0);
        Item.Init_Csv(TableType.Character.ToString(), 1, 0);
#else
        Character.Init_Binary(TableType.Character.ToString());
        Item.Init_Csv(TableType.Character.ToString(), 1, 0);
#endif
    }

    public void Save() 
    {
        Character.Save_Binary(TableType.Character.ToString());
        Item.Save_Binary(TableType.Character.ToString());

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }


}
