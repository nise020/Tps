using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public partial class AiBase
    //MonoBehaviour//유니티에서 할당하는 메모리를 사용하겠다
{

    protected Monster MONSTER;
    protected Player PLAYER;
    protected Skill_Monster SKILL;
    protected MonsterAiState aIState = MonsterAiState.Create;
    protected NpcAiState npcAi = NpcAiState.Search;
    protected MonsterType MobType;
    protected GameObject startObj;
    protected Vector3 targetPos = new Vector3(0f, 0f, 0f);
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
    public void init(Player _player)
    {
        PLAYER = _player;
    }
    public virtual void State() 
    {
        switch (aIState)
        {
            case MonsterAiState.Create:
                Create();
                break;
            case MonsterAiState.Search:
                Search();
                break;
            case MonsterAiState.Move:
                Move(targetPos);
                break;
            case MonsterAiState.Attack:
                Attack();
                break;
            case MonsterAiState.Reset:
                Reset();
                break;
        }
    }
    public virtual void State(CharctorStateEnum _state, Player _player,out NpcAiState _Ai)
    {
        if (_state == CharctorStateEnum.Player) 
        {
            _Ai = new NpcAiState();
            return;
        } 

        switch (npcAi)
        {
            case NpcAiState.Search:
                Search();
                break;
            case NpcAiState.Move:
                Move(targetPos);
                break;
            case NpcAiState.Attack:
                Attack();
                break;
            case NpcAiState.Reset:
                Reset();
                break;
        }
        _Ai = new NpcAiState();
    }
    protected virtual void Create() 
    {
        aIState = MonsterAiState.Search;
    }
    protected virtual void Search() 
    {
        aIState = MonsterAiState.Attack;
    }
    protected virtual void Move(Vector3 _pos) 
    {
        aIState = MonsterAiState.Move;
    }
    protected virtual void Attack()
    {
        aIState = MonsterAiState.Reset;
    }
    protected virtual void Reset() 
    {
        aIState = MonsterAiState.Search;
    }

    protected virtual void Create(Player _obj)
    {
        npcAi = NpcAiState.Search;
    }
    protected virtual void Search(Player _obj)
    {
        npcAi = NpcAiState.Attack;
    }
    protected virtual void Move(Player _obj, Vector3 _pos)
    {
        npcAi = NpcAiState.Move;
    }
    protected virtual void Attack(Player _obj)
    {
        npcAi = NpcAiState.Reset;
    }
    public static int LayerNameEnum(LayerName layer)
    {
        return LayerMask.NameToLayer(layer.ToString());
    }
}
    
