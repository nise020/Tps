using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class Gunner : Player
{
    [Header("Aim")]
    [SerializeField] protected float recoilAmount = 0.01f; // 에임 흔들림 강도s
    [SerializeField] protected float maxAngle = 60f; // 에임 흔들림 강도s



    IEnumerator AttackColutin;
    protected override void Awake()
    {
        Id = 1;
        RenderType = ObjectRenderType.Skin;
        playerStateData.ModeState = PlayerModeState.Npc;
        playerStateData.PlayerType = PlayerType.Gunner;

        initialUpperBodyRot = UpperBody.rotation;
        base.Awake();
        
    }

    protected override void Start()
    {
        base.Start();
        MAINWEAPON = GetComponentInChildren<Gun>();
        FindWeaponObject();

        attackLayerIndex = playerAnimator.GetLayerIndex("Attack");
        //UpperBody = playerAnimator.GetBoneTransform(HumanBodyBones.Spine);
    }

    private void Update()
    {
        if (playerStateData.ModeState == PlayerModeState.Player) 
        {
            inputrocessing();
        }
    }



    protected override void shitdownCheak()
    {
        base.shitdownCheak();
    }
    Vector3 granadOriginalPos = Vector3.zero;
    protected override void FindWeaponObject()
    {
        //MeshRenderer[] Mesh = GetComponentsInChildren<MeshRenderer>();
        Weapon[] weaponData = GetComponentsInChildren<Weapon>();

        //granadObj
        //int value = LayerMask.NameToLayer(LayerName.Weapon.ToString());
        foreach (var weaponObj in weaponData)
        {
            // Weapon weapon = weaponObj.gameObject.GetComponent<Weapon>();

            WeaponType type = weaponObj.WeaponType;

            if (type == WeaponType.Main)
            {
                MainWeaponObj = weaponObj.gameObject;

                MAINWEAPON = weaponObj;//.GetComponent<Weapon>();
                MAINWEAPON.CharcterInit(this);
                MAINWEAPON.init();
            }
            else if (type == WeaponType.Sub)
            {
                SubWeaponObj = weaponObj.gameObject;

                SUBWEAPON = weaponObj;//.gameObject.GetComponent<Granad>();
                SUBWEAPON.CharcterInit(this);
                MAINWEAPON.init();
            }

        }
    }
    //protected void WeaponInit(Weapon weapon)
    //{
    //    if (weapon is Gun)
    //    {
    //        weapon.init();
    //    }
    //    else if (weapon is Granad)
    //    {
    //        weapon.init();
    //    }
    //    else if (weapon is Sword)
    //    {
    //        weapon.init();
    //    }
    //}

}

