using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UiBase : MonoBehaviour
{
    //타입
    //애니메이션 등

    protected UiState state = UiState.Ui_Off;
    protected UiType uiType = UiType.None;

    protected virtual void Start()
    {
        Shared.UiManager.UiInit(this,uiType);
    }

    protected void OnEnable()//껴질때
    {
        state = UiState.Ui_On;
        if (Shared.UiManager != null) 
        {
            Shared.UiManager.widgets.Push(this);
        }
    }

    protected void OnDisable()//꺼질때
    {
        state = UiState.Ui_Off;
    }
}
