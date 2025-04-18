
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
public enum MonsterAiState //Monster AI ����
{
    Create,
    Search,
    Move,
    Attack,
    Reset,
}
public enum MonsterSpownLayer //Monster AI ����
{
    SpawnSpider,
    SpawnDron,
    SpawnSphere,
}
public enum MonsterType //���� �±�
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