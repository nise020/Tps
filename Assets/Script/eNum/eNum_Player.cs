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

public enum Scene //�������� �տ� sScene�� �ٿ��� �ߴ�
{
   Title,
   Login,
   Lobby,
   Battle,
   Loading,
   End,

}

public enum Condition //������ ��������
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
public enum Layer //�������� �տ� sScene�� �ٿ��� �ߴ�
{
    Cover,
    Player,
    Bullet,
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


