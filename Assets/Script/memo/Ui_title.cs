using System.Collections;
using System.Collections.Generic;//
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class Ui_title : MonoBehaviour
{
    Dictionary<int, Character> CharacterMap = new Dictionary<int, Character>();
    //Dictionary<--�ڷ����� ����ID�� �ο��ؼ� ���� ã�� ����
    //public List<Character> player = new List<Character>();
    //public List<Character> monster = new List<Character>();
    public void OnbtnTitle() //����
    {
        shared.SceneMgr.chageSecen(eScene.Lobby);
    }
    public Character GetCharacter(int index) //Dictionary ����
    {
       // CharacterMap.Add(index,player);
        if (CharacterMap.ContainsKey(index))
            return CharacterMap[index];

        CharacterMap.Remove(index);
        CharacterMap.Clear();

        var pair = CharacterMap.GetEnumerator();

        while (pair.MoveNext()) 
        {
            Character Character = pair.Current.Value;
        }

        return null;
    }
}
