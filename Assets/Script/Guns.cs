using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public enum GunTags 
    {
        MG,//¸Ó½Å°Ç
        SMG,//±â°£´ÜÃÑ
        SR,//Àú°ÝÃÑ
    }
    [SerializeField] public GunTags GunType;
    int bullet;
    void Start()
    {
        //GunBulletType();
    }

    private void GunBulletType()
    {
        if (GunType == GunTags.MG)
        {
            bullet = 30;
        }
        else if(GunType == GunTags.SMG) 
        {
            bullet = 300;
        }
        else if(GunType == GunTags.SR) 
        {
            bullet = 5;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
