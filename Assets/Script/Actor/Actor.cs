using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
   [Tooltip("���Ͱ� ������ ����� �Ѿ�,��ô���� �޾Ƽ� ���")] public Transform ActTargetTrs { get; protected set; }
    //protected abstract void nomalAttack();���� ���� Ŭ����

}
