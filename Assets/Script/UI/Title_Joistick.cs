using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Title : MonoBehaviour
{
    //Dictionary<int, Character> CharacterMap = new Dictionary<int, Character>();
    //Dictionary<--자료형에 고유ID를 부여해서 쉽게 찾기 위함
    //public List<Character> player = new List<Character>();
    //public List<Character> monster = new List<Character>();

    public Joystick JOYSTICK;//카메라 셋팅 수정 필요
    //2d 기준이라서 수정 필요
    public void OnbtnTitle() //예시
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
    //public Character GetCharacter(int index) //Dictionary 예시
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
