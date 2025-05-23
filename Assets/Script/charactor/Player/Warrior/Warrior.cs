using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    GameObject Weapon;
    protected override void Awake()
    {
        id = 2;
        RenderType = ObjectRenderType.Skin;
        charctorState = CharctorStateEnum.Player;
        base.Awake();
    }
    void OnDrawGizmosSelected()
    {
        if (weaponObj != null)
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
        weaponState = WeaponState.Sword_Off;

        //FindRootBodyObject();

        //Shared.InutTableMgr();
        //Table_Charactor.Info info = Shared.TableManager.Character.Get(0);
        //Name = info.Img;
        //skillStrategy.WeaponInit(gun);
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
        //inputrocessing();
        //move(charctorState);
        //runcheck(RunCheck);

        //groundCheak();
        if (charctorState != CharctorStateEnum.Npc)
        {
            transform.rotation = new Quaternion();
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
    protected override void FindWeaponObject(LayerName _name)
    {
        SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        int value = LayerMask.NameToLayer(_name.ToString());
        foreach (var skinObj in skin)
        {
            if (skinObj.gameObject.layer == value)
            {
                weaponObj = skinObj.rootBone.gameObject;
                weaponOriginalPos = weaponObj.transform.localPosition;
                break;
            }
        }
    }
}
