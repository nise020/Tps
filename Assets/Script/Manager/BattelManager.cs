using Photon.Realtime;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TextCore.Text;

public class BattelManager : MonoBehaviour
{
    public UI_Battle ui;
    public GameObject CamAim;

    public bool GameOver = false;
    ObjectType objType = ObjectType.None;

    [Header("Player")]
    //�ѱ� �� �����
    public Player PLAYER;
    public Warrior WARRIOR;
    public Gunner GUNNER;
    public bool PlayerAlive = false;
    public GameObject playerUpper;//��ü
    public GameObject playerHand;//������
    PlayerControllState playerControll = PlayerControllState.Off;

    [Header("Gun")]
    public Gun GUN;
    public GameObject attackAim;
    [SerializeField] GameObject startPointObj;
    [SerializeField, Tooltip("���� ����")] public List<bool> AttackSearch;

    [Header("Character/List")]
    [SerializeField] List< Monster> Monsters;
    [SerializeField] List<GameObject> MonstersObj;
    [SerializeField] List< Player> Players;
    [SerializeField] List<GameObject> PlayersObj;
    [SerializeField] List< Character> Characters;
    [SerializeField] List<GameObject> CharactersObj;

    [SerializeField] Shader damageShader;
    [SerializeField] Material damageMaterial;

    //private HashSet<SkinnedMeshRenderer> damagedRenderers = new();
    Dictionary<Material, Shader> originalShaders = new();

    //Vector3 targetPos;
    int mincount = 0;
    int monsterCount = 0;
    //public Dictionary<int, Monster> monsterData = new Dictionary<int, Monster>();
    float spownTimer = 0.0f;
    float spownTime = 1.0f;
    int MobId = 0;
    int stageLevel = 0;

    #region memo
    //�۾� �ؾ� �Ұ�
    //start ���� �̸� ������Ʈ ���� �� �� Ȱ��ȭ+key ���� �ο�
    //������ ��Ȱ��ȭ
    //���� �ð� �� ��Ȱ(��ġ�� ���� ��ġ)
    //���ͳ� �÷��̾� ������ ����� �����۵� ����
    #endregion
    public List<GameObject> LoadToCharcterList(ObjectType _type) 
    {
        switch (_type)
        {
            case ObjectType.Player:
                //if (PlayersObj.Count == 0) 
                //{
                //    Shared.GameManager.
                //}
                return PlayersObj;

            case ObjectType.Monster:
                return MonstersObj;

            default:
                return null;
        }

    }
    public void AddDataToCharcterList(ObjectType _type, Character _character) 
    {
        switch (_type)
        {
            case ObjectType.Player:
                Players.Add(_character as Player);
                PlayersObj.Add(_character.gameObject);
                break;
            case ObjectType.Monster:
                Monsters.Add(_character as Monster);
                MonstersObj.Add(_character.gameObject);
                break;
        }
        Characters.Add(_character);
        CharactersObj.Add(_character.gameObject);
    }
    public void Timer() //�̰� �ΰ��� �ð����� ��� ����
    {
        
        spownTimer += Time.deltaTime;
        if (spownTimer >= spownTime) 
        {
            spownTimer = 0.0f;
        }
    }

