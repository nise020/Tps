using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Weapon
{
    public override WeaponType WeaponType => WeaponType.Main;


    public Dictionary<int, GameObject> bulletData = new Dictionary<int, GameObject>();
    public int bulletcount;
    //Player PLAYER;
    //GameObject playerUpperBody;
    private void Awake()
    {
        ItemStateData.WeaponType = WeaponclassType.Gun;
    }
    private void Start()
    {
        ui = Shared.BattelManager.ui;
        //PLAYER = GetComponentInParent<Player>();
        magazine = Shared.GameManager.CreatTransform();

    }
    public override void init()//character
    {
        GunBulletType();
        creatbullet();
    }
    public void creatbullet() 
    {
        if (bulletData.Count >= maxBullet) return;

        while (bulletcount < maxBullet) 
        {
            GameObject go = Instantiate(bulletObj.gameObject, gunHoleObj.transform.position,
                    Quaternion.identity, magazine.transform);//Creat bullet
            go.SetActive(false);
            Bullet_Player bullet= go.GetComponent<Bullet_Player>();

            bullet.CharcterInit(character);

            bulletData.Add(bulletcount, go);
            bulletcount++;
        }
        bulletcount = 0;
    }
    public override void ReloadClearValue() 
    {
        nowBullet = maxBullet;
        bulletcount = 0;
    }

    public override int ReturnTypeValue(BulletValueType _type)
    {
        if (_type == BulletValueType.NowBullet) 
        {
            return nowBullet;
        }
        else if (_type == BulletValueType.MaxBullet)
        {
            return maxBullet;
        }
        else if (_type == BulletValueType.Pluse_bullet)
        {
            return pluse_bullet;
        }
        Debug.LogError($"Not Found BulletValueType");
        return 0;
    }
    public override void ClearTypeValue(BulletValueType _type)
    {
        
    }

}
