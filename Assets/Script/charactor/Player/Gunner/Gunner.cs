using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class Gunner : Player
{
    [Header("Gunner")]
    [SerializeField] Transform UpperBody;
    [SerializeField] private float maxPitch = 30f;      // 상체 최대 회전
    [SerializeField] private float UpperrotationSpeed = 30f; // 상체 회전 부드러움
    [SerializeField] private float recoilAmount = 0.01f; // 에임 흔들림 강도s
    private bool forceUpperBody;
    private Vector3 cachedUpperBodyEuler;
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
    private void LateUpdate()
    {
        if (forceUpperBody)
        {
            UpperBody.localEulerAngles = cachedUpperBodyEuler;
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
