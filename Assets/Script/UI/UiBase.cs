using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UiBase : MonoBehaviour
{
    //Ÿ��
    //�ִϸ��̼� ��

    protected UiState state = UiState.Ui_Off;
    protected UiType uiType = UiType.None;

    protected virtual void Start()
    {
        Shared.UiManager.UiInit(this,uiType);
    }

    protected void OnEnable()//������
    {
        state = UiState.Ui_On;
        if (Shared.UiManager != null) 
        {
            Shared.UiManager.widgets.Push(this);
        }
    }

    protected void OnDisable()//������
    {
        state = UiState.Ui_Off;
    }
}
