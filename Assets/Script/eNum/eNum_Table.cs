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
    Quest = 5,        // ����Ʈ ������
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
//    public int Id;          // ���� ��Ʈ ID
//    public int MaxHP;       // �ִ� ü��
//    public int MaxMP;       // �ִ� ����
//    public int Attack;      // ���ݷ�
//    public int Defense;     // ����
//    public int Speed;       // �̵� �ӵ�
//    public int CritRate;    // ġ��Ÿ Ȯ�� (0~100)
//    public int CritDamage;  // ġ��Ÿ ���� (%)
//}
