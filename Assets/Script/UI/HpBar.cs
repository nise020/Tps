using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public partial class HpBar : MonoBehaviour 
{
    public int key = 0;
    Charactor Charactor;
    int hpValue = 0;
    public void inIt(Charactor charactor) 
    {
        Charactor = charactor;
        HpImage(Charactor);
    }
    public void HpImage(Charactor charactor) 
    {
        //charactor.
    }
    private void Update()
    {
        Vector3 pos = Shared.BattelMgr.monsterData[key].transform.position;
        pos.y = pos.y + 3;
        transform.position = pos;
    }
}
