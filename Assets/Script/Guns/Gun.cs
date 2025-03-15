using Photon.Pun.Demo.Asteroids;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Item
{
    public Dictionary<int, GameObject> bulletData = new Dictionary<int, GameObject>();
    public int bulletcount;

    Player PLAYER;
    //GameObject playerUpperBody;
    private void Start()
    {
        GunBulletType();
        ui = Shared.BattelManager.ui;
        PLAYER = GetComponentInParent<Player>();
        magazine = Shared.BattelManager.creatTab.gameObject;

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
        Debug.Log($"{bullet}");
        bulletcount = 0;
    }

}
