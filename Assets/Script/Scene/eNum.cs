using System.Collections.Generic;

public enum eScene //�������� �տ� sScene�� �ٿ��� �ߴ�
{
   Title,
   Login,
   Lobby,
   Battle,
   Loading,
   End,

}
public enum eAI //�������� �տ� sScene�� �ٿ��� �ߴ�
{
    Create,
    Search,
    Move,
    Reset,
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
public abstract class PLAYER 
{
    protected abstract void nomalAttack();//���� ����Ŭ����
    //�ڽ��� ������ ������ �ϴ� ���

    protected virtual void nomal_Attack() { }//���� ����Ŭ����
    //�ڽĵ��� ��� �Ҽ��� ���Ҽ��� �ִ� ���
    //������ �Ҽ� �ִ�
    //
}
public class SWORD : PLAYER
{
    protected int hp;
    protected override void nomalAttack() 
    {

    }
    protected override void nomal_Attack()
    {

    }
}