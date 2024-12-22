using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    AudioSource Bgm;
    AudioSource Effect;

    private void Awake()
    {
        Shared.SoundMgr = this;
        DontDestroyOnLoad(this);
        Bgm = transform.Find("Bgm").GetComponent<AudioSource>();
        Bgm.loop = true;
        Effect = transform.Find("Effect").GetComponent<AudioSource>();
    }

    public void PlatBgm(string _Bgm) 
    {
        //2019 버전 이전에 문제가 있어서 아래 처럼 썻다
        Object obj = Resources.Load(_Bgm);

        if (obj == null)
            return;

        AudioClip clip = obj as AudioClip;

        if (null == clip)
            return;

        Bgm.clip = clip;
        Bgm.Play();
        //Bgm.Stop(); 초기화
        //Bgm.mute = false; 일시정지
    }
    public void PlayEffect(string _Effect) 
    {
        Object obj = Resources.Load(_Effect);

        if (null == obj)
            return;

        AudioClip clip = obj as AudioClip;

        if (null == clip)
            return;

        Effect.PlayOneShot(clip);//한번만 실행
    }
}
