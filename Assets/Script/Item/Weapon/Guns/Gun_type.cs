using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gun : Weapon
{

    public int maxBullet;//ÃÑ¾Ë°¹¼ö
    public int nowBullet;//ÃÑ¾Ë°¹¼ö
    protected int pluse_bullet;//Ãß°¡ÇÒ ÃÑ¾Ë°¹¼ö
    protected int RelodingBullet;
    [SerializeField] GunType GunEnumType;
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

        if (GunEnumType == GunType.SMG)
        {
            maxBullet = 30;
            ChargeingTime = 0.2f;
        }
        else if (GunEnumType == GunType.SR)
        {
            maxBullet = 5;
            //Â÷Áö ºñ·Ê ´ë¹ÌÁö ¹®±¸ Ãß°¡ ÆÈ¿ä
            ChargeingTime = 3.0f;
        }
        nowBullet = maxBullet;
        //RerodingBullet = bullet;
    }
    


}
