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

public enum Scene //예전에는 앞에 sScene을 붙여야 했다
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
    Soljer1,//머신건
    Soljer2,//기간단총
    Soljer3,//저격총
    Soljer4,//소총
    Soljer5,//샷건
}


public enum GroundTouchState //예전에는 앞에 sScene을 붙여야 했다
{
    GroundNoneTouch,
    GroundTouch
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



