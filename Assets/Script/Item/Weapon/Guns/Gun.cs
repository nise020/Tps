using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Weapon
{
    
    public Dictionary<int, GameObject> bulletData = new Dictionary<int, GameObject>();
    public int bulletcount;
    Player PLAYER;
    //GameObject playerUpperBody;
    private void Awake()
    {
        weaponType = WeaponEnum.Gun;
    }
    private void Start()
    {
        
        ui = Shared.BattelManager.ui;
        PLAYER = GetComponentInParent<Player>();
        magazine = Shared.GameManager.CreatTransform();

        GunBulletType();
        creatbullet();
    }
    public void creatbullet() 
    {
        while (bulletcount < bullet) 
        {
            GameObject go = Instantiate(bulletObj.gameObject, gunHoleObj.transform.position,
                    Quaternion.identity, magazine.transform);//Creat bullet
            go.SetActive(false);
            bulletData.Add(bulletcount, go);
            bulletcount++;
        }
        bulletcount = 0;
    }
    public override int ReturnTypeValue(BulletValueType _type)
    {
        if (_type == BulletValueType.NowBullet) 
        {
            return nowbullet;
        }
        else if (_type == BulletValueType.Bullet)
        {
            return bullet;
        }
        else if (_type == BulletValueType.Pluse_bullet)
        {
            return pluse_bullet;
        }
        Debug.LogError($"Not Found BulletValueType");
        return 0;
    }
    public override void ReloadClearValue() 
    {
        nowbullet = bullet;
        bulletcount = 0;
    }
    public override void ClearTypeValue(BulletValueType _type)
    {
        
    }

}
