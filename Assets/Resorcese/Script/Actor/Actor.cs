using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
   [Tooltip("몬스터,플레이어 공통 사용")] public Transform ActTargetTrs { get; protected set; }
   [Tooltip("유저의 키")] public string ActKey { get; protected set; }

}
