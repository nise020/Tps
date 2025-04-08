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
    //HP,//ü��
    //MaxHP,//�ִ�ü��
    //movespeed,//�̵��ӵ�
    //attack,//���ݷ�
    //Defens,//����
    SkillCool_1,//1�� ��ų��Ÿ��
    SkillCool_2,//2�� ��ų��Ÿ��
    Buff,//����
    BurstCool,//����Ʈ ��Ÿ��
}

public enum Scene //�������� �տ� sScene�� �ٿ��� �ߴ�
{
   Title,
   Login,
   Lobby,
   Battle,
   Loading,
   End,

}
public enum MonsterAiState //Monster AI ����
{
    Create,
    Search,
    Move,
    Attack,
    Reset,
}
public enum NpcAiState //Npc AI ����
{
    Search,
    Move,
    Attack,
    Reset,
}
public enum MonsterType //���� �±�
{
    Defolt,
    Dron,
    Spider,
    Sphere
}
public enum Condition //������ ��������
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
    AR,//����
    MG,//�ӽŰ�
    SMG,//�Ⱓ����
    SG,//����
    SR,//������
}
public enum SoljerTags
{
    Soljer1,//�ӽŰ�
    Soljer2,//�Ⱓ����
    Soljer3,//������
    Soljer4,//����
    Soljer5,//����
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
public enum Layer //�������� �տ� sScene�� �ٿ��� �ߴ�
{
    Cover,
    Player,
    Bullet,
}
public enum InputType //�������� �տ� sScene�� �ٿ��� �ߴ�
{
    Click,      // GetMouseButtonDown
    Hold,       // GetMouseButton
    Release     // GetMouseButtonUp
}


