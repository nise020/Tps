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
    public SkillState firstSkillCheck = SkillState.SkillOff;
    public SkillState secondSkillCheck = SkillState.SkillOff;


    public RunState runState = RunState.Walk;
}

public class PlayerStateData : CharacterStateData
{
    [Header("Player")]
    public PlayerModeState PlayerState = PlayerModeState.None;
    public PlayerType PlayerType = PlayerType.None;
    public PlayerWeaponState PlayerWeaponState = PlayerWeaponState.None;


    public PlayerWalkState PlayerWalkState = PlayerWalkState.Walk_Off;
    public PlayerRunState PlayerRunState = PlayerRunState.Run_Off;
    public PlayerShitState PlayerShitState = PlayerShitState.ShitUP;

    public ReloadState reloadState = ReloadState.ReloadOff;

    [Header("Npc")]
    public NpcAiState aIState = NpcAiState.Search;

    public NpcWalkState NpcWalkState = NpcWalkState.Stop;
    public FindMoveObject objectInfo = FindMoveObject.None;
}

public class MonsterStateData : CharacterStateData
{
    public MonsterType monsterType = MonsterType.Defolt;
}
