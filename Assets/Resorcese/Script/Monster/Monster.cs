using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : MonoBehaviour
{
    //[SerializeField] protected GameObject[] player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //MobAttackTimecheck();
        nomalAttack();
    }
}
