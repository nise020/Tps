using Photon.Pun.Demo.Asteroids;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Actor
{
    Vector3 beforeMyGunTrs;//해당스크립트의 pos
    Vector3 beforeMyGunRot;//해당스크립트의 rot
    Vector3 gunObjRot;//총 오브젝트의 pos

    public Dictionary<int, GameObject> bulletData = new Dictionary<int, GameObject>();
    public int bulletcount;

    Player PLAYER;
    //GameObject playerUpperBody;
    protected override void Start()
    {
        GunBulletType();
        ui = Shared.BattelManager.ui;
        PLAYER = GetComponentInParent<Player>();
        //playerUpperBody = PLAYER.playerSpine;
        beforeMyGunTrs = gameObject.transform.position;
        beforeMyGunRot = transform.rotation.eulerAngles;

        gunObjRot = gunObj.transform.rotation.eulerAngles;
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
