using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossMonster : Monster
{
    protected override void Awake()
    {
        base.Awake();
        monsterStateData.MonsterType = MonsterType.Boss;
        RenderType = ObjectRenderType.Mesh;

        Id = 201;

        radius = 100;
    }
    protected override void Start()
    {
        base.Start();
        //RootTransform = transform.Find(ModelName.Model.ToString());
        FindRootBodyObject();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void FindWeaponObject()
    {
        //SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        //int value = LayerMask.NameToLayer(_name.ToString());
        //foreach (var skinObj in skin)
        //{
        //    if (skinObj.gameObject.layer == value)
        //    {
        //        MAINWEAPON = skinObj.GetComponentInParent<Weapon>();
        //        MAINWEAPON.CharcterInit(this);

        //        MainWeaponObj = skinObj.rootBone.gameObject;
        //        weaponOriginalPos = MainWeaponObj.transform.localPosition;
        //        break;
        //    }
        //}
        //int value = LayerMask.NameToLayer(_name.ToString());

        Weapon[] weapon = GetComponentsInChildren<Weapon>();
        foreach (var skinObj in weapon)
        {
            WeaponType type = skinObj.WeaponType;

            if (type == WeaponType.Main)
            {

                MAINWEAPON = skinObj;
                MAINWEAPON.CharcterInit(this);

                SkinnedMeshRenderer skin = skinObj.GetComponent<SkinnedMeshRenderer>();
                GameObject go = skin.rootBone.gameObject;

                Transform child = go.transform.GetChild(0);           
                Transform grandChild = child.GetChild(0);

                MainWeaponObj = grandChild.gameObject;
                weaponOriginalPos = MainWeaponObj.transform.localPosition;
                break;
            }
        }
    }
}
