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
