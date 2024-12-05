using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    public Image IMGBALL;
    Vector3 Input = Vector3.zero;
    Vector3 Position = Vector3.zero;

    public void OnDwon(PointerEventData eventData) 
    {
        IMGBALL.rectTransform.anchoredPosition = Vector3.zero;
    }
    public void OnUP(PointerEventData eventData)
    {
        Input = Vector3.zero;
        IMGBALL.rectTransform.anchoredPosition = Vector3.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //사망연산자?
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(IMGBALL.rectTransform, eventData.position, 
            eventData.pressEventCamera, out Vector2 localPoint)) 
        {
            localPoint.x = Input.x / IMGBALL.rectTransform.sizeDelta.x;
            localPoint.y = Input.y / IMGBALL.rectTransform.sizeDelta.y;

            Input.x = localPoint.x;
            Input.y = localPoint.y;

            Input = (Input.magnitude > 1.0f) ? Input.normalized : Input;
            Position.x = Input.x * IMGBALL.rectTransform.sizeDelta.x / 2;
            Position.y = Input.y * IMGBALL.rectTransform.sizeDelta.y / 2;

            IMGBALL.rectTransform.anchoredPosition = Position;
            //Position:이동값(수정이 필요 할수도 있다)
            //이동
        }
    }
}
