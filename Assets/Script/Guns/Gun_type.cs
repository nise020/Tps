using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gun : Actor
{

    protected int bullet;//�Ѿ˰���
    protected int pluse_bullet;//�߰��� �Ѿ˰���
    protected int RelodingBullet;
    [SerializeField] GunTags GunEnumType;
    protected int srGunDmg;


    protected float ChargeingTime;//����
    protected float ChargeingTimer = 0.0f;

    protected float RerodingTime = 3.0f;
    protected float RerodingTimer = 0.0f;

    protected float RapidTimer = 0.0f;
    protected float RapidTime = 0.2f;
    //[SerializeField, Tooltip("Auto Butten")] Button AutoBut;
    public void GunBulletType()//���� ����
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
            //���� ��� ����� ���� �߰� �ȿ�
            ChargeingTime = 3.0f;
        }
        //RerodingBullet = bullet;
    }
    
    protected virtual void Reloding()
    {
        if (bullet == RelodingBullet) { return; }
        RerodingTimer += Time.deltaTime;
        //Debug.Log("Reroding On");
        if (RerodingTimer >= RerodingTime)
        {
            bullet = RelodingBullet;
            RerodingTimer = 0.0f;
            

            //Debug.Log("Reroding off");
        }
    }

}
