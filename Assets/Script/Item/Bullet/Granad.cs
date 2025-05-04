using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Granad : Weapon
{
    Vector3 GranadPos;
    public Vector3 startPos;
    [SerializeField] GameObject explotionObj;
    ParticleSystem explotionEffect;
    float groundCheckLenght = 0.1f;
    bool isGround = false;
    public SkillState state = SkillState.SkillOff;
    private void OnTriggerEnter(Collider other)
    {
        explotionEffect.Play();
        Invoke("ResetObject", 3.0f);
    }
    private void Update()
    {
        expltioncheck();
    }

    private void expltioncheck()
    {
        if (state == SkillState.SkillOn) { return; }
        
        if (Physics.Raycast(transform.position, transform.
            TransformDirection(Vector3.down),
           out RaycastHit hit, groundCheckLenght)) 
        {
            state = SkillState.SkillOn;
            explotionEffect.gameObject.SetActive(true);
            explotionEffect.Play();
            Invoke("ResetObject", 3.0f);
        }
    }

    private void ResetObject() 
    {
        state = SkillState.SkillOff;
        transform.position = startPos;
        explotionEffect.Stop();
        gameObject.SetActive(false);
    }
    private void Start()
    {
        explotionEffect = GetComponentInChildren<ParticleSystem>();
        explotionEffect.gameObject.SetActive(false);
        startPos = transform.position;
        gameObject.SetActive(false);

        //Torque < --È¸Àü·Â
        //GranadPos = startPos;
        //transform.position = GranadPos;
        //GetComponent<Rigidbody>().AddTorque(Vector3.one * 200.0f);
    }
    private void Awake()
    {
        WeaponType = WeaponEnum.Granad;
        isGround = Physics.Raycast(transform.position, Vector3.down,
           out RaycastHit hit, groundCheckLenght + 0.1f);
    }
}
