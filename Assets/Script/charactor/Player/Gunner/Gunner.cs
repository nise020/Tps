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
        PlayerStateData.ModeState = PlayerModeState.Npc;
        PlayerStateData.PlayerType = PlayerType.Gunner;
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        WEAPON = GetComponentInChildren<Gun>();
        FindWeaponObject(LayerName.Weapon);
    }

    private void Update()
    {
        if (PlayerStateData.ModeState == PlayerModeState.Player) 
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
        Weapon[] Mesh = GetComponentsInChildren<Weapon>();

        //granadObj
        int value = LayerMask.NameToLayer(_name.ToString());
        foreach (var MeshObj in Mesh)
        {
            if (MeshObj.gameObject.layer == value)//weapon
            {
                Weapon weapon = MeshObj.gameObject.GetComponent<Weapon>();

                WeaponEnum type = weapon.Weapontype();

                if (type == WeaponEnum.Sword)
                {
                    weaponObj = MeshObj.gameObject;
                }
                else if (type == WeaponEnum.Gun) 
                {
                    weaponObj = MeshObj.gameObject;
                }
                else if (type == WeaponEnum.Granad) 
                {
                    Granad granad = MeshObj.gameObject.GetComponent<Granad>();
                    granadObj = granad;
                }
            }

        }
    }
}
