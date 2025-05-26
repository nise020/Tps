using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GameEvents 
{
    public static Action<Character> onHpChanged;

    public static Action<Item> OnEnterRange;
    public static Action<Item> OnExitRange;

    public static Action<int> AttackDamageEvent;
    public static Action<UiInventory> InventoryTabEvent;

    public static Action<AI_Npc> PlyerChangeEvent;
}
