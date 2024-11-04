using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class shared //메모
{
    public static GameManager GameManager;//싱글톤,static<--정적으로 접근하는 패턴,과거에 메모리를 계산하기 위해서 사용
    public static SceneMgr SceneMgr;//싱글톤,static<--정적으로 접근하는 패턴,과거에 메모리를 계산하기 위해서 사용
    //32bit 2에 32승 데이터 처리
    //64bit 2에 64승 데이터 처리
    //static을 최대한 사용하지 않는게 좋다
    //시용되는 메모리가 얼마나 되는지 모르기 떄문에 무거워서그럼

    //유니티,비주얼 스튜디오에 시작지점 - win main<--윈도우 컴퓨터에서 시작

    //데이터 매니저 안에 정보를 저장 하는게 렉이 덜 거린다

}
