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
    public string UserKey;  
    public string UserId;
    
}
public class SaveData
{
    public static int SaveCount;
    public static string PasswordData = "0";
    //public List<SaveData> saveDatas;//List ����
    //public List<int> dataCount;//List ����
    //public List<string> dataPassworld;//List ����
    //Add �� �� �ϳ��� ���
}
