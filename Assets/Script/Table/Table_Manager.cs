
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TableMgr //: MonoBehaviour
{
    public Table_Charactor Character = new Table_Charactor();

    public void Init() 
    {
#if UNITY_EDITOR
        Character.Init_Csv("Character", 1, 0);
#else
        Character.Init_Binary("Character");
#endif
    }

    public void Save() 
    {
        Character.Save_Binary("Character");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }


}
