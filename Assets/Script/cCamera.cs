using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCamera : MonoBehaviour
{
    Camera cam;
    //public Transform  gunhoie;
    public GameObject GunHole;       
    public float rotationSpeed = 5.0f; 

    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }
 
}
