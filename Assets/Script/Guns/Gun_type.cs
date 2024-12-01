using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gun : Soljer
{
    protected int srGunDmg;//SR 데미지 필요
    //[SerializeField, Tooltip("Auto Butten")] Button AutoBut;
    public void GunBulletType()//총의 종류
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
            //차지 비례 대미지 문구 추가 팔요
            ChargeingTime = 3.0f;
        }
        //RerodingBullet = bullet;
    }
    
    protected override void Reloding()
    {
        if (bullet == RelodingBullet) { return; }
        RerodingTimer += Time.deltaTime;
        Debug.Log("Reroding On");
        if (RerodingTimer >= RerodingTime)
        {
            bullet = RelodingBullet;
            RerodingTimer = 0.0f;
            

            Debug.Log("Reroding off");
        }
    }
    public void Charging()//총구 액션,(리로드 아님!)
    {
        gunHole.SetActive(false);
        ChargeingTimer += Time.deltaTime;
        if (ChargeingTimer > ChargeingTime)//마우스 땟을때 동작 필요
        {
            if (GunEnumType == GunTags.SR && Input.GetKeyUp(KeyCode.Mouse0))
            {
                Debug.Log("SR");
                gunHole.SetActive(true);
                ChargeingTimer = 0.0f;
            }
            else
            {
                Debug.Log("Not SR");
                gunHole.SetActive(true);
                ChargeingTimer = 0.0f;
            }
        }
    }
}
