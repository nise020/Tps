
public enum LayerName
{
    None,
    Monster,
    Player,
    Cover,
    Bullet,
    MobBullet,
    MobGranid,
    BackGround,
    SpawnSpider,
    SpawnDron,
    SpawnSphere,
    Weapon,
    ViewPoint,
    BackPosition1,
    BackPosition2,
    Ground,
    MonsterMovePosition,
    HpBar,
    RootBody,
    Body,
    WeaponHand,
    TriggerZone,
    Item,
}
public enum ModelName 
{
    Model,
}
public enum ObjectType
{
    None,
    Player,
    Monster,
    Gun,
    Bullet_Player,
    Bullet_Monster,
    Item,
}
public enum UiType
{
    None,
    Menu,
    InvenTory,
}
public enum ButtenType
{
    None,
    Warror_Change_Butten,
    Gunne_Change_Buttenr,
}
public enum BodyType 
{
    RightHand,
    LeftHand,
    RightFoot,
    LeftFoot,
}
public enum BulletValueType 
{
    Bullet,
    NowBullet,
    Pluse_bullet,
}
public enum WeaponType
{
    Sowrd,
    Gun,
}
public enum SkillObjType
{
    Sowrd,
    Gun,
}
public enum ItemType
{
    None,
    Material,
    QuestItem,
    Weapon,
    Armor,
    Boots,
    Gloves,
    Consumable,

}
public enum ItemDataType
{
    Id,
    Type,
    Skill,
    State,
    Prefabs,
    Image,
    Name,
    Dec,//����

}
public enum EffectType
{
    None,
    SowrdEffect,
    GunEffect,
    BoomEffect,
}
public enum StatusType
{
    None,
    HP,//ü��
    MaxHP,//�ִ�ü��
    Speed,//�̵��ӵ�
    Power,//���ݷ�
    Defens,//����
    SkillCool_1,//1�� ��ų��Ÿ��
    SkillCool_2,//2�� ��ų��Ÿ��
    Buff,//����
    BurstCool,//����Ʈ ��Ÿ��
    CritRate,//ũ��Ȯ��
    CritDamage,//ũ�� ������
}
public enum ItemStatusType
{
    None,
    Range,
    Power,
    Defense,
    Speed,
}
public enum GunType
{
    None,
    AR,//����
    MG,//�ӽŰ�
    SMG,//�Ⱓ����
    SG,//����
    SR,//������
}
public enum SkillType
{
    None,
    Skill1,
    Skill2,
}

public enum MouseInputType //�������� �տ� sScene�� �ٿ��� �ߴ�
{
    None,
    Click,      // GetMouseButtonDown
    Hold,       // GetMouseButton
    Release     // GetMouseButtonUp
}
public enum KeybordInputType //�������� �տ� sScene�� �ٿ��� �ߴ�
{
    Click,      // GetMouseButtonDown
    Hold,       // GetMouseButton
    Release     // GetMouseButtonUp
}

public enum MonsterType //���� �±�
{
    Defolt,
    Dron,
    Spider,
    Sphere
}
public enum ObjectRenderType //���� �±�
{
    None,
    Mesh,
    Skin
}
public enum ShaderOptionType
{
    _MainTex,
    _TintColor,
    _SubTex,
}
public enum AtlasType
{
    Damage,
    Item
}