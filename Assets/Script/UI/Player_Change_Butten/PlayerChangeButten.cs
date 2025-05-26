using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChangeButten : MonoBehaviour
{
    PlayerCamera MOVECAMERA;
    PlayerType playerType;

    Player PlayerCharacter = new Player();
    Player NpcCharacter = new Player();
    HpBar hpBar = new HpBar();

    //[SerializeField] ButtenType buttenType = ButtenType.None;
   
    public void Initialize(ButtenType _type) 
    {
        switch (_type)
        {
            case ButtenType.Gunne_Change_Buttenr:
                PlayerCharacter = Shared.GameManager.PlayerDataLoad(PlayerType.Gunner);
                NpcCharacter = Shared.GameManager.PlayerDataLoad(PlayerType.Warrior);
  
                break;
            case ButtenType.Warror_Change_Butten:
                PlayerCharacter = Shared.GameManager.PlayerDataLoad(PlayerType.Warrior);
                NpcCharacter = Shared.GameManager.PlayerDataLoad(PlayerType.Gunner);

                break;
        }
        NpcCharacter.AiUpdate(PlayerCharacter);

        Text text = GetComponentInChildren<Text>();
        text.text = PlayerCharacter.name.ToString();


        hpBar = gameObject.GetComponentInChildren<HpBar>();
        Character charactor = PlayerCharacter;

        hpBar.CharactorInIt(charactor);
    }
    

    public void CharactorControllButten()//OnClick
    {
        Shared.GameManager.CharctorContoll(PlayerCharacter, PlayerModeState.Player);
        PlayerCharacter.ClearAllAnimation(PlayerType.Warrior);
        PlayerCameraCheck(PlayerCharacter, PlayerModeState.Player);
        AnotherPlayerReset(NpcCharacter, playerType, PlayerModeState.Npc);
        
    }

    public void PlayerCameraCheck(Player _player, PlayerModeState _check)
    {
        _player.init(out MOVECAMERA);
        Camera camera = MOVECAMERA.gameObject.GetComponent<Camera>();
        if (_check == PlayerModeState.Player)
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
    public void AnotherPlayerReset(Player _npc, PlayerType _type, PlayerModeState _check)
    {
        _npc.AiUpdate(PlayerCharacter);
        //_player.playerTypeInite(out _type);//Load
        _npc.ClearAllAnimation(_type);//Animation reset
        PlayerCameraCheck(_npc, _check);//Camera On_Off
        Shared.GameManager.CharctorContoll(_npc, PlayerModeState.Npc);//Controll Off
    }
}
