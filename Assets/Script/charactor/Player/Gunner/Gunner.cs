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
        charctorState = CharctorStateEnum.Npc;
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        WEAPON = GetComponentInChildren<Gun>();
        FindWeaponObject(LayerName.Weapon);
        //playerType = CharactorJobEnum.Gunner;
        //charctorState = CharctorStateEnum.Player;
        //playerControll = PlayerControllState.On; 
        //Shared.InutTableMgr();
        //Table_Charactor.Info info = Shared.TableManager.Character.Get(1);
        //Name = info.Img;
        //skillStrategy.PlayerInit(this);
        //skillStrategy.WeaponInit(gun);
    }

    private void Update()
    {
        if (charctorState == CharctorStateEnum.Player) 
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
