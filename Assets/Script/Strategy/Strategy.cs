using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Strategy : MonoBehaviour
{
    protected State State;
    protected Player PLAYER;
    protected Weapon Weapon;

    protected WeaponEnum WeaponType = WeaponEnum.None;
    protected CharactorJobEnum playerType = CharactorJobEnum.None;
    protected int DamegeUp;
    abstract public void Skill(CharactorJobEnum _type, int _skillNumber, out int _damageValue);
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
    public void InitType(CharactorJobEnum _type)
    {
        playerType = _type;
    }
}
