using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
   [Tooltip("����,�÷��̾� ���� ���")] public Transform ActTargetTrs { get; protected set; }
   [Tooltip("������ Ű")] public string ActKey { get; protected set; }

}
