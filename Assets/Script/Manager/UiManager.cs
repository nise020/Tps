using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public Stack<UiBase> widgets = new Stack<UiBase>();
    public Ui_Inventory UI_INVENTORY;
    public UI_Battle UI_BATTEL;
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
    void Start()
    {
        //UiInit();
    }

    public void UiInit(UiBase _ui, UiType uiType)
    {
        switch (uiType) 
        {
            case UiType.Menu:
                UI_BATTEL = _ui as UI_Battle;
                break;
            case UiType.InvenTory:
                UI_INVENTORY = _ui as Ui_Inventory;
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
        if(widgets.Count == 0) return;
        if (Input.GetKey(KeyCode.Escape)) 
        {
            widgets.Pop();
        }
    }
}
