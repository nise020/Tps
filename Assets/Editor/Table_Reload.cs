using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Table_Reload : MonoBehaviour
{
    [MenuItem("CS_Util/Table/CSV &F1", false, 1)]
    [MenuItem("CS_Util/Table_2/CSV &F1", false, 1)]
    //TableMgr tableMgr = new TableMgr();
    static public void PerserTableCsv() 
    {
        Shared.TableManager = new TableManager();
        Shared.TableManager.Init();
        Shared.TableManager.Save();

    }

}
