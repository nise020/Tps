public enum TableType 
{
    Character,
    Item,
    Weapon,
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
    Bow = 2,
    Staff = 3,
    Dagger = 4,
    Axe = 5,
    Spear = 6
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
