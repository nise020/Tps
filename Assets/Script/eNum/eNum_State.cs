
//public enum AiState //Npc AI 상태
//{
//    Search,
//    Move,
//    Attack,
//    Reset,
//}
public enum ShakeMode
{
    StaticCamera,
    MoveCamera
}
public enum PlayerCameraState
{
    Rotation_Stop,
    Rotation_On
}
public enum ShakeState
{
    Shake_On,
    Shake_Off,
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
    Reload_Off
}
public enum InvincibleState
{
    invincible_On,
    invincible_Off,
}
public enum SearchState
{
    None,
    Stop,
    Move,
    TargetOn,
    Wait,
}
public enum PositionObjectState
{
    Empty,
    None,
    Occupied
}
public enum PlayerWalkState
{
    Stop,
    Walk,
    Run,
    //Walk_Off,
    Dash,
}
//public enum PlayerRunState
//{
//    None,
//    Run_On,
//    Run_Off,
//}
public enum NpcRunState
{
    None,
    Run_On,
    Run_Off,
}
//public enum RunCheckState
//{
//    Walk,
//    Run,
//}
public enum NpcWalkState
{
    Stop,
    Walk,
    Run,
}
public enum PlayerWeaponState
{
    Sword_On,
    Sword_Off,
}
public enum PlayerControllState
{
    Off,
    On,
}
public enum PlayerAttackState
{
    None,
    Attack_On,
    Attack_Off,
    Attack_Combo,
    Reload_On,
    Reload_Off,
    Block,
    //SwordOn,
    //SwordOff,

}
public enum PlayerModeState
{
    None,   
    Npc,
    Player,
    AutoMode
}
public enum TriggerZoneState
{
    Trigger_On, 
    Trigger_Off,
}

public enum DragState 
{
    None, Dragging 
}
public enum ItemIconState
{
    None_Item_Data,
    Have_a_Item_Data,
}
public enum IconSlotType
{
    None,
    Weapon,
    Armor,
    Boots,
    Glove,
    InvenTory
}
public enum UiState
{
    Ui_Off,
    Ui_On,
}
public enum DamageEvent
{
    Event_On,
    Event_Off,
}

