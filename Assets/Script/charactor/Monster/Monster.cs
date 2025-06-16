using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public partial class Monster : Character
{
    protected List<Item> ITEMLists = new List<Item>();
    protected List <GameObject> itemObjValue = new List<GameObject>();

    protected Dictionary<Item, GameObject> DropItemData = new Dictionary<Item, GameObject>();
    protected AiMonster MONSTERAI = new AiMonster();
    protected Skill_Monster SKILL = new Skill_Monster();
    
    public int mobKey = 0;

    Vector3 startPosition;

    protected bool viewHpBar = false;
    protected virtual void Awake() 
    {
        //objType = ObjectType.Monster;
    }
    protected override void Start() 
    {
        base.Start();
        FindWeaponObject(LayerName.MonsterWeapon);

        monsterAnimator = GetComponent<Animator>();

        creatTabObj = Shared.GameManager.creatTab;//¿ÀºêÁ§Æ® »ý¼º ÅÇ(ex.ÃÑ¾Ë)

        MONSTERAI.init(this, SKILL);
        //MONSTERAI.Type(monsterStateData.MonsterType);

        startPosition = charactorModelTrs.position;

        Compomentinit();
        //STATE.MonsterState(monsterType);
        //stateInIt();
        AttackEvent += AiTagetUpdate;
        StateEvent += AiStateUpdate;
    }

    protected override void FindWeaponObject(LayerName _name)
    {
        Weapon[] weaponData = GetComponentsInChildren<Weapon>();

        int value = LayerMask.NameToLayer(_name.ToString());
        foreach (var skinObj in weaponData)
        {
            //Transform weapontrs = skinObj.transform.parent;

            //MainWeaponObj = weapontrs.gameObject;

            //weaponOriginalPos = MainWeaponObj.transform.localPosition;

            //MAINWEAPON = MainWeaponObj.GetComponent<Granad>();
            //MAINWEAPON.CharcterInit(this);

            WeaponType type = skinObj.WeaponType;

            if (type == WeaponType.Main)
            {
                MAINWEAPON = skinObj.GetComponent<Weapon>();
                MAINWEAPON.CharcterInit(this);
                MainWeaponObj = skinObj.gameObject;

                weaponOriginalPos = MainWeaponObj.transform.localPosition;
            }
            else if (type == WeaponType.Sub)
            {
                SUBWEAPON = skinObj.gameObject.GetComponent<Granad>();
                SUBWEAPON.CharcterInit(this);
                SubWeaponObj = skinObj.gameObject;

                weaponOriginalPos = SubWeaponObj.transform.localPosition;
            }
        }
    }


    protected void cameraInMonsterCheck()
    {
        Player player = Shared.GameManager.PlayerLoad();
        Camera camera = player.GetComponentInChildren<Camera>();

        Vector3 viewportPos = camera.WorldToViewportPoint(gameObject.transform.position);

        bool isVisible = (viewportPos.z > 0 && viewportPos.x > 0 && 
                          viewportPos.x < 1 && viewportPos.y > 0 && 
                          viewportPos.y < 1);
        if (isVisible)
        {
            hbBarCheck(true);
        }
        else
        {
            hbBarCheck(false);
        }
    }
    
    public void KeyUpdate(int _key)
    {
        mobKey = _key;
    }
    public void ItemUpdate(Dictionary<Item, GameObject> _itemObjData) 
    {
        DropItemData = _itemObjData;

        foreach (KeyValuePair<Item, GameObject> pair in _itemObjData)
        {
            ITEMLists.Add(pair.Key);//ITEMLists <- List
            GameObject obj = pair.Value;
            obj.transform.SetParent(gameObject.transform);

        }
    }

    public override int StatusTypeLoad(StatusType _type)
    {
        int status = 0;
        status = base.StatusTypeLoad(_type);
        return status;
    }
}
