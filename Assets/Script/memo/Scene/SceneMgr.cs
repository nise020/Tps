using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class SceneMgr : MonoBehaviour
{ 
    eScene Scene = eScene.Title;
    [SerializeField] Text IdText;
    [SerializeField] Button LoginBut;
    [SerializeField] GameObject InPutObj;
    //string Key;
    int key;
    bool check;
    private void Awake()
    {
        shared.SceneMgr = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (check==true) 
        {
            LoginBut.onClick.AddListener(Login);
            
        }
        else { return; }
        
    }
    private void Update()
    {
        InputFildcheck(check);
    }
    public void Login() 
    {
        SetPlayerPrefslntKey(IdText.text, key);
        Debug.Log($"{IdText.text},{key}");
    }
    public bool InputFildcheck(bool value) 
    {
        if (IdText.text.Length == 0)
        {
            value = false;
        }
        else 
        {
            value = true; 
        }
        return value;
    } 

}
