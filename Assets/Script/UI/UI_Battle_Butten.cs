using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UI_Battle : UiBase
{
    IEnumerator ButtenInit() 
    {
        player_Change_Butten1.Initialize(ButtenType.Warror_Change_Butten);
        player_Change_Butten2.Initialize(ButtenType.Gunne_Change_Buttenr);
        yield break;
    }
    public void On_Menu() 
    {
        Shared.UiManager.UI_INVENTORY.InventoryTabCheck(true);
        Off_Butten.gameObject.SetActive(true);
        On_Butten.gameObject.SetActive(false);
    }
    public void OFF_Menu()
    {
        Shared.UiManager.UI_INVENTORY.InventoryTabCheck(false);
        On_Butten.gameObject.SetActive(true);
        Off_Butten.gameObject.SetActive(false);
    }
}
