using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class AiBase
    //MonoBehaviour//유니티에서 할당하는 메모리를 사용하겠다
{

    protected Monster MONSTER;
    protected Monster_Skill SKILL;
    protected eAI aIState = eAI.Create;
    protected eMobType MobType;

    public bool nextPatternOn = true;
    public bool nowPatternOn = true;
    public bool moveChange = true;

    public void Type(eMobType _eNum) 
    {
        MobType = _eNum;
    }
    public void init(Monster _Monster, Monster_Skill _SKILL) 
    {
        MONSTER = _Monster;
        SKILL = _SKILL;
    }

    public virtual void State(ref eAI _aIState) 
    {
        switch (aIState)
        {
            case eAI.Create:
                Create();
                break;
            case eAI.Search:
                Search();
                break;
            case eAI.Move:
                Move();
                break;
            case eAI.Attack:
                Attack();
                break;
            case eAI.Reset:
                Reset();
                break;
        }
    }
    protected virtual void Create() 
    {
        aIState = eAI.Search;
    }
    protected virtual void Search() 
    {
        aIState = eAI.Attack;
    }
    protected virtual void Move() 
    {
        aIState = eAI.Move;
    }
    protected virtual void Attack()
    {
        aIState = eAI.Reset;
    }
    protected virtual void Reset() 
    {
        aIState = eAI.Search;
    }
}
    
