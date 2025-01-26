using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class AiBase
    //MonoBehaviour//유니티에서 할당하는 메모리를 사용하겠다
{

    protected Monster MONSTER;
    protected MonsterSkill SKILL;
    protected eAI aIState = eAI.Create;
    protected eMobType MobType;
    protected GameObject startObj;

    public bool nextOn_Off = true;
    public bool nowPatternOn = true;
    public bool moveChange = true;
    
    public void Type(eMobType _eNum) 
    {
        MobType = _eNum;
    }
    public void init(Monster _Monster, MonsterSkill _SKILL) 
    {
        MONSTER = _Monster;
        SKILL = _SKILL;
    }

    public virtual void State() 
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
    
