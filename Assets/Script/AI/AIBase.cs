using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class AiBase
    //MonoBehaviour//유니티에서 할당하는 메모리를 사용하겠다
{
    Monster Monster;

    protected eAI aIState = eAI.Create;
    [SerializeField] protected eMobType MobType = eMobType.Defolt;
    protected HugeMob Huge;
    protected FlyingMob Flying;
    protected DefoltMob Defolt;
    public bool nextPatternOn = true;
    public bool moveChange = true;

    protected virtual void Type() 
    {
        
    }
    public void init(Monster _Monster) 
    {
        Monster = _Monster; 
    }

    public virtual void state() 
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
        aIState = eAI.Create;
    }
    protected virtual void Search() 
    {
        aIState = eAI.Search;
    }
    protected virtual void Move() 
    {
        aIState = eAI.Move;
    }
    protected virtual void Attack()
    {
        aIState = eAI.Attack;
    }
    protected virtual void Reset() 
    {
        aIState = eAI.Reset;
    }
}
    
