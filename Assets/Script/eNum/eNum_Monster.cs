public enum Condition //상태패턴
{
    health,
    hard,
    Death,
}
public enum MonsterAiState //Monster AI 상태
{
    Create,
    Search,
    Move,
    Attack,
    Reset,
}
public enum MonsterSpownLayer //Monster AI 상태
{
    SpawnSpider,
    SpawnDron,
    SpawnSphere,
}

public enum MonsterWalkState
{
    Walk_On,
    Walk_Off,
}
public enum MonsterAttackState
{
    Attack_On,
    Attack_Off,
}