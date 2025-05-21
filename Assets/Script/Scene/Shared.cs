using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shared //메모
{
    //버그 잡거나 할때 임의 적으로 사용하기 위해서 
    //MonoBehaviour를 상속하지 않는다
    //싱글톤,static<--정적으로 접근하는 패턴,과거에 메모리를 계산하기 위해서 사용
    public static GameManager GameManager;
    public static SceneManager SceneManager;
    public static BattelManager BattelManager;
    public static AtlasManager AtlasManager;
    public static SoundManager SoundManager;
    public static ShakeCamera ShakeCamera;
    public static MainCamera MainCamera;
    //public static UI_Battle BattelUI;
    public static FaidInOut FaidInOut;
    public static InputManager InputManager;
    public static MonsterManager MonsterManager;
    public static CameraManager CameraManager;
    public static EffectManager EffectManager;
    public static ItemManager ItemManager;
    //public static Ui_Inventory InventoryManager;
    public static UiManager UiManager;

    public static TableManager TableManager;
    public static TableManager InutTableMgr() 
    {
        if (TableManager == null)
        {
            TableManager = new TableManager();
            TableManager.Init();
        }
        return TableManager;
    }
    //private void Awake()//예시
    //{
    //    shared.BattelMgr = this;
    //}

    //32bit 2에 32승 데이터 처리
    //64bit 2에 64승 데이터 처리
    //static을 최대한 사용하지 않는게 좋다
    //시용되는 메모리가 얼마나 되는지 모르기 떄문에 무거워서그럼

    //유니티,비주얼 스튜디오에 시작지점 - win main<--윈도우 컴퓨터에서 시작

    //데이터 매니저 안에 정보를 저장 하는게 렉이 덜 거린다

}
