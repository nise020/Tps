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

public enum MonsterAnimParameters
{
    Walk,
    Run,
    Attack,
    Close,
    Search,
    Idle,
    Serch,
    Open,
    Stop,

}
public enum SkillRunning 
{
    SkillOn,
    SkillOff
}
public enum WeaponState 
{
    None,
    Sword_On,
    Sword_Off,

}
public enum RunState 
{
    Run_On, 
    Run_Off
}
public enum PlayerControllState
{
    Off,
    On,
}
public enum GunState 
{
    Attack,
    Reload,
}
public enum MobAnim
{
    Idle,
    Attack,
    Serch,
    AttackDilray,
    Walk
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
    BuffSkill
}
public enum CameraAnim 
{
    Shake
}
public enum Playerstate
{
    Null,
    Run,
    CloseAttack,
    ShitDown,
    Reload,
    Attack,
}
public enum Characterstate 
{
    Health,
    Dead
}
public enum eState 
{
    //HP,//체력
    //MaxHP,//최대체력
    //movespeed,//이동속도
    //attack,//공격력
    //Defens,//방어력
    SkillCool_1,//1번 스킬쿨타임
    SkillCool_2,//2번 스킬쿨타임
    Buff,//버프
    BurstCool,//버스트 쿨타임
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
public enum MonsterAiState //Monster AI 상태
{
    Create,
    Search,
    Move,
    Attack,
    Reset,
}
public enum NpcAiState //Npc AI 상태
{
    Search,
    Move,
    Attack,
    Reset,
}
public enum MonsterType //몬스터 태그
{
    Defolt,
    Dron,
    Spider,
    Sphere
}
public enum Condition //몬스터의 상태패턴
{
    health,
    hard,
}
public enum SearchState 
{
    Stop,
    Move
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
public enum PositionObjectState 
{
    Empty,
    None,
    Occupied
}
public enum PlayerWalkState 
{
    None,
    Walk_On,
    Walk_Off,
}
public enum NpcWalkState
{
    None,
    Walk,
    Run,
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
public enum InputType //예전에는 앞에 sScene을 붙여야 했다
{
    Click,      // GetMouseButtonDown
    Hold,       // GetMouseButton
    Release     // GetMouseButtonUp
}


