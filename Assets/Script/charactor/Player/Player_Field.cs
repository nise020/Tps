using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Character
{
    protected PlayerCamera viewcam;
    protected AI_Auto PLAYERAI = new AI_Auto();
    
    protected Rigidbody rigid;
    protected Animator playerAnimator;

    protected GameObject cameraViewObj;

    [Header("Weapon")]
    [SerializeField] GameObject WeaponPrefab;
    [SerializeField] GameObject shortSword;

    [Header("Aim")]
    [SerializeField] Transform AimtransPos;//명중 오브젝트
    [SerializeField] Button ControlBtn;

    [Header("Walk State")]
    float burst_RunTime;
    protected int attackReset;

    protected int pluse_bullet;
    protected int RelodingBullet;
    protected float walkStateChangeTime = 3.0f;
    protected float walkStateChangeTimer = 0.0f;
    protected float dashDistanse = 15.0f;
    public float dashTime = 0.2f;
    public bool dashCheck = false;




    [Header("Avoidance Status")]
    protected float moveHeight = 2.0f;
    protected float backDistance = 5f;



    protected SkillStrategy skillStrategy = new SkillStrategy();

    [Header("Animator Info")]
    protected int attackLayerIndex = 1;
    protected int BaseLayerIndex = 0;
    [SerializeField] bool shitCheack = false;
    [SerializeField] bool closeCheck = false;
    protected bool canReceiveInput = false;

    [Header("Skill Effect")]
    [SerializeField] protected GameObject SkillEffectObj1;
    [SerializeField] protected GameObject SkillEffectObj2;
    protected ParticleSystem SkillEffectSystem1 = null;
    protected ParticleSystem SkillEffectSystem2 = null;
    [SerializeField] protected GameObject SkillParentObj1 = null;
    [SerializeField] protected GameObject SkillParentObj2 = null;


    [Header("CharacterData")]
    protected PlayerStateData playerStateData = new PlayerStateData();

    protected PlayerCameraMode cameraMode = PlayerCameraMode.CameraRotationMode;

    List<GameObject> backPositionObject;//my position Object
    float runDistanseValue = 15.0f;
    float playerStopDistanseValue = 0.3f;

    [Header("Npc Move Position")]
    protected FindMoveObject findMoveObject = FindMoveObject.None;
    protected Slot rightObj;
    PositionObjectState rightObjState = PositionObjectState.None;

    protected Slot reftObj;
    PositionObjectState reftObjState = PositionObjectState.None;

    protected float notWalkTimer = 0;
    protected float notWalkTime = 3.0f;

    protected GameObject playerBackObject;

    protected Vector3 movePosition = new Vector3();
    protected Vector3 targetPos = new Vector3();
    Queue<Vector3> aiMovePosQue = new Queue<Vector3>();
    LayerName slotlayerName = LayerName.None;
    List<Slot> slotLists = new List<Slot>();

    [Header("Battel System")]
    protected BattelTrigger battelTrigger = new BattelTrigger();
    protected GameObject battelTriggerObj = new GameObject();
    protected Collider battelTriggerCol = new Collider();

}
