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
    public SkillState skillstate = SkillState.SkillOff;

    MeshRenderer mesh;

    GameObject modelingObject;
    //private void OnTriggerEnter(Collider other)
    //{
    //    explotionEffect.Play();
    //    Invoke("ResetObject", 3.0f);
    //}
    private void Update()
    {
        expltioncheck();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.localPosition, transform.
            TransformDirection(Vector3.down));
    }
    private void expltioncheck()
    {
        if (skillstate == SkillState.SkillOn) { return; }
        
        if (Physics.Raycast(modelingObject.transform.position, 
            modelingObject.transform.
            TransformDirection(Vector3.down),
           out RaycastHit hit, groundCheckLenght)) 
        {
            int layer = LayerMask.NameToLayer(LayerName.Monster.ToString());
            if (hit.collider.gameObject.layer != layer) 
            {
                //Debug.Log($"hit = {hit.transform}");
                //Debug.Log($"transform.position = {transform.position}");
                skillstate = SkillState.SkillOn;
                explotionEffect.gameObject.SetActive(true);
                explotionEffect.Play();
                Invoke("ResetObject", 3.0f);
            }
        }
    }

    private void ResetObject() 
    {
        skillstate = SkillState.SkillOff;
        transform.localPosition = startPos;
        explotionEffect.Stop();
        explotionEffect.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    private void Start()
    {
        explotionEffect = GetComponentInChildren<ParticleSystem>();
        explotionEffect.gameObject.SetActive(false);
        startPos = transform.localPosition;
        gameObject.SetActive(false);

        //Torque < --È¸Àü·Â
        //GranadPos = startPos;
        //transform.position = GranadPos;
        //GetComponent<Rigidbody>().AddTorque(Vector3.one * 200.0f);
    }
    private void Awake()
    {
        ItemStateData.weaponType = WeaponEnum.Granad;

        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        modelingObject = mesh.gameObject;
        //isGround = Physics.Raycast(transform.position, Vector3.down,
        //   out RaycastHit hit, groundCheckLenght + 0.1f);
    }
}
