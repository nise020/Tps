using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] ParticleSystem BoomEffct1;
    [SerializeField] ParticleSystem SwordEffct2;
    [SerializeField] ParticleSystem GunEffct3;
    [SerializeField] GameObject Effct4;
    [SerializeField] GameObject Effct5;
    [SerializeField] float stopTimer = 3.0f;
    [SerializeField] Transform creatTab;
    private void Awake()
    {
        if (Shared.EffectManager == null)
        {
            Shared.EffectManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        creat();
    }
    private void creat() 
    {
        BoomEffct1 = CreatEffect(BoomEffct1);
        SwordEffct2 = CreatEffect(SwordEffct2);
        GunEffct3 = CreatEffect(GunEffct3);
    }
    private ParticleSystem CreatEffect(ParticleSystem _particle)
    {
        GameObject go = Instantiate(_particle.gameObject, creatTab);

        _particle = go.GetComponent<ParticleSystem>();

        _particle.gameObject.SetActive(false);

        return _particle;
    }

    public void Play(EffectType _type, Vector3 _pos) 
    {
        ParticleSystem particle = new ParticleSystem();
        switch (_type)
        {
            case EffectType.BoomEffect:
                particle = BoomEffct1;
                break;
            case EffectType.SowrdEffect:
                particle = SwordEffct2;
                break;
            case EffectType.GunEffect:
                particle = GunEffct3;
                break;
        }
        particle.gameObject.transform.position = _pos;

        particle.gameObject.SetActive(true);

        particle.Play();

        StartCoroutine(stopEffect(particle, stopTimer));
    }
    private IEnumerator stopEffect(ParticleSystem ps, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ps.Stop();
        ps.gameObject.SetActive(false);
    }

    public void Stop(EffectType _type)
    {
        switch (_type)
        {
            case EffectType.BoomEffect:
                BoomEffct1.Play();
                break;
        }
    }
    public void Pouse() 
    {

    }
}
