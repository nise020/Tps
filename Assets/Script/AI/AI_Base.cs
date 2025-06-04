using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class AiBase
    //MonoBehaviour//유니티에서 할당하는 메모리를 사용하겠다
{

    protected Monster MONSTER;
    protected Player PLAYER;
    protected Skill_Monster SKILL;
    protected AiState aIState = AiState.Search;
    
    protected MonsterType MobType;
    protected GameObject startObj;
    protected Vector3 targetPos = new Vector3(0f, 0f, 0f);
    public bool nextOn_Off = true;
    public bool nowPatternOn = true;
    public bool moveChange = true;
    

    public void AIStateUpdate(AiState _state) 
    {
        aIState = _state;
    }
    public void init(Monster _Monster, Skill_Monster _SKILL) 
    {
        MONSTER = _Monster;
        SKILL = _SKILL;
    }
    
    public virtual void State() 
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
                Move(targetPos);
                break;
            case AiState.Attack:
                Attack();
                break;
            case AiState.Reset:
                Reset();
                break;
        }
    }
    //public virtual void State()
    //{
    //    switch (npcAi)
    //    {
    //        case NpcAiState.Search:
    //            Search();
    //            break;
    //        case NpcAiState.Move:
    //            Move(targetPos);
    //            break;
    //        case NpcAiState.Attack:
    //            Attack();
    //            break;
    //        case NpcAiState.Reset:
    //            Reset();
    //            break;
    //    }
    //}
    protected virtual void Create() 
    {
        aIState = AiState.Search;
    }
    protected virtual void Search() 
    {
        aIState = AiState.Attack;
    }
    protected virtual void Move(Vector3 _pos) 
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

    //protected virtual void Create(Player _obj)
    //{
    //    npcAi = NpcAiState.Search;
    //}
    //protected virtual void Search(Player _obj)
    //{
    //    npcAi = NpcAiState.Attack;
    //}
    //protected virtual void Move(Player _obj, Vector3 _pos)
    //{
    //    npcAi = NpcAiState.Move;
    //}
    //protected virtual void Attack(Player _obj)
    //{
    //    npcAi = NpcAiState.Reset;
    //}
    //public static int LayerNameEnum(LayerName layer)
    //{
    //    return LayerMask.NameToLayer(layer.ToString());
    //}
}
    
