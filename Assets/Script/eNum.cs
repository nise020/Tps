using System.Collections.Generic;

public enum bulletType
{
    Mobbullet,
        Playerbullet,
        MobGranad,
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

public enum eScene //�������� �տ� sScene�� �ٿ��� �ߴ�
{
   Title,
   Login,
   Lobby,
   Battle,
   Loading,
   End,

}
public enum eAI //AI ����
{
    Create,
    Search,
    Move,
    Attack,
    Reset,
}

public enum eMobType //���� �±�
{
    Defolt,
    Flying,
    Huge,
}

public enum GunTags
{
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

public enum LayerTag 
{
    Monster,
    Player,
    Cover,
    Bullet,
    MobBullet,
    BackGround,
}

public enum eLayer //�������� �տ� sScene�� �ٿ��� �ߴ�
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
    //public List<SaveData> saveDatas;//List ����
    //public List<int> dataCount;//List ����
    //public List<string> dataPassworld;//List ����
    //Add �� �� �ϳ��� ���
}
