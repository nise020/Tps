using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public Stack<UiBase> WidgetStack = new Stack<UiBase>();
    public UiInventory UI_INVENTORY;
    public UI_Battle UI_BATTEL;

    Action UiOffEvent;
    private void Awake()
    {
        if (Shared.UiManager == null)
        {
            Shared.UiManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update


    public void UiInit(UiBase _ui, UiType uiType)
    {
        switch (uiType) 
        {
            case UiType.Menu:
                UI_BATTEL = _ui as UI_Battle;
                break;
            case UiType.InvenTory:
                UI_INVENTORY = _ui as UiInventory;
                break;
            default:
                return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UiExit();
    }

    private void UiExit()
    {
        if(WidgetStack.Count == 0) return;
        if (Input.GetKey(KeyCode.Escape))
        {
            closeUi();
        }

        if (Input.GetKeyDown(KeyCode.I)) 
        {
            ShowUI(UI_INVENTORY);
        }
    }
    private void closeUi() 
    {
        if (WidgetStack.Count > 0) 
        {
            var topUI = WidgetStack.Peek();
            topUI.Close(); 
        }
    }

    //public void ShowUI(UiBase _ui)
    //{
    //    _ui.UiEvent = () => widgets.Pop(); // 닫을 때 stack에서도 제거
    //    widgets.Push(_ui);
    //    _ui.Open();
    //}

    public bool IsTop(UiBase ui)
    {
        return WidgetStack.Count > 0 && WidgetStack.Peek() == ui;
    }

    public void ToggleUI(UiBase ui)
    {
        if (IsTop(ui))
        {
            ui.Close(); // 이 안에서 Stack Pop
        }
        else
        {
            ShowUI(ui);
        }
    }

    public void ShowUI(UiBase ui)
    {
        if (WidgetStack.Contains(ui))
            return; // 이미 열린 상태 방지

        ui.UiEvent = () =>
        {
            if (WidgetStack.Count > 0 && WidgetStack.Peek() == ui)
                WidgetStack.Pop();
        };

        WidgetStack.Push(ui);
        ui.Open();
    }

    public void CloseTopUI()
    {
        if (WidgetStack.Count > 0)
        {
            var topUI = WidgetStack.Peek();
            topUI.Close();
        }
    }

    public bool HasUI(UiBase ui) => WidgetStack.Contains(ui);
}
