using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int itemID;
    public string itemImage;
    public int quantity;
    public ItemType itemType = ItemType.None;
    public Sprite ItemSprite;
    // 필요한 속성들 추가
}
public class ItemDataBase
{
    public List<Item> items;

    public Dictionary<Item, ItemData> itemDatasDict = new Dictionary<Item, ItemData>();

}

public class ItemSlotData
{
    public ItemIconState State = ItemIconState.None_Item_Data;
    public IconSlotType Type = IconSlotType.None;
    public ItemType AcceptedItemType = ItemType.None;
}

public class CharacterStateData
{
    public SkillState FirstSkillCheck = SkillState.SkillOff;
    public SkillState SecondSkillCheck = SkillState.SkillOff;

    public DamageEvent DamageEvent = DamageEvent.Event_Off;
    //public RunCheckState runState = RunCheckState.Walk;

}

public class PlayerStateData : CharacterStateData
{
    [Header("Player")]
    public PlayerModeState ModeState = PlayerModeState.None;
    public PlayerType PlayerType = PlayerType.None;

    [Header("Weapon")]
    public PlayerWeaponState WeaponState = PlayerWeaponState.Sword_Off;

    public PlayerAttackState AttackState = PlayerAttackState.Attack_Off;
    //public ReloadState reloadState = ReloadState.Reload_Off;//Attackdp 합류

    public PlayerWalkState WalkState = PlayerWalkState.Stop;//하나로 합칠것
    //public PlayerRunState RunState = PlayerRunState.Run_Off;

    public PlayerShitState ShitState = PlayerShitState.ShitUP;


    [Header("Etc")]//따로 분리 할것
    public AiState aIState = AiState.Search;
    public AvoidanceState avoidanceState = AvoidanceState.Avoidance_Off;


    public NpcWalkState NpcWalkState = NpcWalkState.Stop;
    public FindMoveObject objectInfo = FindMoveObject.None;
}

public class MonsterStateData : CharacterStateData
{
    public MonsterType MonsterType = MonsterType.Defolt;
    public MonsterWalkState WalkState = MonsterWalkState.Walk_Off;
    public MonsterAttackState AttackState = MonsterAttackState.Attack_Off;
}

public class ItemStateData 
{
    public ItemType itemType = ItemType.None;

    public WeaponEnum WeaponType = WeaponEnum.None;
    public PlayerType AcceptType = PlayerType.None;
}
