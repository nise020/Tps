using System.Collections.Generic;

public enum eScene //예전에는 앞에 sScene을 붙여야 했다
{
   Title,
   Login,
   Lobby,
   Battle,
   Loading,
   End,

}
public enum eAI //예전에는 앞에 sScene을 붙여야 했다
{
    Create,
    Search,
    Move,
    Reset,
}








public enum eLayer //예전에는 앞에 sScene을 붙여야 했다
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
public abstract class PLAYER 
{
    protected abstract void nomalAttack();//순수 가상클래스
    //자식이 무조건 만들어야 하는 기능

    protected virtual void nomal_Attack() { }//순수 가상클래스
    //자식들이 사용 할수도 안할수도 있는 기능
    //재정의 할수 있다
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