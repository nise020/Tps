using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Title : MonoBehaviour
{
    //Dictionary<int, Character> CharacterMap = new Dictionary<int, Character>();
    //Dictionary<--�ڷ����� ����ID�� �ο��ؼ� ���� ã�� ����
    //public List<Character> player = new List<Character>();
    //public List<Character> monster = new List<Character>();

    public Joystick JOYSTICK;//ī�޶� ���� ���� �ʿ�
    //2d �����̶� ���� �ʿ�
    public void OnbtnTitle() //����
    {
        Shared.SceneMgr.chageScene(eScene.Lobby);
    }
    public void OnPointerdown(BaseEventData eventData) 
    {
        JOYSTICK.gameObject.SetActive(true);


//#if UNITY_ANDROID
#if UNITY_EDITOR
        JOYSTICK.transform.position = Input.mousePosition;
        //JOYSTICK.transform.position
#else
      Touch touch = Input.GetTouch(0);
      JOYSTICK.transform.position = touch.position;
#endif
//#endif
        JOYSTICK.OnDwon((PointerEventData)eventData);
    }

    public void OnpointerUP(BaseEventData eventData) 
    {
        JOYSTICK.gameObject.SetActive(true);
        JOYSTICK.OnUP((PointerEventData)eventData);
    }
    public void OnpointerDrag(BaseEventData eventData)
    {
        JOYSTICK.OnDrag((PointerEventData)eventData);
    }
    //public Character GetCharacter(int index) //Dictionary ����
    //{
    //   // CharacterMap.Add(index,player);
    //    if (CharacterMap.ContainsKey(index))
    //        return CharacterMap[index];

    //    CharacterMap.Remove(index);
    //    CharacterMap.Clear();

    //    var pair = CharacterMap.GetEnumerator();

    //    while (pair.MoveNext()) 
    //    {
    //        Character Character = pair.Current.Value;
    //    }

    //    return null;
    //}
}
