using System.Collections.Generic;

public enum BulletType
{
    Mobbullet,
    Playerbullet,
    MobGranad,
}
public enum PlayerAnimParameters 
{
    Walk,
    Run,
    Reload,
    Attack,
    Back,
    Shit,
    Right,
    Left,
    WeaponWalk,
    Close,

}

public enum SkillRunning 
{
    SkillOn,
    SkillOff
}


public enum WeaponEnum 
{
    None,
    Gun,
    Sword
}

public enum PlayerAnimName
{
    Attack,
    closeAttack,
    reloading,
    AttackSkill,
    BuffSkill,
    Shit,

}
public enum CameraAnim 
{
    Shake
}
public enum PlayerCameraMode
{
    CameraRotationMode,
    GunAttackMode,
}

public enum Scene //예전에는 앞에 sScene을 붙여야 했다
{
   Title,
   Login,
   Lobby,
   Battle,
   Loading,
   End,

}

public enum Condition //몬스터의 상태패턴
{
    health,
    hard,
}

public enum CharactorJobEnum
{
    None,
    Warrior,
    Gunner
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
public enum SceneName
{
    Title,
    Login,
    Lobby,
    Battle,
    Loading,
}
public enum CharctorStateEnum 
{
    Npc,
    Player,
    AutoMode
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
public enum SoljerTags
{
    Soljer1,//머신건
    Soljer2,//기간단총
    Soljer3,//저격총
    Soljer4,//소총
    Soljer5,//샷건
}

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
    Sward,
    ViewPoint,
    BackPosition1,
    BackPosition2
}

public enum FindMoveObject
{
    None,
    Find
}
public enum Layer //예전에는 앞에 sScene을 붙여야 했다
{
    Cover,
    Player,
    Bullet,
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


