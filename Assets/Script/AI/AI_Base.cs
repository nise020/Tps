using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class AiBase
    //MonoBehaviour//유니티에서 할당하는 메모리를 사용하겠다
{

    protected Monster MONSTER;
    protected Skill_Monster SKILL;
    protected AiState aIState = AiState.Create;
    protected MonsterType MobType;
    protected GameObject startObj;

    public bool nextOn_Off = true;
    public bool nowPatternOn = true;
    public bool moveChange = true;
    
    public void Type(MonsterType _eNum) 
    {
        MobType = _eNum;
    }
    public void init(Monster _Monster, Skill_Monster _SKILL) 
    {
        MONSTER = _Monster;
        SKILL = _SKILL;
    }

    public virtual void State(ref AiState _aIState) 
    {
        switch (aIState)
        {
            case AiState.Create:
                Create();
                break;
            case AiState.Search:
                Search();
                break;
            case AiState.Move:
                Move();
                break;
            case AiState.Attack:
                Attack();
                break;
            case AiState.Reset:
                Reset();
                break;
        }
    }
    protected virtual void Create() 
    {
        aIState = AiState.Search;
    }
    protected virtual void Search() 
    {
        aIState = AiState.Attack;
    }
    protected virtual void Move() 
    {
        aIState = AiState.Move;
    }
    protected virtual void Attack()
    {
        aIState = AiState.Reset;
    }
    protected virtual void Reset() 
    {
        aIState = AiState.Search;
    }
}
    
