using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    bool CameraShake = false; // 카메라가 흔들리는 상태인지 여부를 나타내는 변수

    Transform ShakeTr; // 카메라 흔들림을 적용할 Transform

    public class cShakeInfo  // 카메라 흔들림 정보를 저장하는 클래스
    {
        public float StartDelay; // 흔들림 시작 전 지연 시간
        public bool UseTotalTime; // 전체 흔들림 시간을 사용할지 여부
        public float TotalTime; // 전체 흔들림 지속 시간
        public Vector3 Dest; // 흔들림의 목표 위치
        public Vector3 Shake; // 흔들림 크기 벡터
        public Vector3 Dir; // 흔들림 방향

        public float RemainDist; // 남은 이동 거리
        public float RemainCountDis; // 남은 횟수 당 이동 거리

        public bool UseCount; // 흔들림을 특정 횟수로 제한할지 여부
        public int Count; // 흔들림 횟수

        public float Veclocity; // 흔들림 속도

        public bool UseDamping; // 댐핑을 사용할지 여부
        public float Damping; // 댐핑 계수
        public float DampingTime; // 댐핑 시간
    }

    public cShakeInfo ShakeInfo = new cShakeInfo(); // 카메라 흔들림 정보 인스턴스

    Vector3 Orgpos; // 카메라의 원래 위치

    float FovX = 0.2f; // 좌우 이동 범위 제한 값
    float FovY = 0.2f; // 상하 이동 범위 제한 값

    float Left = -1.0f; // 왼쪽 경계값
    float Right = 1.0f; // 오른쪽 경계값

    private void Awake()
    {
        Shared.ShakeCamera = this; // ShakeCamera 인스턴스를 공유 변수에 저장
        Orgpos = transform.position; // 초기 카메라 위치 저장
        InitShake(); // 초기화 함수 호출
    }

    private void InitShake()
    {
        ShakeTr = transform.parent; // 흔들림을 적용할 Transform 설정
        CameraShake = false; // 초기 상태를 흔들림 없음으로 설정
    }

    private void ResetShakeTr()
    {
        transform.rotation = Quaternion.identity;
        ShakeTr.localRotation = Quaternion.identity;
        ShakeTr.localPosition = Vector3.zero; // 카메라 위치를 초기화<- 여기 주의
        CameraShake = false; // 흔들림 상태 해제
        CameraLimit(); // 카메라 위치 제한 적용<- 여기 주의
    }

    void CameraLimit(bool _OrgPosY = false)
    {

        Vector3 camera = Orgpos; // 초기 카메라 위치 사용

        if (camera.x - FovX < Left) // 왼쪽 제한 검사
            camera.x = Left + FovX;
        else if (camera.x + FovX > Right) // 오른쪽 제한 검사
            camera.x = Right - FovX;

        if (_OrgPosY) // Y축 초기 위치 유지 여부
            camera.y = Orgpos.y;
    }

    public void Shake(int _CameraID,int _count)
    {

        ShakeInfo.StartDelay = 0f; // 흔들림 지연 시간 초기화
        ShakeInfo.TotalTime = 3f; // 흔들림 지속 시간 설정
        ShakeInfo.UseTotalTime = true; // 전체 시간 사용 설정

        ShakeInfo.Shake = new Vector3(0.3f, 0.3f, 0f); // 흔들림 크기 설정
        ShakeInfo.Dest = ShakeInfo.Shake; // 초기 목표 위치 설정
        ShakeInfo.Dir = ShakeInfo.Shake; // 초기 방향 설정
        ShakeInfo.Dir.Normalize(); // 방향 벡터 정규화

        ShakeInfo.RemainDist = ShakeInfo.Shake.magnitude; // 남은 이동 거리 계산
        ShakeInfo.RemainCountDis = float.MaxValue; // 남은 거리 초기화

        ShakeInfo.Veclocity = 10; // 흔들림 속도 설정

        ShakeInfo.Damping = 0.5f; // 댐핑 계수 설정
        ShakeInfo.UseDamping = true; // 댐핑 사용 설정
        ShakeInfo.DampingTime = ShakeInfo.RemainDist / ShakeInfo.Veclocity; // 댐핑 시간 계산

        ShakeInfo.Count = _count; // 흔들림 횟수 설정
        ShakeInfo.UseCount = true; // 횟수 사용 설정

        StopCoroutine("ShakeCoroutin"); // 기존 코루틴 중지
        ResetShakeTr(); // 흔들림 초기화
        StartCoroutine("ShakeCoroutin"); // 흔들림 코루틴 시작
    }

    public IEnumerator ShakeCoroutin()
    {
        CameraShake = true; // 흔들림 상태 활성화

        float dt, dist;

        if (ShakeInfo.StartDelay > 0)
            yield return new WaitForSeconds(ShakeInfo.StartDelay); // 지연 시간 대기

        while (true)
        {
            dt = Time.fixedDeltaTime; // 고정 델타 타임 사용
            dist = dt * ShakeInfo.Veclocity; // 현재 프레임 이동 거리 계산

            if ((ShakeInfo.RemainDist -= dist) > 0) // 남은 거리 감소
            {
                ShakeTr.localPosition += ShakeInfo.Dir * dist; // 이동 적용

                float rc = transform.position.x - FovX - Left; // 경계 검사

                if (rc < 0f)
                { ShakeTr.localPosition += new Vector3(-rc, 0, 0); } // 왼쪽 제한 적용

                rc = Right - (transform.position.x + FovX);

                if (rc < 0)
                    ShakeTr.localPosition += new Vector3(rc, 0, 0); // 오른쪽 제한 적용

                CameraLimit(true); // 카메라 위치 제한 적용

                if (ShakeInfo.UseCount)
                {
                    if ((ShakeInfo.RemainCountDis -= dist) < 0)
                    {
                        ShakeInfo.RemainCountDis = float.MaxValue; // 횟수 거리 초기화

                        if (--ShakeInfo.Count < 0) // 흔들림 횟수 감소
                        {
                            break; // 반복 종료
                        }
                    }
                }
            }
            else
            {
                if (ShakeInfo.UseDamping) // 댐핑 사용 여부
                {
                    float disdamping = Mathf.Max(
                        ShakeInfo.Damping * ShakeInfo.DampingTime,
                        ShakeInfo.Damping * dt);

                    if (ShakeInfo.Shake.magnitude > disdamping)
                    { ShakeInfo.Shake -= ShakeInfo.Dir * disdamping; }
                    else
                    {
                        ShakeInfo.UseCount = true;
                        ShakeInfo.Count = 1; // 남은 횟수 설정
                    }
                }

                ShakeTr.localPosition = ShakeInfo.Dest - ShakeInfo.Dir *
                    (-ShakeInfo.RemainDist); // 최종 위치 설정

                float rc = transform.position.x - FovX - Left;

                if (rc < 0)
                    ShakeTr.localPosition += new Vector3(-rc, 0, 0); // 왼쪽 제한 적용

                rc = Right - (transform.position.x + FovX);

                if (rc < 0)
                    ShakeTr.localPosition += new Vector3(rc, 0, 0); // 오른쪽 제한 적용

                CameraLimit(true); // 카메라 위치 제한

                ShakeInfo.Shake = -ShakeInfo.Shake; // 방향 반전
                ShakeInfo.Dest = ShakeInfo.Shake; // 새로운 목표 위치
                ShakeInfo.Dir = -ShakeInfo.Dir; // 방향 벡터 반전

                float len = ShakeInfo.Shake.magnitude;

                ShakeInfo.RemainCountDis = len + ShakeInfo.RemainDist; // 남은 거리 갱신
                ShakeInfo.RemainDist += len * 2f;

                ShakeInfo.DampingTime = ShakeInfo.RemainDist / ShakeInfo.Veclocity; // 댐핑 시간 재설정

                if (ShakeInfo.RemainDist < dist) break; // 반복 종료 조건
            }

            if (ShakeInfo.UseTotalTime && (ShakeInfo.TotalTime -= dt) < 0)
                break; // 전체 시간 초과 시 종료
            yield return new WaitForFixedUpdate(); // 다음 프레임까지 대기
        }

        ResetShakeTr(); // 흔들림 종료 후 초기화
        yield break;
    }
}
