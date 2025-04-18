
public enum MonsterAnimParameters
{
    Walk,
    Run,
    Attack,
    Close,
    Search,
    Idle,
    Serch,
    Open,
    Stop,

}
public enum MonsterAnim
{
    Idle,
    Attack,
    Serch,
    AttackDilray,
    Walk
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
public enum MonsterType //몬스터 태그
{
    Defolt,
    Dron,
    Spider,
    Sphere
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