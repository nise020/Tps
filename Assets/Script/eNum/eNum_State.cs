
public enum NpcAiState //Npc AI 상태
{
    Search,
    Move,
    Attack,
    Reset,
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
public enum PlayerShitState
{
    None,
    ShitDown,
    ShitUP,
}
public enum ReloadState
{
    None,
    ReloadOn,
    ReloadOff
}
public enum Characterstate
{
    Health,
    Dead
}
public enum SearchState
{
    Stop,
    Move
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
public enum PlayerRunState
{
    None,
    Run_On,
    Run_Off,
}
public enum NpcRunState
{
    None,
    Run_On,
    Run_Off,
}
public enum RunState
{
    Walk,
    Run,
}
public enum NpcWalkState
{
    None,
    Walk,
    Run,
}
public enum WeaponState
{
    None,
    Sword_On,
    Sword_Off,

}
public enum PlayerControllState
{
    Off,
    On,
}
public enum AttackState
{
    None,
    AttackOn,
    AttackOff,
    Reload,
}