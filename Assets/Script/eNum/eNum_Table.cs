public enum TableType 
{
    Character,
    Item,
    ItemType,
    Weapon,
    WeaponType,
    State,
    CharacterClass,
    EquipmentSlot,
    Skill,
    SkillType,
}
public enum CharacterTableType : byte
{
    None = 0,
    Player = 1,                
    Companion = 2,            
    NPC = 3,                   
    Monster = 4
}
public enum CharacterClassTableType : byte
{
    None = 0,
    Warrior = 1,
    Gunner = 2,
}
public enum ItemTableType : byte
{
    None = 0,
    Consumable = 1,
    Weapon = 2,
    Armor = 3,
    Accessory = 4,
    Quest = 5,        // 퀘스트 아이템
    Etc = 6
}
public enum WeaponTableType : byte
{
    None = 0,
    Sword = 1,
    Gun = 2,    
    Staff = 3,
    Dagger = 4,
    Axe = 5,
    Spear = 6,
    Bow = 7,
}

public enum EquipmentSlotType : byte
{
    None = 0,
    Weapon = 1,
    Armor = 2,
    Helmet = 3,
    Gloves = 4,
    Boots = 5,
    Ring = 6,
    Necklace = 7
}
public enum GradeType : byte
{
    Normal = 0,
    Rare = 1,
    Epic = 2,
    Legendary = 3
}
public enum SkillTableType : byte
{
    None = 0,
    Active = 1,
    Passive = 2,
    Buff = 3,
    Debuff = 4
}
//public class StatInfo
//{
//    public int Id;          // 스탯 세트 ID
//    public int MaxHP;       // 최대 체력
//    public int MaxMP;       // 최대 마나
//    public int Attack;      // 공격력
//    public int Defense;     // 방어력
//    public int Speed;       // 이동 속도
//    public int CritRate;    // 치명타 확률 (0~100)
//    public int CritDamage;  // 치명타 배율 (%)
//}
