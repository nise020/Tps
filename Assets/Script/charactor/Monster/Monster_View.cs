using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class Monster : Character
{
    public float viewDistance = 10f; // 탐지 거리
    public float viewAngle = 40f;   // 탐지 시야각
    public Color debugColor = Color.red;
    public bool view(Transform _trs,GameObject _obj)
    {
        Vector3 forward = transform.forward * viewDistance;
        Quaternion leftRotation = Quaternion.Euler(0, -viewAngle * 0.5f, 0);
        Quaternion rightRotation = Quaternion.Euler(0, viewAngle * 0.5f, 0);

        Vector3 leftDir = leftRotation * forward;
        Vector3 rightDir = rightRotation * forward;

        Vector3 position = transform.position; // 몬스터 위치

        // Debug.DrawRay()를 사용하여 선을 씬 뷰에서 표시
        Debug.DrawRay(position, leftDir, debugColor);
        Debug.DrawRay(position, rightDir, debugColor);
        Debug.DrawRay(position, forward, Color.blue); // 정면 방향 (참고용)

        return true;
    }



}
