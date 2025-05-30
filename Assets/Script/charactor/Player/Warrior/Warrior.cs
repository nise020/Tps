using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    //GameObject Weapon;
    protected override void Awake()
    {
        id = 2;
        RenderType = ObjectRenderType.Skin;
        playerStateData.ModeState = PlayerModeState.Player;
        playerStateData.PlayerType = PlayerType.Warrior;
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
        if (playerStateData.ModeState != PlayerModeState.Npc)
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
    protected override void FindWeaponObject(LayerName _name)
    {
        SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        int value = LayerMask.NameToLayer(_name.ToString());
        foreach (var skinObj in skin)
        {
            if (skinObj.gameObject.layer == value)
            {
                WEAPON = skinObj.GetComponentInParent<Weapon>();
                weaponObj = skinObj.rootBone.gameObject;
                weaponOriginalPos = weaponObj.transform.localPosition;
                break;
            }
        }
    }
}
