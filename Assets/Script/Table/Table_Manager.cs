
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TableManager //: MonoBehaviour
{
    public Table_Character Character = new Table_Character();
    public Table_Item Item = new Table_Item();
    public Table_State State = new Table_State();
    public void Init() 
    {
#if UNITY_EDITOR
        Character.Init_Csv(TableType.Character.ToString(), 1, 0);
        //Item.Init_Csv(TableType.Item.ToString(), 1, 0);
        State.Init_Csv(TableType.State.ToString(), 1, 0);
#else
        Character.Init_Binary(TableType.Character.ToString());
        //Item.Init_Binary(TableType.Item.ToString());
        State.Init_Binary(TableType.State.ToString());
#endif
    }

    public void Save() 
    {
        Character.Save_Binary(TableType.Character.ToString());
        //Item.Save_Binary(TableType.Item.ToString());
        State.Save_Binary(TableType.State.ToString());
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }


}
