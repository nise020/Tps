using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Granad : Weapon
{
    Vector3 GranadPos;
    public Vector3 startPos;
    [SerializeField] GameObject explotionObj;
    ParticleSystem explotionEffect;
    float groundCheckLenght = 0.2f;
    bool isGround = false;
    public SkillState skillstate = SkillState.SkillOff;

    MeshRenderer mesh;

    GameObject modelingObject;
    Character character;

    private Coroutine effectCoroutine;
    public void CharcterInit(Character _charactor)
    {
        character = _charactor;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.localPosition, transform.
            TransformDirection(Vector3.down));
    }
    public void WeaponObjectOn() 
    {
        modelingObject.SetActive(true);
        modelingObject.transform.localPosition = startPos;

        explotionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        skillstate = SkillState.SkillOff;
    }

    private void Update()
    {
        expltioncheck();
    }
    private void expltioncheck()
    {
        if (skillstate == SkillState.SkillOn ||
            !modelingObject.activeSelf) { return; }
        
        if (Physics.Raycast(modelingObject.transform.position, 
            modelingObject.transform.
            TransformDirection(Vector3.down),
           out RaycastHit hit, groundCheckLenght)) 
        {
            int layer1 = LayerMask.NameToLayer(LayerName.Monster.ToString());

            if (hit.collider.gameObject == gameObject){ return; }

            if (hit.collider.gameObject.layer != layer1)
            {
                Debug.Log($"{hit.collider.gameObject.layer}");
                modelingObject.SetActive(false);
                skillstate = SkillState.SkillOn;

                PlayExplosionEffect();
            }
            else 
            {
                return;
            }
        }
    }

    private void PlayExplosionEffect()
    {
        // 강제 리셋
        //explotionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        explotionEffect.gameObject.SetActive(true);
        explotionEffect.Play();

        // 기존 종료 코루틴 취소
        if (effectCoroutine != null)
            StopCoroutine(effectCoroutine);

        // 새로 시작
        effectCoroutine = StartCoroutine(AutoStopEffect());
    }

    private IEnumerator AutoStopEffect()
    {
        yield return new WaitForSeconds(explotionEffect.main.duration);

        explotionEffect.gameObject.SetActive(false);
        if (explotionEffect.isPlaying) 
        {
            explotionEffect.Stop();
        }
        explotionEffect.gameObject.transform.localPosition = startPos;
        gameObject.SetActive(false);
        //ResetObject(); // 위치 초기화 등
    }

    private void Start()
    {
        explotionEffect = GetComponentInChildren<ParticleSystem>();
        startPos = transform.localPosition;

        explotionEffect.gameObject.SetActive(false);
        modelingObject.SetActive(false);
        gameObject.SetActive(false);


        //Torque < --회전력
        //GranadPos = startPos;
        //transform.position = GranadPos;
        //GetComponent<Rigidbody>().AddTorque(Vector3.one * 200.0f);
    }
    private void Awake()
    {
        ItemStateData.WeaponType = WeaponEnum.Granad;

        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        modelingObject = mesh.gameObject;
        //isGround = Physics.Raycast(transform.position, Vector3.down,
        //   out RaycastHit hit, groundCheckLenght + 0.1f);
    }
}