    private void Awake()
    {
        if (Shared.BattelManager == null)
        {
            Shared.BattelManager = this;
            //SceneMgr �̱���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //private void Start()
    //{
    //    //DamageShadarLoad();
    //}

    private void DamageShadarLoad()
    {
        //damageShader = Resources.Load<Shader>("Shader/WhiteShader");
        Debug.Log($"damageShader = {damageShader}");
    }

    public void DamageCheck(Character _attacker, Character _defender,Item _weapon) 
    {
        float defenserHp = _defender.StatusTypeLoad(StatusType.HP);

        if (defenserHp <= 0) return;

        float attakerPower = _attacker.StatusTypeLoad(StatusType.Power);

        float weaponPower = 0.0f;

        if (_weapon != null) 
        {
            weaponPower = _weapon.WeaponStatusLoad(StatusType.Power);
        }
        //float CriticalValue = _attacker.StatusTypeLoad(StatusType.CritRate);

        //attakerPower = DamageCalculator(attakerPower, CriticalValue);

        defenserHp = defenserHp - (attakerPower + weaponPower);
        //defenserHp = defenserHp - 1000; <- TEST

        //State
        //Debug.Log($"attakerPower ={attakerPower}\n" +
        //    $"_attacker = {_attacker}" +
        //    $"weaponPower = {weaponPower}\n" +
        //    $"defenserHp = {defenserHp}");

        _defender.StatusUpLoad(defenserHp);

        if (defenserHp <= 0)
        {
            //GameEvents.DefenderState(true);
            _attacker.AttackEvent?.Invoke(true);
            _defender.StateEvent?.Invoke(AiState.Search);
        }
        else
        {
            _attacker.AttackEvent?.Invoke(false);
        }

        //sharder
        if (_defender.DamageEventCheck()) 
        {
            _defender.DamageEventUpdate(DamageEvent.Event_On);
            DamageColor(_defender);
        }

        HpBar hpBar = _defender.HpDataLoad();
        hpBar.AttackDamageEvent?.Invoke((int)attakerPower);

    }
    private float DamageCalculator(float _attakerPower ,float _cri) 
    {
        //ũ��Ƽ�� �ڵ� �߰� �ʿ�


        return _attakerPower;
    }
    private void DamageColor(Character _defender) 
    {
        SkinnedMeshRenderer[] renderers = _defender.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        //originalShaders.Clear();

        List<(SkinnedMeshRenderer renderer, Material[] originalMats)> backup = new();

        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            Material[] originalMats = renderer.sharedMaterials;
            Material[] damageMet = new Material[originalMats.Length];

            for (int i = 0; i < originalMats.Length; i++)
            {
                Material damageInstance = new Material(damageMaterial);
                if (originalMats[i].HasProperty("_MainTex"))
                    damageInstance.SetTexture("_MainTex", originalMats[i].GetTexture("_MainTex"));

                damageMet[i] = damageInstance;
                #region memo
                //Material origin = renderer.materials[i];
                ////Material damageMet = new Material(damageShader);

                //Material damageMet = damageMaterial;//���׸��� ����

                ////if (origin.HasProperty(ShaderOptionType._MainTex.ToString()))
                ////{
                ////    damageMet.SetTexture(ShaderOptionType._MainTex.ToString(),
                ////        origin.GetTexture(ShaderOptionType._MainTex.ToString()));
                ////}
                ////if (origin.HasProperty(ShaderOptionType._TintColor.ToString()))
                ////{
                ////    damageMet.SetTexture(ShaderOptionType._TintColor.ToString(),
                ////        origin.GetTexture(ShaderOptionType._TintColor.ToString()));
                ////}
                //renderer.materials[i] = damageMet;
                #endregion
            }
            renderer.materials = damageMet;
            backup.Add((renderer, originalMats));
            //damagedRenderers.Add(renderer);
            #region memo
            //foreach (Material material in renderer.materials)
            //{
            //    if (!originalShaders.ContainsKey(material))
            //    {
            //        originalShaders.Add(material, material.shader);

            //        material.shader = damageShader;
            //    }

            //}
            #endregion
        }

        StartCoroutine(MaterialBackUp(_defender,backup, 0.1f));
    }
    IEnumerator MaterialBackUp(Character _defender, List<(SkinnedMeshRenderer renderer,
        Material[] originalMats)> backup, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var(renderer, originalMats) in backup)
        {
            renderer.sharedMaterials = originalMats;
            _defender.DamageEventUpdate(DamageEvent.Event_Off);
            //damagedRenderers.Remove(renderer);
        }
    }
    public void JustAvoidance() 
    {
        TriggerSlowMotion();
    }
    public void TriggerSlowMotion(float scale = 0.3f, float duration = 0.5f)
    {
        StartCoroutine(SlowMotionCoroutine(scale, duration));
    }

    IEnumerator SlowMotionCoroutine(float scale, float duration)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02f * scale;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
    private void creatObject() 
    {
        //player
        PlayerModeState controll = PlayerModeState.Player;

        PLAYER.gameObject.transform.position = startPointObj.gameObject.transform.position;
        PLAYER.PlayerControllChange(controll);
        PlayerAlive = true;

        //playerCam.transform.position = PLAYER.transform.position;
        //MOVECAM.PlayerObj = PLAYER.gameObject;
        //Gun
        

        //monster
        //spownListArrange(STAGE[stageLevel]);

        //GameObject monster = Instantiate(GUN.gameObject, transform.position, Quaternion.identity, creatTab);
        
    }

    //IEnumerator MaterialBackUp(Charactor _defander) 
    //{
    //    yield return new WaitForSeconds(0.1f);

    //    //Material[] material = _defander.gameObject.GetComponentsInChildren<Material>();
    //    foreach (var mat in originalShaders)
    //    {
    //        mat.Key.shader = mat.Value;
    //    }
    //    originalShaders.Clear();
    //}
    //private void rollBackShader() 
    //{
    //    //Material[] material = _defender.gameObject.GetComponentsInChildren<Material>();
    //    //foreach (Material shader2 in material)
    //    //{
    //    //    Shader defoltShader = shader2.shader;

    //    //    shader2.shader = damageShader;

    //    //    shader2.shader = defoltShader;
    //    //}
    //}
}
