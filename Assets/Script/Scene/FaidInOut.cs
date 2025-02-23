using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FaidInOut : MonoBehaviour
{
    [SerializeField] public bool fade = false;//Ȯ�ο�
    float fadeTime = 1.0f;
    Image FadeImg;
    UnityAction action = null;
    private void Awake()
    {
        if (Shared.FaidInOut == null)
        {
            Shared.FaidInOut = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//�����
            return;
        }
        FadeImg = GetComponentInChildren<Image>();
    }


    // Update is called once per frame
    void Update()
    {
        faidImgInOut();
    }

    private void faidImgInOut()
    {
        if (fade == true && FadeImg.color.a < 1)//On,�������
        {
            Color color = FadeImg.color;
            color.a += Time.deltaTime / fadeTime;
            if (color.a > 1)
            {
                color.a = 1;
                if (action != null)
                {
                    action.Invoke();
                    action = null;
                }
            }
            FadeImg.color = color;
        }
        else if (fade == false && FadeImg.color.a > 0)//Off,��ο�����
        {
            Color color = FadeImg.color;
            color.a -= Time.deltaTime / fadeTime;
            if (color.a < 0)
            {
                color.a = 0;
            }
            FadeImg.color = color;
        }
        FadeImg.raycastTarget = FadeImg.color.a != 0.0f;
    }
    public void ActiveFade(bool _fade, UnityAction _action)//= null
    {
        fade = _fade;
        action = _action;
    }
}
