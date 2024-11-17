using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : Actor
{
    float LodingTime = 10.0f;
    [SerializeField] Image maxImg;
    // Start is called before the first frame update
    void Start()
    {
        StartBar();
    }

    IEnumerator StartBar() 
    {

        yield return null;
    }
}
