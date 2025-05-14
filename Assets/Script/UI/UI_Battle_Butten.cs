using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UI_Battle : UiBase
{
    PlayerCamera MOVECAMERA;
    CharactorJobEnum playerType;

    public void AutoBuutten()//ing~
    {
        Shared.GameManager.PlayerbleDataLoad(out Dictionary<int, Player> _value);
    }

    public void CharactorControllButten1()//warrior 
    {

        Player warrior = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Warrior);
        Player gunner = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Gunner);

        if (warrior.CharactorEnumCheck(CharactorJobEnum.Warrior) == true)
        {
            Shared.GameManager.CharctorContoll(warrior, CharctorStateEnum.Player);
            warrior.ClearAllAnimation(CharactorJobEnum.Warrior);
            PlayerCameraCheck(warrior, CharctorStateEnum.Player);

            if (gunner.CharactorEnumCheck(CharactorJobEnum.Gunner) == true)
            {
                AnotherPlayerReset(gunner, playerType, CharctorStateEnum.Npc);
            }
        }
    }
    public void CharactorControllButten2()//gunner
    {
        Player warrior = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Warrior);
        Player gunner = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Gunner);
        if (gunner.CharactorEnumCheck(CharactorJobEnum.Gunner) == true)
        {
            Shared.GameManager.CharctorContoll(gunner, CharctorStateEnum.Player);
            gunner.ClearAllAnimation(CharactorJobEnum.Gunner);
            PlayerCameraCheck(gunner, CharctorStateEnum.Player);

            if (warrior.CharactorEnumCheck(CharactorJobEnum.Warrior) == true)
            {
                AnotherPlayerReset(warrior, playerType, CharctorStateEnum.Npc);
            }
        }
    }
    public void PlayerCameraCheck(Player _player, CharctorStateEnum _check)
    {
        _player.init(out MOVECAMERA);
        Camera camera = MOVECAMERA.gameObject.GetComponent<Camera>();
        if (_check == CharctorStateEnum.Player)
        {
            MOVECAMERA.gameObject.SetActive(true);
            camera = Camera.main;
            Shared.CameraManager.CameraChange(camera);
            Shared.MonsterManager.PlayerCameraUpdate();
        }
        else //npc
        {
            MOVECAMERA.gameObject.SetActive(false);
        }
        //MOVECAMERA.gameObject.transform.SetParent(_player.transform);
    }
    public void AnotherPlayerReset(Player _player, CharactorJobEnum _type, CharctorStateEnum _check)
    {
        //_player.playerTypeInite(out _type);//Load
        _player.ClearAllAnimation(_type);//Animation reset
        PlayerCameraCheck(_player, _check);//Camera On_Off
        Shared.GameManager.CharctorContoll(_player, CharctorStateEnum.Npc);//Controll Off
    }
}
