using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class cCursur : MonoBehaviour
{
    Camera cam;
    [SerializeField] BoxCollider2D coll;
    [SerializeField] CapsuleCollider2D[] MobColl;//몬스터의 콜라이더
    [SerializeField] GameObject gunHoie;
    float time = 0.5f;
    float timer = 0.0f;
    CircleCollider2D CircleColl;
    public bool shoot = false;
    GameManager gameManager;
    private void Awake()
    {
        CircleColl = GetComponent<CircleCollider2D>();
        CircleColl.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instanse;
        transform.position = new Vector3(0, 0, 0);
        cam = Camera.main;
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            shoot = true;
            Debug.Log("Hit");
            CircleColl.enabled = false;
        }
    }

    void MobHit()
    {
        if (Input.GetMouseButton(0)) 
        {
            CircleColl.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        MobHit();
        sight();
        //cursurCheck();
    }
    private void sight() 
    {
        Vector3 vactor = cam.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log($"{vactor}");
        vactor.z = -20.0f;
        this.transform.position = vactor;
    }
    private void cursurCheck()//수정필요
    {
        float minXmax = coll.size.x/2;
        float minYmax = coll.size.y/2;
        this.transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -Mathf.Abs(minXmax), Mathf.Abs(minXmax)),
            Mathf.Clamp(transform.position.y, -Mathf.Abs(minYmax), Mathf.Abs(minYmax)),
            0.0f);
    }
}
