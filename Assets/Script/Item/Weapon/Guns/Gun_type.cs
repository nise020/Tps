using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gun : Weapon
{

    public int bullet;//�Ѿ˰���
    public int nowbullet;//�Ѿ˰���
    protected int pluse_bullet;//�߰��� �Ѿ˰���
    protected int RelodingBullet;
    [SerializeField] GunType GunEnumType;
    protected int srGunDmg;


    protected float ChargeingTime;//����
    protected float ChargeingTimer = 0.0f;

    protected float RerodingTime = 3.0f;
    protected float RerodingTimer = 0.0f;

    protected float RapidTimer = 0.0f;
    protected float RapidTime = 0.1f;
    //[SerializeField, Tooltip("Auto Butten")] Button AutoBut;
    public void GunBulletType()//���� ����
    {

        if (GunEnumType == GunType.SMG)
        {
            bullet = 30;
            ChargeingTime = 0.2f;
        }
        else if (GunEnumType == GunType.SR)
        {
            bullet = 5;
            //���� ��� ����� ���� �߰� �ȿ�
            ChargeingTime = 3.0f;
        }
        nowbullet = bullet;
        //RerodingBullet = bullet;
    }
    


}
