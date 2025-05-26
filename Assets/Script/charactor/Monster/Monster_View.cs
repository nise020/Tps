using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class Monster : Character
{
    public float viewDistance = 10f; // Ž�� �Ÿ�
    public float viewAngle = 40f;   // Ž�� �þ߰�
    public Color debugColor = Color.red;
    public bool view(Transform _trs,GameObject _obj)
    {
        Vector3 forward = transform.forward * viewDistance;
        Quaternion leftRotation = Quaternion.Euler(0, -viewAngle * 0.5f, 0);
        Quaternion rightRotation = Quaternion.Euler(0, viewAngle * 0.5f, 0);

        Vector3 leftDir = leftRotation * forward;
        Vector3 rightDir = rightRotation * forward;

        Vector3 position = transform.position; // ���� ��ġ

        // Debug.DrawRay()�� ����Ͽ� ���� �� �信�� ǥ��
        Debug.DrawRay(position, leftDir, debugColor);
        Debug.DrawRay(position, rightDir, debugColor);
        Debug.DrawRay(position, forward, Color.blue); // ���� ���� (�����)

        return true;
    }



}
