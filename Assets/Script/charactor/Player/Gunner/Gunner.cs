using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class Gunner : Player
{
    protected override void Awake()
    {
        id = 1;
        RenderType = ObjectRenderType.Skin;
        playerStateData.ModeState = PlayerModeState.Npc;
        playerStateData.PlayerType = PlayerType.Gunner;
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        MAINWEAPON = GetComponentInChildren<Gun>();
        FindWeaponObject(LayerName.Weapon);
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
    protected override void FindWeaponObject(LayerName _name)
    {
        //MeshRenderer[] Mesh = GetComponentsInChildren<MeshRenderer>();
        Weapon[] weaponData = GetComponentsInChildren<Weapon>();

        //granadObj
        int value = LayerMask.NameToLayer(_name.ToString());
        foreach (var weaponObj in weaponData)
        {
            Weapon weapon = weaponObj.gameObject.GetComponent<Weapon>();

            WeaponType type = weapon.WeaponType;

            if (type == WeaponType.Main)
            {
                weapon.init();
                MAINWEAPON = weaponObj;//.GetComponent<Weapon>();
                MAINWEAPON.CharcterInit(this);
                MainWeaponObj = weaponObj.gameObject;
            }
            else if (type == WeaponType.Sub)
            {
                weapon.init();
                SUBWEAPON = weaponObj;//.gameObject.GetComponent<Granad>();
                SUBWEAPON.CharcterInit(this);
                SubWeaponObj = weaponObj.gameObject;
            }

        }
    }
    protected void WeaponInit(Weapon weapon) 
    {
        if (weapon is Gun)
        {
            weapon.init();
        }
        else if (weapon is Granad)
        {
            weapon.init();
        }
        else if (weapon is Sword) 
        {
            weapon.init();
        } 
    }
}
