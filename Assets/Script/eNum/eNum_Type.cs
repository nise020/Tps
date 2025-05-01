
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
    HP,//체력
    MaxHP,//최대체력
    Speed,//이동속도
    Power,//공격력
    Defens,//방어력
    SkillCool_1,//1번 스킬쿨타임
    SkillCool_2,//2번 스킬쿨타임
    Buff,//버프
    BurstCool,//버스트 쿨타임
}

public enum GunType
{
    None,
    AR,//소총
    MG,//머신건
    SMG,//기간단총
    SG,//샷건
    SR,//저격총
}
public enum SkillType
{
    None,
    Skill1,
    Skill2,
}

public enum MouseInputType //예전에는 앞에 sScene을 붙여야 했다
{
    None,
    Click,      // GetMouseButtonDown
    Hold,       // GetMouseButton
    Release     // GetMouseButtonUp
}
public enum KeybordInputType //예전에는 앞에 sScene을 붙여야 했다
{
    Click,      // GetMouseButtonDown
    Hold,       // GetMouseButton
    Release     // GetMouseButtonUp
}

public enum MonsterType //몬스터 태그
{
    Defolt,
    Dron,
    Spider,
    Sphere
}
public enum ObjectRenderType //몬스터 태그
{
    None,
    Mesh,
    Skin
}

