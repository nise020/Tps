using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.ProBuilder.Shapes;
using UnityEngine.U2D;//아틀라스
using UnityEngine.UI;

public partial class AtlasManager : MonoBehaviour
{
    
    [NonReorderable]
    Dictionary<string,SpriteAtlas> DicSpritAtlas = new Dictionary<string,SpriteAtlas>();
    public List<Image> UiImag;
    public string AtlasName;
    public List<string> SpritName;
    public UnityEngine.Sprite GetSpritAtlas(string _Atlas, string _name) 
    {
        if (DicSpritAtlas.ContainsKey(_Atlas))
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

    private void Awake()
    {
        if (Shared.AtlasManager == null)
        {
            Shared.AtlasManager = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void MySprite() 
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        for (int iNum = 0; iNum < SpritName.Count; iNum++)
        {
            UiImag[iNum].overrideSprite = GetSpritAtlas($"{AtlasName[iNum]}", $"{SpritName[iNum]}");
            Debug.Log($"Image Object Name = {AtlasName[iNum]},{SpritName[iNum]}");
        }

        gameObject.AddComponent<SpriteRenderer>();

        //GetSpritAtlas(string _Atlas, string _name)
    }
    private void Start()
    {
        MySprite();
        //GetSpritAtlas("Common", "ak-47");
        //GetSpritAtlas("Damage", "Number1 7x10");
    }
    public void AtlasLoad(GameObject _ImageObj) 
    {

    }
}
