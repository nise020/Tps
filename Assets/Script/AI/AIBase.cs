using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class AiBase //: MonoBehaviour
    //MonoBehaviour//유니티에서 할당하는 메모리를 사용하겠다
{
    protected Charactor Charactor;

    protected eAI aIState = eAI.Create;
    protected virtual void Type() { }
    public void init(Charactor _Charactor) 
    {
        Charactor = _Charactor; 
    }

    public virtual void staet() 
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
    protected virtual void Reset() 
    {
        aIState = eAI.Reset;
    }
}
    
