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
public enum AiState //AI 상태
{
    Create,
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
public enum ObjectType 
{
    None,
    Player,
    Monster,
    Gun,
    Bullet_Player,
    Bullet_Monster,
}
public enum GunTags
{
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

public enum LayerTag 
{
    Monster,
    Player,
    Cover,
    Bullet,
    MobBullet,
    MobGranid,
    BackGround,
}

public enum Layer //예전에는 앞에 sScene을 붙여야 했다
{
    Cover,
    Player,
    Bullet,
}

public class UserData 
{
    public static int acount = 3;  
    public static string UserKey;  
    public static string UserId;
    
}
public class SaveData
{
    public string IdData;
    public string PasswordData = "0";
    //public List<SaveData> saveDatas;//List 형태
    //public List<int> dataCount;//List 형태
    //public List<string> dataPassworld;//List 형태
    //Add 도 또 하나의 방법
}
