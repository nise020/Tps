using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Strategy : MonoBehaviour
{
    protected State State;
    protected Player PLAYER;
    protected Weapon Weapon;

    protected WeaponclassType WeaponType = WeaponclassType.None;
    protected PlayerType playerType = PlayerType.None;
    protected int DamegeUp;
    abstract public void Skill(PlayerType _type, int _skillNumber, out int _damageValue);
    public void init(State _state) 
    {
        State = _state;
    }
    public void PlayerInit(Player _player)
    {
        PLAYER = _player;
    }
    public void WeaponInit(Weapon _gun)
    {
        Weapon = _gun;
    }
    public void InitType(PlayerType _type)
    {
        playerType = _type;
    }
}
