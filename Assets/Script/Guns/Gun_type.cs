using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gun : Actor
{

    public int bullet;//ÃÑ¾Ë°¹¼ö
    public int nowbullet;//ÃÑ¾Ë°¹¼ö
    protected int pluse_bullet;//Ãß°¡ÇÒ ÃÑ¾Ë°¹¼ö
    protected int RelodingBullet;
    [SerializeField] GunTags GunEnumType;
    protected int srGunDmg;


    protected float ChargeingTime;//ÀåÀü
    protected float ChargeingTimer = 0.0f;

    protected float RerodingTime = 3.0f;
    protected float RerodingTimer = 0.0f;

    protected float RapidTimer = 0.0f;
    protected float RapidTime = 0.1f;
    //[SerializeField, Tooltip("Auto Butten")] Button AutoBut;
    public void GunBulletType()//ÃÑÀÇ Á¾·ù
    {
        if (GunEnumType == GunTags.MG)
        {
            bullet = 300;
            ChargeingTime = 0.1f;
        }
        else if (GunEnumType == GunTags.SMG)
        {
            bullet = 30;
            ChargeingTime = 0.2f;
        }
        else if (GunEnumType == GunTags.SR)
        {
            bullet = 5;
            //Â÷Áö ºñ·Ê ´ë¹ÌÁö ¹®±¸ Ãß°¡ ÆÈ¿ä
            ChargeingTime = 3.0f;
        }
        nowbullet = bullet;
        //RerodingBullet = bullet;
    }
    


}
