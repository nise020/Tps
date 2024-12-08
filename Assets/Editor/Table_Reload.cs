using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Table_Reload : MonoBehaviour
{
    [MenuItem("CS_Util/Table/CSV &F1", false, 1)]
    //TableMgr tableMgr = new TableMgr();
    static public void PerserTableCsv() 
    {
        Shared.TableMgr = new TableMgr();
        Shared.TableMgr.Init();
        Shared.TableMgr.Save();

    }

}
