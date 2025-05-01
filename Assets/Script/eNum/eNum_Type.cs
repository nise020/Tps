
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
}
public enum ObjectType
{
    None,
    Player,
    Monster,
    Gun,
    Bullet_Player,
    Bullet_Monster,
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
    Hill,
    SpeedUP,
    Weapon,

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

