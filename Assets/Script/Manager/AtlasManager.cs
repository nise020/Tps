using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;//아틀라스
using UnityEngine.UI;

public partial class AtlasManager : MonoBehaviour
{
    
    [NonReorderable]
    Dictionary<string,SpriteAtlas> DicSpritAtlas = new Dictionary<string,SpriteAtlas>();
    public List<Image> UiImag;
    public string AtlasName;
    public List<string> SpritName;
    public Sprite GetSpritAtlas(string _Atlas, string _name) 
    {
        if (DicSpritAtlas.ContainsKey(_name))
            return DicSpritAtlas[_Atlas].GetSprite(_name);

        UnityEngine.Object obj = null;

        obj = Resources.Load("Atlas/"+ _Atlas);

        if (obj == null) 
        {
            Debug.Log("load fail" + _Atlas); 
            return null;
        }

        SpriteAtlas sa = obj as SpriteAtlas;

        if (sa != null) 
        {
            DicSpritAtlas.Add(_Atlas, sa);
            return sa.GetSprite(_name);
        }

        return null;
    }


    public void MySprite() 
    {
        //SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        for (int iNum = 0; iNum < SpritName.Count; iNum++) 
        {
            //UIimag = GetSpritAtlas($"{AtlasName[iNum]}", $"{SpritName[iNum]}");
        }
        
        //gameObject.AddComponent<SpriteRenderer>();

        //GetSpritAtlas(string _Atlas, string _name)
    }
    private void Start()
    {
        MySprite();
        //GetSpritAtlas("Common", "ak-47");
        //GetSpritAtlas("ak-47", "Common");
    }
}
