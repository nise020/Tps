using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battel_UI : Actor
{
    Camera cam;
    [SerializeField] Image MainCursur;
    [SerializeField, Tooltip("Auto 버튼")] Button AutoBut;
    [SerializeField] Text TimerImg;//제한시간 글씨
    float GameTimer = 0.0f;
    float GameTime = 60.0f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        //AutoBut.onClick.AddListener(autoOnOff);<연결필요
    }

    // Update is called once per frame
    void Update()
    {
        //cursurTrs();
        //addforse
    }
    public void autoOnOff() 
    {

    }
    private void cursurTrs() 
    {
        //Vector3 vactor = cam.ScreenToViewportPoint(Input.mousePosition);
        //Debug.Log($"{vactor}");
        //vactor.z = 0.0f;
        //MainCursur.rectTransform.position = Input.mousePosition;<--새로 생성
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawRay(Input.mousePosition, MainCursur.rectTransform.position * 100.0f);
    }
    
}
