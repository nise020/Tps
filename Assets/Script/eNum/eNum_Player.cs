using System.Collections.Generic;

public enum BulletType
{
    Mobbullet,
    Playerbullet,
    MobGranad,
}


public enum SkillState 
{
    SkillOn,
    SkillOff
}


public enum WeaponEnum 
{
    None,
    Gun,
    Sword,
    Granad,
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

public enum PlayerType
{
    None,
    Warrior,
    Gunner
}

public enum SceneName
{
    Title,
    Login,
    Lobby,
    Battle,
    Loading,
}


public enum SoljerTags
{
    Soljer1,//�ӽŰ�
    Soljer2,//�Ⱓ����
    Soljer3,//������
    Soljer4,//����
    Soljer5,//����
}


public enum GroundTouchState //�������� �տ� sScene�� �ٿ��� �ߴ�
{
    GroundNoneTouch,
    GroundTouch
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



