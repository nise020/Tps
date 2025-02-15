using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class AiBase
    //MonoBehaviour//유니티에서 할당하는 메모리를 사용하겠다
{

    protected Monster MONSTER;
    protected Skill_Monster SKILL;
    protected AI aIState = AI.Create;
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

    public virtual void State(ref AI _aIState) 
    {
        switch (aIState)
        {
            case AI.Create:
                Create();
                break;
            case AI.Search:
                Search();
                break;
            case AI.Move:
                Move();
                break;
            case AI.Attack:
                Attack();
                break;
            case AI.Reset:
                Reset();
                break;
        }
    }
    protected virtual void Create() 
    {
        aIState = AI.Search;
    }
    protected virtual void Search() 
    {
        aIState = AI.Attack;
    }
    protected virtual void Move() 
    {
        aIState = AI.Move;
    }
    protected virtual void Attack()
    {
        aIState = AI.Reset;
    }
    protected virtual void Reset() 
    {
        aIState = AI.Search;
    }
}
    
