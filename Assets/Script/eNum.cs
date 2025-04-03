using System.Collections.Generic;

public enum bulletType
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
    Close,
    Shit,
    Right,
    Left
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
public enum mobAnimInfoName
{
    Idle,
    Attack,
    Serch,
    Open,
    Stop,
    Walk
}
public enum WeaponEnum 
{
    None,
    Gun,
    Sword
}
public enum playerAnimInfoName
{
    closeAttack,
    reloading
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
    Move_Player,
    Move_Monster,
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
public enum PlayerjobEnum
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
    Player
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
}

public enum Layer //�������� �տ� sScene�� �ٿ��� �ߴ�
{
    Cover,
    Player,
    Bullet,
}


