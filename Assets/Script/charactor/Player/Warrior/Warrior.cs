using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    //GameObject Weapon;
    protected override void Awake()
    {
        Id = 2;
        RenderType = ObjectRenderType.Skin;
        playerStateData.ModeState = PlayerModeState.Player;
        playerStateData.PlayerType = PlayerType.Warrior;
        base.Awake();
    }
    void OnDrawGizmosSelected()
    {
        if (MainWeaponObj != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(SkillParentObj1.transform.position, 3.5f);
            
            Gizmos.DrawLine(gameObject.transform.position, 
                gameObject.transform.position - Vector3.down);
        }
    }
    protected override void Start()
    {
        base.Start();
        playerStateData.WeaponState = PlayerWeaponState.Sword_Off;
    }

    //protected void OnAnimatorMove()
    //{
    //    //Vector3 rotatedDeltaPos = transform.rotation * playerAnim.deltaPosition;
    //    //charactorModelTrs.position += rotatedDeltaPos;

    //    //charactorModelTrs.position += playerAnim.deltaPosition;
    //    //if()

    //    //charactorModelTrs.rotation *= playerAnim.deltaRotation;

    //    //RootTrransform.position += playerAnim.deltaPosition;
    //    //RootTrransform.rotation *= playerAnim.deltaRotation;
    //}

    private void Update()
    {
        if (playerStateData.ModeState != PlayerModeState.AutoMode)
        {
            //transform.rotation = new Quaternion();
            inputrocessing();
        }
        else 
        {
            return;
        }
        
    }
    
    protected override void shitdownCheak()
    {
        base.shitdownCheak();
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

        Weapon[] weapon = GetComponentsInChildren<Weapon>();
        //int value = LayerMask.NameToLayer(LayerName.Weapon.ToString());
        foreach (var skinObj in weapon)
        {
            WeaponType type = skinObj.WeaponType;

            if (type == WeaponType.Main)
            {
                MAINWEAPON = skinObj;
                MAINWEAPON.CharcterInit(this);

                SkinnedMeshRenderer skin = skinObj.GetComponent<SkinnedMeshRenderer>();
                MainWeaponObj = skin.rootBone.gameObject;
                weaponOriginalPos = MainWeaponObj.transform.localPosition;
                break;
            }
        }
    }
}
