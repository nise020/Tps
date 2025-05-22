using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChangeButten : MonoBehaviour
{
    PlayerCamera MOVECAMERA;
    CharactorJobEnum playerType;

    Player PlayerCharacter = new Player();
    Player NpcCharacter = new Player();
    HpBar hpBar = new HpBar();

    [SerializeField] ButtenType buttenType = ButtenType.None;
   
    public void Initialize() 
    {
        switch (buttenType)
        {
            case ButtenType.Gunne_Change_Buttenr:
                PlayerCharacter = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Warrior);
                NpcCharacter = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Gunner);
                break;
            case ButtenType.Warror_Change_Butten:
                PlayerCharacter = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Gunner);
                NpcCharacter = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Warrior);

                break;
        }

        Text text = GetComponentInChildren<Text>();
        text.text = PlayerCharacter.name.ToString();


        hpBar = gameObject.GetComponentInChildren<HpBar>();
        Charactor charactor = PlayerCharacter;

        hpBar.inIt(charactor);
    }
    

    public void CharactorControllButten()//OnClick
    {
        Shared.GameManager.CharctorContoll(PlayerCharacter, CharctorStateEnum.Player);
        PlayerCharacter.ClearAllAnimation(CharactorJobEnum.Warrior);
        PlayerCameraCheck(PlayerCharacter, CharctorStateEnum.Player);
        AnotherPlayerReset(NpcCharacter, playerType, CharctorStateEnum.Npc);
        //if (PlayerCharacter.CharactorEnumCheck(CharactorJobEnum.Warrior) == true)
        //{
        //    Shared.GameManager.CharctorContoll(PlayerCharacter, CharctorStateEnum.Player);
        //    PlayerCharacter.ClearAllAnimation(CharactorJobEnum.Warrior);
        //    PlayerCameraCheck(PlayerCharacter, CharctorStateEnum.Player);
        //    AnotherPlayerReset(NpcCharacter, playerType, CharctorStateEnum.Npc);

        //    if (NpcCharacter.CharactorEnumCheck(CharactorJobEnum.Gunner) == true)
        //    {
        //    }
        //}
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
