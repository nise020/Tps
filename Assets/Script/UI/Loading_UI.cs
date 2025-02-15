using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Loading : Actor
{
    float LodingTime = 10.0f;
    [SerializeField] Image maxImg;
    // Start is called before the first frame update
    void Start()
    {
        if (maxImg.fillAmount>= 0.0f) 
        {
            maxImg.fillAmount = 0.0f;
        }

        StartCoroutine(StartBar());
    }

    IEnumerator StartBar() 
    {
        while (maxImg.fillAmount <= 1.0f) 
        {
            maxImg.fillAmount += Time.deltaTime / LodingTime;
            yield return null;
            
        }
        maxImg.fillAmount = 1.0f;
        if (maxImg.fillAmount == 1.0f) 
        {
            Shared.SceneMgr.chageScene(Scene.Lobby);
        }


    }
}
