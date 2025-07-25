using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Granad : Weapon
{
    public override WeaponType WeaponType => WeaponType.Sub;

    Vector3 GranadPos;
    public Vector3 startPos;
    [SerializeField] GameObject explotionObj;
    ParticleSystem explotionEffect;
    float groundCheckLenght = 0.2f;
    bool isGround = false;
    public SkillState skillstate = SkillState.SkillOff;

    MeshRenderer mesh;

    GameObject modelingObject;
    //Character character;

    private Coroutine effectCoroutine;

    float range = 10.0f;

    //GranadPos = startPos;
    //transform.position = GranadPos;
    //GetComponent<Rigidbody>().AddTorque(Vector3.forward * 200.0f);


    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(transform.localPosition, transform.
    //        TransformDirection(Vector3.down));
    //}

    public override void WeaponReset() 
    {
        modelingObject.SetActive(true);
        modelingObject.transform.localPosition = startPos;

        if (effectCoroutine != null)
            StopCoroutine(effectCoroutine);

        explotionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        skillstate = SkillState.SkillOff;

        
    }
    private void PlayExplosionEffect()
    {
        explotionEffect.gameObject.SetActive(true);
        explotionEffect.Play();


        if (effectCoroutine != null)
            StopCoroutine(effectCoroutine);


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
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Collision (tigger==false)물체가 통과가 안됨

        //trigger (tigger==ture)물체가 통과됨

        modelingObject.SetActive(false);
        skillstate = SkillState.SkillOn;

        PlayExplosionEffect();

        for (int i = 0; i < targetToAttackList.Count; i++) 
        {
            float value = Vector3.Distance(gameObject.transform.position,
                                           targetToAttackList[i].transform.position);
            if (value <= range) 
            {
                Character target = targetToAttackList[i].GetComponent<Character>();
                Shared.BattelManager.DamageCheck(CHARACTER, target, this);
            }
        }
    }

    private void expltioncheck()
    {
        if (skillstate == SkillState.SkillOn ||
            !modelingObject.activeSelf) { return; }

        //int layerMask = LayerMask.GetMask(LayerName.Monster.ToString());
        int mask = ~(1 << LayerMask.NameToLayer(LayerName.Monster.ToString()));

        Debug.Log($"mask = {mask}");

        if (Physics.Raycast(modelingObject.transform.position, 
            modelingObject.transform.
            TransformDirection(Vector3.down),
           out RaycastHit hit, groundCheckLenght, mask)) 
        {
            //int layer1 = LayerMask.NameToLayer(LayerName.Monster.ToString());
            if (hit.collider.gameObject == gameObject){ return; }

            Debug.Log($"{hit.collider.gameObject.layer}");
            modelingObject.SetActive(false);
            skillstate = SkillState.SkillOn;

            PlayExplosionEffect();

            //Collision (tigger==false)물체가 통과가 안됨

            //trigger (tigger==ture)물체가 통과됨

            //if (hit.collider.gameObject.layer != layer1)
            //{
            //    Debug.Log($"{hit.collider.gameObject.layer}");
            //    modelingObject.SetActive(false);
            //    skillstate = SkillState.SkillOn;

            //    PlayExplosionEffect();
            //}
            //else 
            //{
            //    GranadPos = startPos;
            //    transform.position = GranadPos;
            //    GetComponent<Rigidbody>().AddTorque(Vector3.one * 200.0f);
            //    return;
            //}
        }
    }

   
    private void Start()
    {
        explotionEffect = GetComponentInChildren<ParticleSystem>();
        startPos = transform.localPosition;

        explotionEffect.gameObject.SetActive(false);
        modelingObject.SetActive(false);
        gameObject.SetActive(false);


        //Torque < --회전력
        
    }
    private void Awake()
    {
        ItemStateData.WeaponType = WeaponclassType.Granad;

        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        modelingObject = mesh.gameObject;
        

        Power = 100;
    }
}
