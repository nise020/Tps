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
public class UserData 
{
    public string UserKey;  
    public string UserId;
    
}
public class SaveData
{
    public static int SaveCount;
    public static string PasswordData = "0";
    //public List<SaveData> saveDatas;//List 형태
    //public List<int> dataCount;//List 형태
    //public List<string> dataPassworld;//List 형태
    //Add 도 또 하나의 방법
}
