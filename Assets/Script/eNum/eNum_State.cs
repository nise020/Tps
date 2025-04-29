
public enum NpcAiState //Npc AI 상태
{
    Search,
    Move,
    Attack,
    Reset,
}
public enum GunState 
{
    On,
    Off,
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
    Stop,
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
    SwordOn,
    SwordOff,

}
public enum CharctorStateEnum
{
    Npc,
    Player,
    AutoMode
}
